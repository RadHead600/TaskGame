using System;
using System.Collections;
using UnityEngine;

public class EnemyShoot : Enemy
{
    [SerializeField] private Transform hand;
    [SerializeField] private Transform body;

    private int offset;
    private Vector3 difference;
    private Weapon weapon;

    public void Start()
    {
        if (player == null)
            player = FindObjectOfType<Character>().gameObject;
        HealthPoints = Parameters.HealthPoints;
        weapon = Instantiate(Parameters.Weapon);
        weapon.transform.SetParent(hand);
        weapon.transform.localPosition = Vector3.zero;
        StartCoroutine(Attack());
    }

    private void Update()
    {
        RotateWeapons();
        RotateBody();
    }

    // Поворачивает оружие в сторону направления врага
    private void RotateWeapons()
    {
        difference = player.transform.position - transform.position;
        float rotate = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        hand.transform.rotation = Quaternion.Euler(0f, 0f, rotate + offset);
    }

    // Поворачивается тело в сторону направления врага
    private void RotateBody()
    {
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

    // Стрельба с задержкой между выстрелом
    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(weapon.Parameters.BulletDelay);

        weapon.isEnemy = true;
        weapon.Attack(difference);

        StartCoroutine(Attack());
    }
}