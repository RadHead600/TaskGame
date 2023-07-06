using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI amountMoney;
    [SerializeField] private List<Weapon> weapons;
    [SerializeField] private ShopWeaponButton weaponButton;
    [SerializeField] private GameObject weaponsPanel;
    [SerializeField] private Character character;

    private void Awake()
    {
        SetStaticParameters();
        amountMoney.text = SaveParameters.money.ToString();
        CreateWeaponPanels();
        UnlockWeapons();
        gameObject.SetActive(false);
    }

    private void Start()
    {
        Equip(SaveParameters.weaponEquip);
    }

    // При старте игры установить все значения на стандартные
    private void SetStaticParameters()
    {
        int levelCount = SceneManager.sceneCountInBuildSettings;
        SaveParameters.numberKilled = new int[levelCount];
        if (SaveParameters.weaponEquip == 0)
        {
            SaveParameters.weaponsBought = new Weapon[weapons.Count()];
            SaveParameters.weaponsBought[0] = weapons[0];
        }
    }

    // Добавляет панель с оружием в панель в окне
    private void CreateWeaponPanels()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            int saveI = i;
            ShopWeaponButton button = Instantiate(weaponButton);
            button.transform.SetParent(weaponsPanel.transform);
            button.transform.position = new Vector3(button.transform.position.x, button.transform.position.y, 0);
            button.transform.localScale = Vector3.one;
            button.Button.onClick.AddListener(() => Equip(saveI));
            button.WeaponImage.sprite = weapons[i].Parameters.WeaponSprite;
            button.CostText.text = weapons[i].Parameters.Cost.ToString();
            button.NameText.text = weapons[i].Parameters.WeaponName.ToString();
        }
    }

    // Функция экипирования оружия на игрока
    public void Equip(int weaponNum)
    {
        if (SaveParameters.weaponsBought[weaponNum] != null)
        {
            SaveParameters.weaponEquip = weaponNum;
            for (int i = 0; i < SaveParameters.weaponsBought.Length; i++) 
            {
                if (SaveParameters.weaponsBought[i] != null)
                    weaponsPanel.GetComponentsInChildren<ShopWeaponButton>()[i].WeaponImage.color = Color.white;
            }
            weaponsPanel.GetComponentsInChildren<ShopWeaponButton>()[weaponNum].WeaponImage.color = Color.green;
            character.SetWeapon(SaveParameters.weaponsBought[weaponNum]);
            return;
        }
        BuyWeapon(weaponNum);
    }

    // Функция разблокирования открытого оружия в магазине
    private void UnlockWeapons()
    {
        int i = 0;
        foreach (var weaponBought in SaveParameters.weaponsBought)
        {
            if (weaponBought == null)
                continue;
            foreach (var weapon in weapons)
            {
                if (string.Compare(weaponBought.Parameters.WeaponName, weapon.Parameters.WeaponName) == 0)
                {
                    UnlockWeapon(i);
                }
                i++;
            }
            i = 0;
        }
    }

    // разблокирует заданное оружие
    private void UnlockWeapon(int buttonNum)
    {
        weaponsPanel.GetComponentsInChildren<ShopWeaponButton>()[buttonNum].WeaponImage.material = null;
        weaponsPanel.GetComponentsInChildren<ShopWeaponButton>()[buttonNum].CostText.alpha = 0;
    }

    // Функция покупки оружия
    private void BuyWeapon(int weaponNum)
    {
        if (SaveParameters.money < weapons[weaponNum].Parameters.Cost)
            return;

        weaponsPanel.GetComponentsInChildren<ShopWeaponButton>()[weaponNum].WeaponImage.material = null;
        weaponsPanel.GetComponentsInChildren<ShopWeaponButton>()[weaponNum].CostText.alpha = 0;
        SaveParameters.weaponsBought[weaponNum] = weapons[weaponNum]; // добавления купленного оружия в заранее созданный пулл объектов
        SaveParameters.money -= weapons[weaponNum].Parameters.Cost;
        amountMoney.text = SaveParameters.money.ToString();
    }
}
