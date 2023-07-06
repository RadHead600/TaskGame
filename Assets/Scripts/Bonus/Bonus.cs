using UnityEngine;

public abstract class Bonus : MonoBehaviour
{
    protected Units unit;

    // При столкновении с бонусом вызывается метод, который отвечает за логику бонуса и переопредлеяется в наследниках
    private void OnTriggerEnter2D(Collider2D collision)
    {
        unit = collision.GetComponentInChildren<Character>();
        if (unit != null)
            GiveBonus();
    }
    
    // Логика бонуса при столкновении с игроком
    protected abstract void GiveBonus();
}
