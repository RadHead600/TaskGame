using UnityEngine;

// Бонусный ящик
public class BonusBox : Units
{
    [SerializeField] private GameObject[] bonuses;

    private void Start()
    {
        HealthPoints = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponentInChildren<Bullet>();
        if(bullet != null)
            DropBonus();
    }

    // Выпадение случайного бонуса из заданных в инспекторе
    public void DropBonus()
    {
        GameObject dropBonus = Instantiate(bonuses[Random.Range(0, bonuses.Length)].gameObject, transform.position, transform.rotation) as GameObject;
        dropBonus.SetActive(true);
        Destroy(gameObject);  
    }
}
