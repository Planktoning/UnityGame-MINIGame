using UnityEngine;

public class Move : MonoBehaviour
{
    public Rigidbody2D rb;

    public int speed;
    private Vector2 Movement = new Vector2();

    private Animator _animator;

    public bool canMove = true;

    public AK.Wwise.Event sound;

    void Start()
    {
        _animator = this.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (canMove)
            MoveController();
    }

    void MoveController()
    {
        Movement.x = Input.GetAxis("Horizontal");
        Movement.y = Input.GetAxis("Vertical");
        var a = Input.GetAxisRaw("Horizontal"); //控制角色朝向
        var b = Input.GetAxisRaw("Vertical"); //控制角色朝向

        rb.MovePosition(rb.position + Movement * speed * Time.deltaTime);

        if (a < 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (a > 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }

        _animator.SetFloat("Speed", Mathf.Abs(Movement.x + Movement.y)); //控制角色行走动画


        // this.transform.localScale = new Vector3(, 1, 1);
    }

    void FootStep()
    {
        sound.Post(gameObject);
    }
}