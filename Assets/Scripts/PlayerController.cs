using UnityEngine;
using System.Collections;

public class PlayerController :Unit
{
    [SerializeField]
    private float speed= 3.0F;
    [SerializeField]
    private int lives = 5;
    [SerializeField]
    private float jumpForce = 15.0F;

    private Rigidbody2D rb2d;
    private Animator animator;
    private SpriteRenderer sprite;
    private bool isGrounded = false;

    public GameObject bullet;

    //private Bullet bullet;//нужно хранить ссылку на префаб bullet

    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
       // bullet = Resources.Load<Bullet>("bullet");//сцена загрузится, сразу подгрузили ссылку на префаб
    }



    private void Update()
    {
       if(isGrounded) State = CharState.idle;
        if (Input.GetButtonDown("Fire1")) Shoot();
        if (Input.GetButton("Horizontal")) Run();
        if (isGrounded && Input.GetButtonDown("Jump")) Jump();
    }

    private void Shoot()
    {
        Vector3 position = transform.position; position.y += 1.0F;
       GameObject newBullet= Instantiate(bullet, position, bullet.transform.rotation) as GameObject;
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        newBullet.transform.position = Vector3.MoveTowards(position, position + direction, 10.0F * Time.deltaTime);//newBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F);
    }

    private void FixedUpdate()
    {
        CheckGround();
    }
    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        sprite.flipX = direction.x < 0.0F;
        if (isGrounded) State = CharState.run;
    }

    private void Jump()
    {
       
        rb2d.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2F);
        isGrounded = colliders.Length > 1;
        if(!isGrounded) State = CharState.jump;
    }
}
public enum CharState
{
    idle,
    run,
    jump
}