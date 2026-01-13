using UnityEngine;
using System;
public class unitBattle : MonoBehaviour
{
    private EnemyUnity enemy;
    private General general;

    private float enemyLosses;
    private float generalLosses;

    void OnEnable()
    {
        GameEvent.OnColliderContactEnter += StartBattle;
        GameEvent.OnInBattleTurn += BattleTurn;
    }

    void OnDisable()
    {
        GameEvent.OnColliderContactEnter -= StartBattle;
        GameEvent.OnInBattleTurn -= BattleTurn;
    }

    void StartBattle(EnemyUnity enemyUnity, General generalComponent)
    {
        Debug.Log("Battle Started!");
        enemy = enemyUnity;
        general = generalComponent;

        DecideBattle();
    }
    void DecideBattle()
    {
        if( enemy.armySize < 10 && general.armySize < 10)
        {
            if(RollDice() > 3)
            {
                GeneralWin();
            } else
            {
                EnemyWin();
            }
        }
        else if ( enemy.armySize < 10 && general.armySize > 10 )
        {
            GeneralWin();
        }
        else if ( enemy.armySize > 10 && general.armySize < 10)
        {
            EnemyWin(); 
        }
        else
        {
            BattleTurn();
        }
    }

    void BattleTurn(EnemyUnity enemyUnity, General generalComponent)
    {
        BattleTurn();
        Debug.Log("Battle Turn Executed!");
    }
    void BattleTurn()
    {
        float generalPower = general.armySize * general.armyStr * RollDice();
        float enemyPower = enemy.armySize * enemy.armyStr * RollDice();

        if ( generalPower > enemyPower)
        {
            enemyLosses = enemy.armySize * 0.3f;
            generalLosses = general.armySize * 0.1f;
        }
        else if ( enemyPower > generalPower)
        {
            enemyLosses = enemy.armySize * 0.1f;
            generalLosses = general.armySize * 0.3f;
        }
        else
        {
            enemyLosses = enemy.armySize * 0.2f;
            generalLosses = general.armySize * 0.2f;
        }

        enemy.armySize -= enemyLosses;
        general.armySize -= generalLosses;

        GameEvent.InBattleTurn(enemy, general);
    }

    int RollDice()
    {
    int zar = UnityEngine.Random.Range(1, 7);
    return zar;
    }

    void GeneralWin()
    {
        Debug.Log("General Wins the battle!");
    }

    void EnemyWin()
    {
        Debug.Log("Enemy Wins the battle!");
    }
}