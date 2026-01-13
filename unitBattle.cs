using UnityEngine;
using System.Collections;

public class unitBattle : MonoBehaviour
{
    private EnemyUnity currentEnemy;
    private General currentGeneral;
    private Coroutine battleCoroutine; 

    // Kaçıncı turda olduğumuzu saymak için
    private int currentTurnCount = 0;

    void OnEnable()
    {
        GameEvent.OnColliderContactEnter += StartBattle;
        GameEvent.OnColliderContactExit += StopBattle;
    }

    void OnDisable()
    {
        GameEvent.OnColliderContactEnter -= StartBattle;
        GameEvent.OnColliderContactExit -= StopBattle;
    }

    void StartBattle(EnemyUnity enemyUnity, General generalComponent)
    {
        currentEnemy = enemyUnity;
        currentGeneral = generalComponent;
        currentTurnCount = 0; // Savaş başlayınca sayacı sıfırla

        if (battleCoroutine != null) StopCoroutine(battleCoroutine);
        battleCoroutine = StartCoroutine(BattleRoutine());
    }

    void StopBattle(EnemyUnity enemy, General general)
    {
        if (battleCoroutine != null)
        {
            StopCoroutine(battleCoroutine);
            battleCoroutine = null;
        }
    }

    IEnumerator BattleRoutine()
    {
        while (currentEnemy.armySize > 10 && currentGeneral.armySize > 10)
        {
            currentTurnCount++; // Turu arttır

            // 1. Zarları BURADA atıyoruz ki değerleri görebilelim
            int enemyRoll = RollDice();
            int generalRoll = RollDice();

            // 2. Saldırıyı yap (Zarları parametre olarak gönderiyoruz)
            PerformAttack(enemyRoll, generalRoll);

            // 3. Bekle
            yield return new WaitForSeconds(1.0f); 
        }

        if (currentEnemy.armySize <= 10) GeneralWin();
        else EnemyWin();
        
        battleCoroutine = null;
    }

    // Artık zarları dışarıdan alıyor
    void PerformAttack(int eRoll, int gRoll)
    {
        // Zarları UI'da göstermek için hesaplamayı burada yapıyoruz
        float generalPower = currentGeneral.armySize * currentGeneral.armyStr * gRoll;
        float enemyPower = currentEnemy.armySize * currentEnemy.armyStr * eRoll;

        float enemyLosses = 0;
        float generalLosses = 0;

        if (generalPower > enemyPower)
        {
            enemyLosses = currentEnemy.armySize * 0.3f;
            generalLosses = currentGeneral.armySize * 0.1f;
        }
        else if (enemyPower > generalPower)
        {
            enemyLosses = currentEnemy.armySize * 0.1f;
            generalLosses = currentGeneral.armySize * 0.3f;
        }
        else
        {
            enemyLosses = currentEnemy.armySize * 0.2f;
            generalLosses = currentGeneral.armySize * 0.2f;
        }

        currentEnemy.armySize -= enemyLosses;
        currentGeneral.armySize -= generalLosses;

        // GÜNCELLEME: Tüm detayları UI'ya gönderiyoruz
        GameEvent.InBattleTurn(currentEnemy, currentGeneral, enemyLosses, generalLosses, eRoll, gRoll, currentTurnCount);
    }

    int RollDice()
    {
        return UnityEngine.Random.Range(1, 7);
    }

    void GeneralWin() { Debug.Log("General Wins!"); }
    void EnemyWin() { Debug.Log("Enemy Wins!"); }
}