using System;
using System.Collections;
using UnityEngine;

public class EnemyShoot : Enemy
{
    [SerializeField] private Transform hand;
    [SerializeField] private Transform body;

    private int _offset;
    private Vector3 _difference;
    private Weapon _weapon;

    public void Start()
    {
        if (player == null)
            player = FindObjectOfType<Character>().gameObject;
        HealthPoints = Parameters.HealthPoints;
        _weapon = Instantiate(Parameters.Weapon);
        _weapon.transform.SetParent(hand);
        _weapon.transform.localPosition = Vector3.zero;
        StartCoroutine(Attack());
    }

    private void Update()
    {
        RotateWeapons();
        RotateBody();
    }

    private void RotateWeapons()
    {
        _difference = player.transform.position - transform.position;
        float rotate = Mathf.Atan2(_difference.y, _difference.x) * Mathf.Rad2Deg;
        hand.transform.rotation = Quaternion.Euler(0f, 0f, rotate + _offset);
    }

    private void RotateBody()
    {
        Vector3 pos = body.transform.localScale;
        body.transform.localScale = new Vector3(
            (_difference.x < 0 ? Math.Abs(pos.x) * -1 : Math.Abs(pos.x)) * (transform.localScale.x > 0 ? -1 : 1),
            pos.y,
            pos.z
            );

        if (pos.x > 0)
        {
            _offset = -180;
            if (transform.localScale.x < 0)
                _offset = 0;
            return;
        }
        _offset = 0;
        if (transform.localScale.x < 0)
            _offset = -180;
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(_weapon.Parameters.BulletDelay);

        _weapon.isEnemy = true;
        _weapon.Attack(_difference);

        StartCoroutine(Attack());
    }
}