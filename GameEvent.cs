using UnityEngine;
using System;
public class GameEvent : MonoBehaviour
{
    
    public static event Action<EnemyUnity, General> OnColliderContactEnter;
    public static event Action<EnemyUnity, General> OnColliderContactExit;

    public static event Action<EnemyUnity, General> OnInBattleTurn;

    public static void InBattleTurn(EnemyUnity enemy, General general)
    {
        Debug.Log("event fired for in battle turn");
        OnInBattleTurn?.Invoke(enemy, general);
    }
    public static void ColliderContactEnter(EnemyUnity enemyUnity, General general)
    {
        Debug.Log("event fired for enter!");
        OnColliderContactEnter?.Invoke(enemyUnity, general);
    }

    public static void ColliderContactExit(EnemyUnity enemyUnity, General general)
    {
        Debug.Log("event fired for exit!");
        OnColliderContactExit?.Invoke(enemyUnity, general);
    }
    
}