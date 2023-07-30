using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private LayerMask blocks;

    public float Speed { get; set; }
    public int Damage { get; set; }
    public Vector3 Direction { get; set; }

    private void Start()
    {
        Destroy(gameObject, 4);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Direction, Speed * Time.deltaTime);
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        Units unit = collider.GetComponentInChildren<Units>();
        if (unit != null)
        {
            unit.ReceiveDamage(Damage);
            Destroy(gameObject);
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f, blocks);
        if (colliders.Length > 0.8F)
        {
            Destroy(gameObject);
        }
    }
}
