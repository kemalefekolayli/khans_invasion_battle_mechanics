using UnityEngine;

public class EnemyUnity : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PolygonCollider2D collider2d;

    [Header("Army Params")]
    public float armySize;
    public float armyStr;

    void OnTriggerEnter2D(Collider2D other)
    {

        General generalComponent = other.GetComponent<General>();

        // Eğer çarptığımız şey bir General ise (null değilse)
        if (generalComponent != null)
        {
            GameEvent.ColliderContactEnter(this, generalComponent);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        General generalComponent = other.GetComponent<General>();

        if (generalComponent != null)
        {
            GameEvent.ColliderContactExit(this, generalComponent);
        }
    }
}