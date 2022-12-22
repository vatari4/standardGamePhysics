
public class Character : MonoBehaviour
{
[SerializeField]
private float speed = 3.0F;
[SerializeField]
private float jumpForce = 10.0F;

private bool isGrounded = false;

private Rigidbody2D rb;
private SpriteRenderer sprite;


private void Awake() 
{
    rb = GetComponent<Rigidbody2D>();
    sprite = GetComponentInChildren<SpriteRenderer>();
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
    if (Input.GetButton("Horizontal")) Run();
    if (isGrounded && Input.GetButtonDown("Jump")) Jump();
}
private void Run()
{
    Vector3 dir = transform.right * Input.GetAxis("Horizontal");
    transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
    sprite.flipX = dir.x < 0.0f;

}
private void CheckGround()
{
    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);
    isGrounded = colliders.Length > 1;
}
}

