using UnityEngine;
using TMPro;

public class WarGUI : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI army1Size;
    [SerializeField] TMPro.TextMeshProUGUI army2Size;
    [SerializeField] TMPro.TextMeshProUGUI army1Str;
    [SerializeField] TMPro.TextMeshProUGUI army2Str;
    [SerializeField] GameObject menuCanvasPrefab;
    private GameObject currentMenuInstance;
    public float menuOffset;

    void OnEnable()
    {
        GameEvent.OnColliderContactEnter += OpenMenu;
        GameEvent.OnColliderContactExit += CloseMenu;
        GameEvent.OnInBattleTurn += assignTextValues;
    }

    void OnDisable()
    {
        GameEvent.OnColliderContactEnter -= OpenMenu;
        GameEvent.OnColliderContactExit -= CloseMenu;
        GameEvent.OnInBattleTurn -= assignTextValues;
    }

    void assignTextValues(EnemyUnity enemy, General general)
    {
        army1Size.text = enemy.armySize.ToString();
        army2Size.text = general.armySize.ToString();
    
        army1Str.text = enemy.armyStr.ToString();
        army2Str.text = general.armyStr.ToString();
    }

    void OpenMenu(EnemyUnity enemy, General general)
    {
        if (currentMenuInstance == null)
        {
            currentMenuInstance = Instantiate(menuCanvasPrefab);

            Vector3 enemyPos = enemy.transform.position;
            Vector3 generalPos = general.transform.position;
            Vector3 midPoint = (enemyPos + generalPos) / 2;
            midPoint.y += menuOffset; 
            currentMenuInstance.transform.position = midPoint;
            currentMenuInstance.transform.position = new Vector3(midPoint.x, midPoint.y, 0);
        }

    }

    void CloseMenu(EnemyUnity enemy, General general)
    {

        if (currentMenuInstance != null)
        {
            Destroy(currentMenuInstance);
            currentMenuInstance = null; 
        }
    }
}