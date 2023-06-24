using UnityEngine;

[CreateAssetMenu(fileName = "TowerScriptableObject", menuName = "ScriptableObjects/Towers")]
public class TowerSO : ScriptableObject
{
    [field: SerializeField]
    public int Range { get; private set; }
    [field: SerializeField]
    public int Damage { get; private set; }
    [field: SerializeField]
    public float FireRate { get; private set; }
    [field: SerializeField]
    public int Price { get; private set; }
    [field: SerializeField]
    public float Multiplier { get; private set; }
    [field: SerializeField]
    public GameObject ProjectilePregabe { get; private set; }
}
