using UnityEngine;

// Таверна (магазин) на уровне
public class Tavern : MonoBehaviour
{
    [SerializeField] private GameObject shopCanvas;

    // Если игрок находится у таверны и зажимает Е, то открыть окно магазина
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (collision.GetComponentInChildren<Character>() != null)
            {
                shopCanvas.SetActive(true);
            }
        }
    }
}
