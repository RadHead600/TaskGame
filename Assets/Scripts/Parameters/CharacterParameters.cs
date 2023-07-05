using UnityEngine;

[CreateAssetMenu(fileName = "CharacterParameters", menuName = "CustomParameters/CharacterParameters")]
public class CharacterParameters : ScriptableObject
{
    [SerializeField] private float speed;
    [SerializeField] private float jump;
    [SerializeField] private int healthPoints;
    [SerializeField] private LayerMask blockStay;

    public float Speed => speed;
    public float Jump => jump;
    public int HealthPoints => healthPoints;
    public LayerMask BlockStay => blockStay;
}
