using UnityEngine;

// Пуля
public class Bullet : MonoBehaviour
{
    [SerializeField] private LayerMask blocks;

    public float Speed { get; set; }
    public int Damage { get; set; }
    public Vector3 Direction { get; set; }

    private void Start()
    {
        Destroy(gameObject, 4); // Уничтожение пули при "жизни" в 4 секунды
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Direction, Speed * Time.deltaTime); // полет в заданном направлении
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        Units unit = collider.GetComponentInChildren<Units>();
        // Нанесение урона в случае столкновения с Unit'ом
        if (unit != null)
        {
            unit.ReceiveDamage(Damage);
            Destroy(gameObject);
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f, blocks);
        // Уничтожение пули при столкновении с заданными слоями
        if (colliders.Length > 0.8F)
        {
            Destroy(gameObject);
        }
    }
}
