using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Character : Units
{
    [SerializeField] private CharacterParameters parameters;

    [SerializeField] private GameObject HPText;
    [SerializeField] private GameObject bulletsText;
    [SerializeField] private ParticleSystem bloodParticles;

    [SerializeField] private Texture2D aimCursorTexture;
    [SerializeField] private Texture2D reloadCursorTexture;
    [SerializeField] private GameObject hand;
    [SerializeField] private Transform body;
    [SerializeField] private GameObject[] legs;

    private float offset;
    private bool isLockShootCoroutine;
    private Rigidbody2D rigidBody;
    private Weapon weaponAttack;

    private void Awake()
    {
        Time.timeScale = 1;
        rigidBody = GetComponent<Rigidbody2D>();
        SetWeapon(SaveParameters.weaponsBought[SaveParameters.weaponEquip]);
        weaponAttack.Reloading += SetReloadCursor;
        weaponAttack.Reloaded += SetAimCursor;
    }

    private void Start()
    {
        HealthPoints = parameters.HealthPoints;
        SaveParameters.numberOfCoinsRaised = 0;
        SetAimCursor();
    }

    private void FixedUpdate()
    {
        HPText.GetComponent<Text>().text = $"{HealthPoints}";
        bulletsText.GetComponent<Text>().text = $"{weaponAttack.AmountBulletsInMagazine + "/" + weaponAttack.AmountBulletsInMagazineStandart}";
        RotateWeapons();
        RotateBody();
    }

    private void Update()
    {
        IsGrounded();
        Run();
        if (Input.GetButtonDown("Jump") && IsGrounded())
            Jump();
        if (Input.GetMouseButton(0) && !isLockShootCoroutine)
            StartCoroutine(Shoot());
    }

    // Отвечает за поворот оружия в направлении мыши
    public void RotateWeapons()
    {
        // позиция мыши + текущая позиция персонажа
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; 
        float rotate = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        hand.transform.rotation = Quaternion.Euler(0f, 0f, rotate + offset);
    }

    // Отвечает за поворот тела в сторону мыши
    public void RotateBody()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Vector3 pos = body.transform.localScale;
        body.transform.localScale = new Vector3(
            (difference.x < 0 ? Math.Abs(pos.x) * -1 : Math.Abs(pos.x)) * (transform.localScale.x > 0 ? -1 : 1),
            pos.y,
            pos.z
            );

        if (pos.x > 0)
        {
            offset = -180;
            if (transform.localScale.x < 0)
                offset = 0;
            return;
        }
        offset = 0;
        if (transform.localScale.x < 0)
            offset = -180;
    }

    // Отвечает за бег
    private void Run()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        gameObject.GetComponentInChildren<Animation>().IsRun = horizontal != 0;
        legs[0].GetComponentInChildren<SpriteRenderer>().flipX = (horizontal > 0 && offset > 0);
        legs[1].GetComponentInChildren<SpriteRenderer>().flipX = (horizontal > 0 && offset > 0);

        Vector2 movement = new Vector3(horizontal * Time.deltaTime, 0, 0);
        if (movement.x == 0) // отключение скольжения
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            return;
        }
        if (rigidBody.velocity.x > -parameters.Speed && rigidBody.velocity.x < parameters.Speed)
        {
            rigidBody.AddForce(movement.normalized, ForceMode2D.Impulse); // Создает толчок в заданное направление с расчитанной силой
            return;
        }
        rigidBody.velocity = new Vector2(parameters.Speed * horizontal, rigidBody.velocity.y); // ограничение для скорости
    }

    // Отвечает за прыжок
    public void Jump()
    {
        rigidBody.AddForce(transform.up.normalized * parameters.Jump, ForceMode2D.Impulse); // Создает толчок при нажатии кнопки прыжка
    }

    // Возвращает провку на существование пола под игроком
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircleAll(transform.position, .3F, parameters.BlockStay).Length > 0.8;
    }

    // Отвечает за выстрел
    private IEnumerator Shoot()
    {
        isLockShootCoroutine = true; // останавливает возможность вызова до достижения конца корутины
        yield return new WaitForSeconds(weaponAttack.Parameters.BulletDelay); // задержка между пулями

        // Выстрел в направлении мыши
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        weaponAttack.Attack(difference);
        isLockShootCoroutine = false;
    }

    // Отвечает за отнятие жизни у игрока
    public override int ReceiveDamage(int damage)
    {
        // Отвечает за отталкивание в при попадании
        rigidBody.velocity = Vector3.zero;
        rigidBody.AddForce(transform.up * (parameters.Jump / 3), ForceMode2D.Impulse);
        
        // Отвечает за создания партикла крови
        ParticleSystem particle = Instantiate(bloodParticles, transform.position, transform.rotation);
        particle.Play();
        Destroy(particle.gameObject, particle.startLifetime);

        if (HealthPoints - damage <= 0)
        {
            FindObjectOfType<EndGame>().LossCanvas(); // находит объект с EndGame и вызывает LossCanvas при проигрыше
            return 0;
        }
        return HealthPoints -= damage;
    }

    // Устанавливает выбранное оружие из магазина
    public void SetWeapon(Weapon weapon)
    {
        if (SaveParameters.weaponsBought != null)
        {
            foreach (Transform weaponT in hand.GetComponentInChildren<Transform>())
                Destroy(weaponT.gameObject);
            weaponAttack = Instantiate(weapon);
            weaponAttack.transform.parent = hand.transform;
            weaponAttack.transform.localScale = weapon.transform.localScale;
            weaponAttack.transform.localPosition = Vector3.zero;
            weaponAttack.transform.localRotation = Quaternion.identity;
        }
    }

    // Устанавливает курсор прицела
    private void SetAimCursor()
    {
        Cursor.SetCursor(aimCursorTexture, new Vector2(100, 100), CursorMode.Auto);
    }

    // Устанавливает курсор перезарядки
    private void SetReloadCursor()
    {
        Cursor.SetCursor(reloadCursorTexture, Vector2.zero, CursorMode.Auto);
    }

    private void OnDestroy()
    {
        weaponAttack.Reloading -= SetReloadCursor;
        weaponAttack.Reloaded -= SetAimCursor;
    }
}
