using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/Enemy")]
public class EnemySO : ScriptableObject
{
    public enum State
    {
        Alive,
        Death
    }

    [field: SerializeField]
    public int Health { get; private set; }
    [field: SerializeField]
    public float Speed { get; private set; }
    [field: SerializeField]
    public int Damage { get; private set; }
    [field: SerializeField]
    public int MoneyDrop { get; private set; }
    [field: SerializeField]
    public float Multiplier { get; private set; }
   
}
