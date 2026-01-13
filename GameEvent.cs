using UnityEngine;
using System;
public class GameEvent : MonoBehaviour
{
    
    public static event Action<EnemyUnity, General> OnColliderContactEnter;
    public static event Action<EnemyUnity, General> OnColliderContactExit;

    public static event Action<EnemyUnity, General, float, float, int, int, int> OnInBattleTurn;

    public static void InBattleTurn(EnemyUnity enemy, General general, float eLoss, float gLoss, int eRoll, int gRoll, int turn)
    {
        OnInBattleTurn?.Invoke(enemy, general, eLoss, gLoss, eRoll, gRoll, turn);
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