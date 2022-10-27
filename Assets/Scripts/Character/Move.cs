using System;
using UniRx;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Rigidbody2D rb;

    public int speed;
    private Vector2 Movement = new Vector2();

    private Animator _animator;

    public bool canMove = true;

    public AK.Wwise.Event sound;

    private void Awake()
    {
        Observable.FromEvent<Vector3>(action => Teleport.gameObjectChange += action,
            action => Teleport.gameObjectChange -= action).Subscribe(
            a =>
            {
                gameObject.transform.position = a;
            });
    }

    void Start()
    {
        _animator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.Instance.dialogueManger.isDialogue.Value)
        {
            _animator.Play("Idle");
        }
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