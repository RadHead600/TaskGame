using TMPro;
using UnityEngine;
using UnityEngine.UI;

//  ласс отвечающий за панель оружи€ в окне магазина
public class ShopWeaponButton : MonoBehaviour
{ 
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image weaponImage;

    public Button Button => button;
    public TextMeshProUGUI CostText => costText;
    public TextMeshProUGUI NameText => nameText;
    public Image WeaponImage => weaponImage;
}
