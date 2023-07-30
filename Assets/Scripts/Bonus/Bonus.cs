using UnityEngine;

public abstract class Bonus : MonoBehaviour
{
    protected Units unit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        unit = collision.GetComponentInChildren<Character>();
        if (unit != null)
            GiveBonus();
    }
    
    protected abstract void GiveBonus();
}
