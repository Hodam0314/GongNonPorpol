using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("���ʵ�����")] // [Header("")] = ����Ƽ �� ������ �ּ�ó���ϵ��� �Ӹ����� �޾���.
    [SerializeField] private Camera mainCam; // [SerializeField] = ����Ƽ ������ ���� ������ �Լ��� ������ ������
    [SerializeField] private Animator animator;
    [SerializeField] private Transform layerDynamic;
    private SpriteRenderer sr;

    [Header("�÷��̾� ������")]
    [Space]
    [SerializeField, Tooltip("�÷��̾��� �̵��ӵ�")][Range(0.5f , 100.0f)] private float speedPlayer = 1.0f; // Tooltip = ������ ���� ����Ƽ �� ���� �Լ��� ���� /// �ּ��� ������. | Range( , ) = �������� �� �ȿ������� ������ ���
    [SerializeField] float horizontal;
    [SerializeField] float vertical;
    private PlayerHp playerHp;

    [Header("������")]
    [SerializeField] private GameObject objBullet;
    [SerializeField] private GameObject objExplosion;

    [Header("�Ѿ˼���")]
    [SerializeField] private bool AutoBullet = false; // false = ������ Ű�� �Է��ؾ߸� �Ѿ��� �߻� , true = �ڵ����� �Ѿ��� �߻�
    [SerializeField, Range(0.0f , 3.0f)] private float timerShoot = 0.5f; // �Ѿ��� �߻��ϴ� ����
    private float timer = 0.0f; // �Ѿ� �߻� Ÿ�̸�
    [SerializeField] private float bulletDamage = 1.0f;
    private Transform trsShootPos;

    [Header("�÷��̾� ������")]
    [SerializeField] private float MaxHp = 3;
    [SerializeField] private float curHp;
    [SerializeField, Range(1 , 5)] private int level = 1;
    [SerializeField] private int levelmin = 1;
    [SerializeField] private int levelmax = 5;

    private void OnValidate() // �ν����Ϳ��� ���� ����Ǹ� �� �Լ��� ȣ���
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
                //� �������� �Ծ����� üũ�� ������ Ÿ�Կ� �´±�� ����
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
        //GameObject objCamera = GameObject.Find("Camera"); // ����Ƽ ���� Hierarchy �� �ִ� ��� �ҷ�����
        //mainCam = objCamera.GetComponent<Camera>();

        //mainCam = GameObject.Find("Camera").GetComponent<Camera>();

        mainCam = Camera.main; // �̱���
        animator = transform.GetComponent<Animator>();
    }

    private void Update()
    {
        moving();
        checkMovePosition();
        GongAnimation();

        checkShooting();
    }
    #region moving �ڵ�
    /// <summary>
    /// �÷��̾��� �̵����
    /// </summary>
    private void moving()
    {
        //����
        horizontal = Input.GetAxisRaw("Horizontal"); //horizontal = �����̶�� ��
        //Debug.Log(horizontal);

        //����
        vertical = Input.GetAxisRaw("Vertical"); // Vertical = �����̶�� ��
        //Debug.Log(vertical);
        transform.position += new Vector3(horizontal, vertical, 0f) * Time.deltaTime * speedPlayer;

    }
    #endregion

    #region ī�޶� ȭ����� �ڵ�
    /// <summary>
    /// ī�޶� ȭ����� (��ü�� ȭ�� ������ ������ �ʰ� ����)
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

    #region �ִϸ��̼� �ڵ�
    /// <summary>
    /// �ִϸ��̼�
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
        //    Debug.Log("�ٿ��ư�� �������ϴ�.");
        //}
        //else if(Input.GetKey(KeyCode.DownArrow))
        //{
        //    Debug.Log("�ٿ��ư�� ������  �ֽ��ϴ�.");
        //}
        //else if (Input.GetKeyUp(KeyCode.DownArrow))
        //{
        //    Debug.Log("�ٿ��ư�� ���ҽ��ϴ�.");
        //}
    }
    #endregion

    #region �÷��̾��� ���� �ڵ�
    /// <summary>
    /// �Ѿ� �߻� ���� üũ
    /// </summary>
    private void checkShooting()
    {
        if (AutoBullet == false && Input.GetKeyDown(KeyCode.Z))
        {
            shootBullet();
        }
        else if (AutoBullet == true)
        {
            timer += Time.deltaTime; //Ÿ�̸� ����
            if (timerShoot <= timer) //Ÿ�̸Ӱ� ������ �����ϸ�
            {
                shootBullet();  //�Ѿ��� �߻�
                timer = 0.0f;   //Ÿ�̸Ӱ� �ʱ�ȭ
            }
        }
    }
    #endregion

    #region �Ѿ� �߻��ڵ�
    /// <summary>
    /// �Ѿ��� �߻�
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
                Debug.LogError("������ �̹� �ִ��Դϴ�.");
                break;
        }


    }
    #endregion

    private void createBullet(Vector3 _pos, Vector3 _rot)
    {
        GameObject obj = Instantiate(objBullet, _pos, Quaternion.Euler(_rot), layerDynamic);// Instantiate = ������Ʈ�� ����(���� = ����) (10������) , 1���� �������.   [���ֻ�� , �߿���!] 
        Bullet objSc = obj.GetComponent<Bullet>();
        objSc.SetDamage(true, bulletDamage);

        //obj.transform.position = transform.position;
        //obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 30)); // �����̼��� ���ʹϾ� ����
        //obj.transform.parent = layerDynamic;

        //obj.transform.SetParent(transform);
        //obj.transform.eulerAngles = new Vector3(0, 0, 30); // eulerAngles�� �̿��� ����
    }


    #region �÷��̾ �������� �޾����� ǥ���ϴ� �ڵ�
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
