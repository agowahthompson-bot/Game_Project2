using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Components
    private Rigidbody2D rb;
    private Animator ani;
    
    //Movement
    public float moveSpeed = 7f;
    public float jumpforce = 12f;
    private Vector2 direction = Vector2.zero;
    private Vector2 velocity = Vector2.zero;
    private bool jump_pressed = false;
    private bool is_grounded = true;
    
    void Start()
    {
            //Get Components
            rb = GetComponent<Rigidbody2D>();
            ani = GetComponent<Animator>();
    }

    void Update()
{
        //Jump Input
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            jump_pressed = true;
        }

        //Flip Sprite Scale based on Input
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(0.3f, 0.3f, 1f);
        }
        else if (direction.x < 0)
        {
           transform.localScale = new Vector3(-0.3f, 0.3f, 1f);
        }
    
        // Animator
        if (ani != null)
        {
            // Update Animator parameters
            ani.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
            ani.SetBool("IsJumping", !is_grounded);

            // Ducking input (Down Arrow or S)
            bool isDucking = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
            ani.SetBool("IsDucking", isDucking);
        }
    }
    void FixedUpdate()
    {
        // Left/Right Input
        direction.x = Input.GetAxisRaw("Horizontal");
        Debug.Log("Horizontal: " + direction.x);
        direction.y = 0f;
        direction = direction.normalized;

        //Construct Velocity Vector
        velocity = direction * moveSpeed;

        //Move Horizontal
        rb.linearVelocity = new Vector2(velocity.x, rb.linearVelocity.y);

        //Jump
        if (jump_pressed && is_grounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpforce);
            is_grounded = false;
        }

        jump_pressed = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string obj_name = collision.gameObject.name.ToLower();
        if (obj_name.Contains("floor") || obj_name.Contains("platform"))
        {
            is_grounded = true;
        }
    }
}