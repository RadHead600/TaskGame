using UnityEngine;

public abstract class Units : MonoBehaviour
{
    public int HealthPoints { get; set; }

    // Отвечает за нанесение урона юниту, переопределяется в наследниках
    public virtual int ReceiveDamage(int damage)
    {
        return 0;
    }

    // Отвечает за логику при смерти, перепределяется в наследниках
    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
