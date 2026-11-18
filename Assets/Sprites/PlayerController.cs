using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Components
    private Rigidbody2D rb;
    private Animator;
    
    //Movements
    public float moveSpeed = 7f;
    public float moveSpeed = 12f;
    private Vector2 direction = Vector2 zero;
    private Vector2 velocity = Vector2 zero;
    private bool isGrounded;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>;
    }

    //Left/Right

    // Update is called once per frame
    void Update()
    {
        
    }
}
