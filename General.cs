using UnityEngine;
using UnityEngine.InputSystem;

public class General : MonoBehaviour
{
    public float speed = 5f;
    private float _horizontalMovement ;
    private float _verticalMovement ;

    private Vector2 moveAction;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D rb;

    
    [Header("Army Params")]
    public float armySize;
    public float armyStr;
    
    public void Move(InputAction.CallbackContext context)
    {
        _horizontalMovement = context.ReadValue<Vector2>().x;
        _verticalMovement = context.ReadValue<Vector2>().y;
    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(_horizontalMovement * speed * (-1) , _verticalMovement * speed );

        if(rb.linearVelocity.x < -0.1f) 
        {
        transform.rotation = Quaternion.Euler(0, 180, 0); 
        }

        else if(rb.linearVelocity.x > 0.1f)
        {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        }   
    }

    public Vector2 GetGeneralMovement()
    {
        return rb.linearVelocity;
    }


}
