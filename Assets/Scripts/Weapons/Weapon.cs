using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponParameters parameters;
    [SerializeField] private GameObject posAttack;

    [NonSerialized] public bool isEnemy = false;
    
    private float timerRecharge;
    private int amountBulletsInMagazine;

    public delegate void ReloadingDelegate();
    public event ReloadingDelegate Reloading;
    public delegate void ReloadedDelegate();
    public event ReloadedDelegate Reloaded;

    public WeaponParameters Parameters => parameters;
    public int AmountBulletsInMagazine => amountBulletsInMagazine;
    public int AmountBulletsInMagazineStandart => parameters.AmountBulletsInMagazine;

    private void Start()
    {
        timerRecharge = parameters.RechargeTime;
        amountBulletsInMagazine = parameters.AmountBulletsInMagazine;
    }

    private void Update()
    {
        if(timerRecharge > 0)
            timerRecharge -= Time.deltaTime;
        else if (amountBulletsInMagazine == 0)
        {
            if (Reloaded != null)
                Reloaded.Invoke();
            amountBulletsInMagazine = parameters.AmountBulletsInMagazine;
        }
    }

    // Функция стрельбы оружия
    public void Attack(Vector3 difference)
    {
        if (timerRecharge > 0 || posAttack == null)
            return;

        Bullet newBullet = Instantiate(parameters.Bullet, posAttack.transform.position, posAttack.transform.rotation); // Создание пули на заданной точки у оружия
        if (isEnemy)
            newBullet.gameObject.layer = LayerMask.NameToLayer("BulletEnemy"); // если стреляет враг, то установить слой пули врага (игнорирование слоев настроено в Build Settings)

        newBullet.Speed = parameters.BulletSpeed;
        newBullet.Damage = parameters.BulletDamage;
        newBullet.Direction = newBullet.transform.right * (difference.x < 0 ? -1 : 1);

        TakeAwayBullet(1);
    }

    // Отнять пулю из магазина, в случае, если магазин пуст, то начать перезарядку
    public void TakeAwayBullet(int quantity)
    {
        amountBulletsInMagazine -= quantity;
        if (amountBulletsInMagazine <= 0)
        {
            if (Reloading != null)
                Reloading.Invoke();
            timerRecharge = parameters.RechargeTime;
            return;
        }
    }
}
