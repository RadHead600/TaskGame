using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponParameters parameters;
    [SerializeField] private GameObject posAttack;

    [NonSerialized] public bool isEnemy = false;
    
    private float timerRecharge;
    private int amountBulletsInMagazine;

    public int AmountBulletsInMagazine => amountBulletsInMagazine;
    public WeaponParameters Parameters => parameters;

    private void Start()
    {
        timerRecharge = parameters.RechargeTime;
        amountBulletsInMagazine = parameters.AmountBulletsInMagazine;
    }

    private void Update()
    {
        if(timerRecharge > 0)
            timerRecharge -= Time.deltaTime;
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
    public int TakeAwayBullet(int quantity)
    {
        if (amountBulletsInMagazine - quantity < 0)
        {
            timerRecharge = parameters.RechargeTime;
            amountBulletsInMagazine = parameters.AmountBulletsInMagazine;
        }
        return amountBulletsInMagazine -= quantity;
    }
}
