using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("기초데이터")] // [Header("")] = 유니티 툴 내에서 주석처리하듯이 머리말을 달아줌.
    [SerializeField] private Camera mainCam; // [SerializeField] = 유니티 툴에서 내가 설정한 함수를 눈으로 보여줌
    [SerializeField] private Animator animator;
    [SerializeField] private Transform layerDynamic;
    private SpriteRenderer sr;

    [Header("플레이어 데이터")]
    [Space]
    [SerializeField, Tooltip("플레이어의 이동속도")][Range(0.5f , 100.0f)] private float speedPlayer = 1.0f; // Tooltip = 협업을 위한 유니티 툴 내에 함수에 대한 /// 주석과 동일함. | Range( , ) = 범위내의 값 안에서만의 수정이 허용
    [SerializeField] float horizontal;
    [SerializeField] float vertical;
    private PlayerHp playerHp;

    [Header("프리팹")]
    [SerializeField] private GameObject objBullet;
    [SerializeField] private GameObject objExplosion;

    [Header("총알세팅")]
    [SerializeField] private bool AutoBullet = false; // false = 유저가 키를 입력해야만 총알이 발사 , true = 자동으로 총알이 발사
    [SerializeField, Range(0.0f , 3.0f)] private float timerShoot = 0.5f; // 총알을 발사하는 기준
    private float timer = 0.0f; // 총알 발사 타이머
    [SerializeField] private float bulletDamage = 1.0f;
    private Transform trsShootPos;

    [Header("플레이어 데이터")]
    [SerializeField] private float MaxHp = 3;
    [SerializeField] private float curHp;
    [SerializeField, Range(1 , 5)] private int level = 1;
    [SerializeField] private int levelmin = 1;
    [SerializeField] private int levelmax = 5;

    private void OnValidate() // 인스펙터에서 값이 변경되면 이 함수가 호출됨
    {
        if (playerHp != null)
        {
            playerHp.SetPlayerHp(curHp, MaxHp);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == GameTag.Enemy.ToString())
        {
            Hit(1.0f);
            Enemy enemySc = collision.GetComponent<Enemy>();
            enemySc.Hit(0f, true);
        }
            else if (collision.tag == GameTag.Item.ToString())
            {
                //어떤 아이템을 먹었는지 체크후 아이템 타입에 맞는기능 실행
                Item itemSc = collision.GetComponent<Item>();
                Item.ItemType itemType = itemSc.GetItemType();
                if (itemType == Item.ItemType.PowerUp)
                {
                    level++;
                    if (level > levelmax)
                    {
                        level = levelmax;
                    }
                }
                else if (itemType == Item.ItemType.HpRecovery)
                {
                    curHp++;
                    if (curHp > MaxHp)
                    {
                        curHp = MaxHp;
                    }
                    playerHp.SetPlayerHp(curHp, MaxHp);
                }
                Destroy(collision.gameObject);

                //switch (itemType)
                //{
                //    case Item.ItemType.PowerUp:
                //        break;
                //    case Item.ItemType.HpRecovery:
                //        break;
                //}
            }

    }
    private void Awake()
    {
        curHp = MaxHp;
        sr = GetComponent<SpriteRenderer>();
        trsShootPos = transform.Find("ShootPos");
    }
    private void Start()
    {
        //GameObject objCamera = GameObject.Find("Camera"); // 유니티 내의 Hierarchy 에 있는 기능 불러오기
        //mainCam = objCamera.GetComponent<Camera>();

        //mainCam = GameObject.Find("Camera").GetComponent<Camera>();

        mainCam = Camera.main; // 싱글톤
        animator = transform.GetComponent<Animator>();
    }

    private void Update()
    {
        moving();
        checkMovePosition();
        GongAnimation();

        checkShooting();
    }
    #region moving 코드
    /// <summary>
    /// 플레이어의 이동기능
    /// </summary>
    private void moving()
    {
        //수평
        horizontal = Input.GetAxisRaw("Horizontal"); //horizontal = 수평이라는 뜻
        //Debug.Log(horizontal);

        //수직
        vertical = Input.GetAxisRaw("Vertical"); // Vertical = 수직이라는 뜻
        //Debug.Log(vertical);
        transform.position += new Vector3(horizontal, vertical, 0f) * Time.deltaTime * speedPlayer;

    }
    #endregion

    #region 카메라 화면잠금 코드
    /// <summary>
    /// 카메라 화면잠금 (기체가 화면 밖으로 나가지 않게 해줌)
    /// </summary>
    private void checkMovePosition()
    {
        Vector3 currentPlayerPos = mainCam.WorldToViewportPoint(transform.position);
        if (currentPlayerPos.x < 0.1f)
        {
            currentPlayerPos.x = 0.1f;
        }
        else if (currentPlayerPos.x > 0.9f)
        {
            currentPlayerPos.x = 0.9f;
        }

        if (currentPlayerPos.y < 0.05f)
        {
            currentPlayerPos.y = 0.05f;
        }
        else if (currentPlayerPos.y > 0.95f)
        {
            currentPlayerPos.y = 0.95f;
        }

        Vector3 fixedPlayerPos = mainCam.ViewportToWorldPoint(currentPlayerPos);
        transform.position = fixedPlayerPos;
    }
    #endregion

    #region 애니메이션 코드
    /// <summary>
    /// 애니메이션
    /// </summary>
    private void GongAnimation()
    {
        int fixedHorizontal = (int)horizontal;
        animator.SetInteger("Gong", fixedHorizontal);

        //animator.SetBool("Bool Left", left);
        //animator.SetBool("Bool Right", right);

        //animator.SetInteger("GongHorizontal", (int)horizontal);

        //animator.SetFloat("New Float", horizontal);

        //if(Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    Debug.Log("다운버튼을 눌렀습니다.");
        //}
        //else if(Input.GetKey(KeyCode.DownArrow))
        //{
        //    Debug.Log("다운버튼을 누르고  있습니다.");
        //}
        //else if (Input.GetKeyUp(KeyCode.DownArrow))
        //{
        //    Debug.Log("다운버튼을 놓았습니다.");
        //}
    }
    #endregion

    #region 플레이어의 슈팅 코드
    /// <summary>
    /// 총알 발사 조건 체크
    /// </summary>
    private void checkShooting()
    {
        if (AutoBullet == false && Input.GetKeyDown(KeyCode.Z))
        {
            shootBullet();
        }
        else if (AutoBullet == true)
        {
            timer += Time.deltaTime; //타이머 증가
            if (timerShoot <= timer) //타이머가 기준을 오버하면
            {
                shootBullet();  //총알을 발사
                timer = 0.0f;   //타이머가 초기화
            }
        }
    }
    #endregion

    #region 총알 발사코드
    /// <summary>
    /// 총알을 발사
    /// </summary>
    private void shootBullet()
    {
        switch (level)
        {
            case 1:
                {
                    createBullet(trsShootPos.position, Vector3.zero);
                }
                break;     
            case 2:
                {
                    createBullet(trsShootPos.position + new Vector3(-0.2f,0), Vector3.zero);
                    createBullet(trsShootPos.position + new Vector3(0.2f,0), Vector3.zero);
                }
                break;     
            case 3:
                {
                    createBullet(trsShootPos.position + new Vector3(-0.2f, 0), Vector3.zero);
                    createBullet(trsShootPos.position + new Vector3(0.2f, 0), Vector3.zero);
                    createBullet(trsShootPos.position, Vector3.zero);
                }
                break;     
            case 4:
                {
                    createBullet(trsShootPos.position + new Vector3(-0.2f, 0), Vector3.zero);
                    createBullet(trsShootPos.position + new Vector3(0.2f, 0), Vector3.zero);
                    createBullet(trsShootPos.position, Vector3.zero);
                    createBullet(trsShootPos.position + new Vector3(-0.2f,0), new Vector3(0,0,15f));
                }
                break;           
            case 5:
                {
                    createBullet(trsShootPos.position + new Vector3(-0.2f, 0), new Vector3(0, 0, 3f));
                    createBullet(trsShootPos.position + new Vector3(0.2f, 0), new Vector3(0, 0, -3f));
                    createBullet(trsShootPos.position, Vector3.zero);
                    createBullet(trsShootPos.position + new Vector3(-0.2f, 0), new Vector3(0, 0, 7f));
                    createBullet(trsShootPos.position + new Vector3(0.2f, 0), new Vector3(0, 0, -7f));
                }
                break;
            default:
                Debug.LogError("레벨이 이미 최대입니다.");
                break;
        }


    }
    #endregion

    private void createBullet(Vector3 _pos, Vector3 _rot)
    {
        GameObject obj = Instantiate(objBullet, _pos, Quaternion.Euler(_rot), layerDynamic);// Instantiate = 오브젝트를 생성(실제 = 복제) (10개까지) , 1개도 상관없다.   [자주사용 , 중요함!] 
        Bullet objSc = obj.GetComponent<Bullet>();
        objSc.SetDamage(true, bulletDamage);

        //obj.transform.position = transform.position;
        //obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 30)); // 로테이션의 쿼터니언 사용법
        //obj.transform.parent = layerDynamic;

        //obj.transform.SetParent(transform);
        //obj.transform.eulerAngles = new Vector3(0, 0, 30); // eulerAngles을 이용한 사용법
    }


    #region 플레이어가 데미지를 받았을때 표시하는 코드
    public void Hit(float _damage)
    {
        curHp -= _damage;
        playerHp.SetPlayerHp(curHp, MaxHp);
        if (curHp <= 0)
        {
            Destroy(gameObject);
            GameObject obj = Instantiate(objExplosion, transform.position, Quaternion.identity, layerDynamic);
            Explosion objSc = obj.GetComponent<Explosion>();
            float sizeWidth = sr.sprite.rect.width;
            objSc.SetAnimationSize(sizeWidth);

            GameManager.Instance.GameOver();
        }
        else
        {
            level--;
            if (level < levelmin)
            {
                level = levelmin;
            }
        }
    }
    #endregion



    public void SetPlayerHp(PlayerHp _value)
    {
        playerHp = _value;
        playerHp.SetPlayerHp(curHp, MaxHp);
    }

}
