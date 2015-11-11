using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 5f;
    public float jumpSpeed = 5f;

    private bool grounded = false;
    private bool jumping = false;
    private bool facingRight = true;
    private Vector2 inputSpeed = Vector2.zero;
    private Animator anim;
    private Rigidbody2D rb2d;


    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, transform.position + Vector3.down * 0.25f, 1 << LayerMask.NameToLayer("Ground"));
        inputSpeed.x = Input.GetAxis("Horizontal");
        inputSpeed.y = Input.GetAxis("Vertical");

        if (grounded && inputSpeed.y > 0) {
            jumping = true;
        }
    }

    void FixedUpdate()
    {
        if ((inputSpeed.x > 0 && !facingRight) || (inputSpeed.x < 0 && facingRight)) {
            Flip();
        }

        Vector3 newVelocity = rb2d.velocity;
        newVelocity.x = inputSpeed.x * moveSpeed;

        if (jumping) {
            //anim.SetTrigger("Jump");
            newVelocity.y = jumpSpeed;
            jumping = false;
        }

        rb2d.velocity = newVelocity;

        anim.speed = Mathf.Abs(inputSpeed.x);
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}