using UnityEngine;

public class BonusHpUp : Bonus
{
    [SerializeField] private int hpAdd;

    protected override void GiveBonus()
    {
        unit.GetComponentInChildren<Character>().HealthPoints += hpAdd;
        Destroy(gameObject);
    }
}
