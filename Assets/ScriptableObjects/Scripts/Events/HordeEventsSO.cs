using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "HordeEventsScriptableObject", menuName = "ScriptableObjects/HordeEvents")]

public class HordeEventsSO : ScriptableObject
{
    public Action OnEnemySpawned;
    public Action OnEnemyDestroyed;
}
