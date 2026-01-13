using UnityEngine;

public class ArmyUnit : MonoBehaviour
{   
    public float speed = 3f;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private Vector2 formationOffset = new Vector2(-0.3f, -0.2f);
    [SerializeField] private float stopDistance = 0.2f;
    [SerializeField] General general; 

    [Header("Army Params")]
    public float armySize;
    public float armyStr;
    
    void Start()
    {

    }

    
    void Update()
    {   
        MoveTowardsGeneral();

        if(rb.linearVelocity.x < -0.1f) 
        {
        transform.rotation = Quaternion.Euler(0, 180, 0); 
        }

        else if(rb.linearVelocity.x > 0.1f)
        {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        }   
    }

    private void MoveTowardsGeneral()
    {
        Vector3 targetPos = general.transform.position + (Vector3) formationOffset;
        float distance = Vector3.Distance(transform.position, targetPos);

        if( distance > stopDistance)
        {
           Vector3 direction = (targetPos - transform.position).normalized;
           rb.linearVelocity = direction * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero ;
        }

    }
}
