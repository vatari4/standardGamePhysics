 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.0F;
    [SerializeField]
    private float jumpForce = 10.0F;

    private bool isGrounded = false;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;


    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }


    private void FixedUpdate()
    {
        CheckGround();
    }

    void Update()
    {
        if (isGrounded) State = States.idle;
        if (Input.GetButton("Horizontal")) Run();
        if (isGrounded && Input.GetButtonDown("Jump")) Jump();
    }
    private void Run()
    {
        if (isGrounded) State = States.run;

        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
        
    }
    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);
        isGrounded = colliders.Length > 1;

        if (!isGrounded) State = States.jump;
    }

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    } 
}
public class CameraConroller : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private Vector3 pos;

    private void Awake() 
    {
        if (!player)
            player = FindObjectOfType<Character>().tranform;

    }
    private void Update() 
    {
        pos = player.position;

            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime); //lerp - delaet dvijenie plavnim
    }

}

public enum States 
{ 
    idle,
    run,
    jump
}
