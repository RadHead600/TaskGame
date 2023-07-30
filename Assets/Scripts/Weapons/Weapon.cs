using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponParameters parameters;
    [SerializeField] private GameObject posAttack;

    [NonSerialized] public bool isEnemy = false;
    
    private float _timerRecharge;
    private int _amountBulletsInMagazine;

    public delegate void ReloadingDelegate();
    public event ReloadingDelegate Reloading;
    public delegate void ReloadedDelegate();
    public event ReloadedDelegate Reloaded;

    public WeaponParameters Parameters => parameters;
    public int AmountBulletsInMagazine => _amountBulletsInMagazine;
    public int AmountBulletsInMagazineStandart => parameters.AmountBulletsInMagazine;

    private void Start()
    {
        _timerRecharge = parameters.RechargeTime;
        _amountBulletsInMagazine = parameters.AmountBulletsInMagazine;
    }

    private void Update()
    {
        if(_timerRecharge > 0)
            _timerRecharge -= Time.deltaTime;
        else if (_amountBulletsInMagazine == 0)
        {
            if (Reloaded != null)
                Reloaded.Invoke();
            _amountBulletsInMagazine = parameters.AmountBulletsInMagazine;
        }
    }

    public void Attack(Vector3 difference)
    {
        if (_timerRecharge > 0 || posAttack == null)
            return;

        Bullet newBullet = Instantiate(parameters.Bullet, posAttack.transform.position, posAttack.transform.rotation);
        if (isEnemy)
            newBullet.gameObject.layer = LayerMask.NameToLayer("BulletEnemy");

        newBullet.Speed = parameters.BulletSpeed;
        newBullet.Damage = parameters.BulletDamage;
        newBullet.Direction = newBullet.transform.right * (difference.x < 0 ? -1 : 1);

        TakeAwayBullet(1);
    }

    public void TakeAwayBullet(int quantity)
    {
        _amountBulletsInMagazine -= quantity;
        if (_amountBulletsInMagazine <= 0)
        {
            if (Reloading != null)
                Reloading.Invoke();
            _timerRecharge = parameters.RechargeTime;
            return;
        }
    }
}
