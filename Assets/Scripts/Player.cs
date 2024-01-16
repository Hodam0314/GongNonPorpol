using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager manager;
    Rigidbody2D rigid;
    Camera mainCam;
    BoxCollider2D box;
    Animator anim;

    private float verticalVelocity;
    private bool checkjumping = false;
    private bool isGround = false;
    private float countjump = 2f;
    private bool checkhit = false;
    private float godTimer = 0f;
    private bool checkNuckBack = false;
    private float nuckBackTimer = 0f;

    [Header("플레이어 관련")]
    [SerializeField] private float maxHp = 30f;
    [SerializeField] private float curHp;
    [SerializeField] private float damage = 5f;
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private bool checkGod = false;
    [SerializeField] private float godLimit = 3f;
    [SerializeField] private float highNuckBackForce = 6f;
    [SerializeField] private float nuckBackForce = 4f;
    [SerializeField] private float nuckBackTimeLimit = 1f;
    Vector3 moveDir;

    [Header("중력 및 점프")]
    private float gravity = 9.81f;
    [SerializeField] private float jumpForce = 5f;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        manager = GetComponent<GameManager>();
        curHp = maxHp;
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
        doublejump();

        playAnimation();
        GodMod();
        Timecheck();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == GameTag.Mob.ToString())
        {
            Hit(5f);
            NuckBack();
        }
        else if (collision.gameObject.tag == GameTag.FlyingMob.ToString())
        {
            Hit(10f);
            NuckBack();
        }
    }

    private void Timecheck()
    {
        if (nuckBackTimer != 0.0f)
        {
            nuckBackTimer -= Time.deltaTime;
            if(nuckBackTimer < 0.0f) 
            {
                checkNuckBack = false;
                nuckBackTimer = 0.0f;
            }
        }
    }


    private void NuckBack()
    {
        if (checkNuckBack == true)
        {
            Vector2 nuckback = new Vector2(nuckBackForce, highNuckBackForce);
            if (moveDir.x > 0)
            {
                nuckback.x *= -1;
            }
            rigid.velocity += nuckback;
        }
    }

    public void Hit(float _damage)
    {
        if (checkGod == false)
        {
            curHp -= _damage;
        }

        checkGod = true;
        checkNuckBack = true;
        nuckBackTimer = nuckBackTimeLimit;
    }

    private void GodMod()
    {
        if (checkGod == true)
        {
            godTimer += Time.deltaTime;
            if (godTimer >= godLimit)
            {
                godTimer = 0.0f;
                checkGod = false;
            }
        }

    }

    private void turning() //왼쪽 , 오른쪽 움직이는 키 할당으로 스케일을 변경하여 플레이어 캐릭터를 반전시켜줌
    {
        if (moveDir.x > 0 && transform.localScale.x < 1)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        else if (moveDir.x < 0 && transform.localScale.x > -1)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void moving() //플레이어의 움직이는 코드
    {
        if (checkNuckBack == false)
        {
            moveDir.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
            moveDir.y = rigid.velocity.y;
            rigid.velocity = moveDir;
        }
    }

    private void checkGround() //플레이어가 타일위에 서있는지 체크해주는 코드
    {
        isGround = false;
        RaycastHit2D hit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0f,
            Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
        if (hit.transform != null)
        {
            isGround = true;
            verticalVelocity = 0;
        }
    }

    private void checkjump() //플레이어 점프가능상태를 체크 , 점프 키를 입력받는 코드
    {
        if (Input.GetKey(KeyCode.Space) && isGround == true && checkNuckBack == false)
        {
            countjump = 1;
            checkjumping = true;
            jumping();
        }
    }

    private void jumping() //플레이어의 점프기능을 실행시켜주는 코드
    {
        if (isGround == false)
        {
            verticalVelocity -= gravity * Time.deltaTime;
            if (verticalVelocity < -10)
            {
                verticalVelocity = -10;
            }
        }
        if (checkjumping == true && checkNuckBack == false)
        {
            checkjumping = false;
            verticalVelocity = jumpForce;
        }
        rigid.velocity = new Vector2(rigid.velocity.x, verticalVelocity);
    }

    private void doublejump() //플레이어의 점프카운트를 이용한 더블점프 기능
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround == false && countjump == 1 && checkNuckBack == false)
        {
            checkjumping = true;
            jumping();
            countjump += -1;
        }
        else if (isGround == true)
        {
            countjump = 2;
        }
    }

    private void playAnimation() //플레이어의 애니메이션 코드
    {
        anim.SetBool("isJump", isGround);
        anim.SetInteger("isMoving", (int)moveDir.x);
    }
}
