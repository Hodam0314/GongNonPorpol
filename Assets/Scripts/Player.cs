using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{ 
    Rigidbody2D rigid;
    Camera mainCam;
    BoxCollider2D box;
    Animator anim;

    private float verticalVelocity;
    private bool checkjumping = false;
    private bool isGround = false;
    private float curHp = 0f;

    [Header("�÷��̾� ����")]
    [SerializeField] private float MaxHp = 30f;
    [SerializeField] private float Damage = 5f;
    [SerializeField] private float moveSpeed = 5.0f;
    Vector3 moveDir;

    [Header("�߷� �� ����")]
    private float gravity = 9.81f;
    [SerializeField] private float jumpForce = 5f;
    

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        moving();
        checkjump();
        jumping();
        checkGround();
        turning();

        playAnimation();
    }
    private void turning() //���� , ������ �����̴� Ű �Ҵ����� �������� �����Ͽ� �÷��̾� ĳ���͸� ����������
    {
        if (moveDir.x > 0 && transform.localScale.x < 1)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        else if(moveDir.x < 0 && transform.localScale.x > -1)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void moving() //�÷��̾��� �����̴� �ڵ�
    {
        moveDir.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        moveDir.y = rigid.velocity.y;
        rigid.velocity = moveDir;
    }

    private void checkGround() //�÷��̾ Ÿ������ ���ִ��� üũ���ִ� �ڵ�
    {
        isGround = false;
        RaycastHit2D hit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0f,
            Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
        if(hit.transform != null)
        {
            isGround = true;
            verticalVelocity = 0;
        }
    }

    private void checkjump() //�÷��̾� �������ɻ��¸� üũ , ���� Ű�� �Է¹޴� �ڵ�
    {
        if (Input.GetKey(KeyCode.Space)&& isGround == true)
        {
            checkjumping = true;
            jumping();
        }
    }

    private void jumping() //�÷��̾��� ��������� ��������ִ� �ڵ�
    {
        if (isGround == false)
        {
            verticalVelocity -= gravity * Time.deltaTime;
            if (verticalVelocity < -10)
            {
                verticalVelocity = -10;
            } 
        }
        if(checkjumping == true)
        {
            checkjumping = false;
            verticalVelocity = jumpForce;
        }
        rigid.velocity = new Vector2(rigid.velocity.x, verticalVelocity);
    }
    private void playAnimation() //�÷��̾��� �ִϸ��̼� �ڵ�
    {
        anim.SetBool("isJump", isGround);
        anim.SetInteger("isMoving", (int)moveDir.x);
    }
}
