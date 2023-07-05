using System;
using UnityEngine;

[Serializable] // ƒанный класс отвечает за ScriptableObject'ы. ƒл€ создани€ нового врага, требуетс€ создать класс-наследник и создать ScriptableObject в Unity. ƒалее настроить и прив€зать ScriptableObject к новому врагу.
public class EnemyParameters : ScriptableObject
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private int healthPoints;

    public Weapon Weapon => weapon;
    public int HealthPoints => healthPoints;
}
