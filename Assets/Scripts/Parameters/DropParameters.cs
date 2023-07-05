using UnityEngine;

[CreateAssetMenu(fileName = "DropParameters", menuName = "CustomParameters/DropParameters")]
public class DropParameters : ScriptableObject
{
    [SerializeField] private GameObject coin;
    [SerializeField] private int moneyForCoin;

    public GameObject Coin => coin;
    public int MoneyForCoin => moneyForCoin;
}
