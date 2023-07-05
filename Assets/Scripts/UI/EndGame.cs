using TMPro;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private DropParameters dropParameters;
    [SerializeField] private GameObject menuEndGame;
    [SerializeField] private GameObject lossMenu;
    [SerializeField] private TextMeshProUGUI textMurders;
    [SerializeField] private TextMeshProUGUI textMoney;

    void Start()
    {
        menuEndGame.SetActive(false);
        lossMenu.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Units unit = collision.GetComponent<Character>();
        if (unit == null)
            return;
        // При достижении триггера конца уровня, открывается окно конца уровня и демонстрируется текст с количеством убийств и монет.
        Time.timeScale = 0;
        menuEndGame.SetActive(true);

        textMurders.text = "Убийств: " + SaveParameters.numberKilled[SaveParameters.levelActive].ToString();
        textMoney.text = (SaveParameters.numberOfCoinsRaised * dropParameters.MoneyForCoin).ToString();
    }

    // Открывает окно при проигрыше
    public void LossCanvas()
    {
        Time.timeScale = 0;
        lossMenu.SetActive(true);
    }
}
