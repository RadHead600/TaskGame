using UnityEngine;

public class Tavern : MonoBehaviour
{
    [SerializeField] private GameObject shopCanvas;

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
