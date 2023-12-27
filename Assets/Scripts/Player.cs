using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{ 
    Rigidbody2D rigid;
    Camera mainCam;
    BoxCollider2D box;

    private float verticalVelocity;
    private bool checkjumping = false;
    private bool isGround = false;

    [Header("플레이어 관련")]
    [SerializeField] private float moveSpeed = 5.0f;
    Vector3 moveDir;

    [Header("중력 및 점프")]
    private float gravity = 9.81f;
    [SerializeField] private float jumpForce = 5f;
    

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
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
    }
    private void turning()
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

    private void moving()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        moveDir.y = rigid.velocity.y;
        rigid.velocity = moveDir;
    }

    private void checkGround()
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

    private void checkjump()
    {
        if (Input.GetKey(KeyCode.Space)&& isGround == true)
        {
            checkjumping = true;
            jumping();
        }
    }

    private void jumping()
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
}
