using UnityEngine;
using TMPro;

public class WarGUI : MonoBehaviour
{
    [SerializeField] GameObject menuCanvasPrefab;
    private GameObject currentMenuInstance;
    private WarMenuRefs currentMenuRefs; 
    public float menuOffset;

    void OnEnable()
    {
        GameEvent.OnColliderContactEnter += OpenMenu;
        GameEvent.OnColliderContactExit += CloseMenu;
        GameEvent.OnInBattleTurn += UpdateValues; 
    }

    void OnDisable()
    {
        GameEvent.OnColliderContactEnter -= OpenMenu;
        GameEvent.OnColliderContactExit -= CloseMenu;
        GameEvent.OnInBattleTurn -= UpdateValues;
    }

    void OpenMenu(EnemyUnity enemy, General general)
    {
        if (currentMenuInstance == null)
        {
            currentMenuInstance = Instantiate(menuCanvasPrefab);
            
            Vector3 midPoint = (enemy.transform.position + general.transform.position) / 2;
            midPoint.y += menuOffset;
            currentMenuInstance.transform.position = midPoint;

            currentMenuRefs = currentMenuInstance.GetComponent<WarMenuRefs>();
            
            // İlk açılışta her şey 0 gözüksün
            UpdateValues(enemy, general, 0, 0, 0, 0, 0);
        }
    }

    // Parametreleri Event'e uygun olarak güncelledik
    void UpdateValues(EnemyUnity enemy, General general, float eLoss, float gLoss, int eRoll, int gRoll, int turn)
    {
        if (currentMenuRefs != null)
        {
            // Ordu Büyüklükleri (Mevcut)
            currentMenuRefs.army1Size.text = enemy.armySize.ToString("F0");
            currentMenuRefs.army2Size.text = general.armySize.ToString("F0");
            currentMenuRefs.army1Str.text = enemy.armyStr.ToString();
            currentMenuRefs.army2Str.text = general.armyStr.ToString();

            // YENİLER: Kayıplar (Kırmızı renk güzel olur)
            currentMenuRefs.army1Deaths.text = "-" + eLoss.ToString("F0");
            currentMenuRefs.army2Deaths.text = "-" + gLoss.ToString("F0");

            // YENİLER: Zarlar
            currentMenuRefs.army1Dice.text = eRoll.ToString();
            currentMenuRefs.army2Dice.text = gRoll.ToString();

            // YENİLER: Tur Sayısı
            currentMenuRefs.turnCount.text =  turn.ToString();
        }
    }

    void CloseMenu(EnemyUnity enemy, General general)
    {
        if (currentMenuInstance != null)
        {
            Destroy(currentMenuInstance);
            currentMenuInstance = null;
            currentMenuRefs = null;
        }
    }
}