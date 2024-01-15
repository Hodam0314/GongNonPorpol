using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float MaxHp = 5; //���� ü�¹� ���鿹��
    private float CurHp = 0;
    [SerializeField] private float Speed = 4;

    [SerializeField] private Sprite sprHit;
    private Sprite sprDefault;

    private SpriteRenderer sr;
    [SerializeField] private GameObject objExplosion;

    private Transform layerDynamic;
    private GameManager gameManager;
    private bool haveItem;
    private bool isDeath = false;

    [SerializeField] bool isBoss = false; // ���� �Ǵܱ���
    public bool IsBoss
    {
        get 
        {
            return isBoss;
        }
    }

    [Header("����������")]
    private float startingPosY; // ������ġ
    private bool isStartingMoving = false; // ���۽� �̵����� Ȯ��
    private float ratioY = 0.0f; // �̵���� ���� ����
    private bool isSwayRight = false; //���� ������ �������� �����ϴ��� ��ΰ����ϴ��� ����
    private Camera mainCam;

    [Header("��������")]
    //������ �߻�
    [SerializeField] private int pattern1Count = 8;
    [SerializeField] private float pattern1Reload = 1f;
    [SerializeField] private GameObject pattern1Bullet;
    //����
    [SerializeField] private int pattern2Count = 8;
    [SerializeField] private float pattern2Reload = 1f;
    [SerializeField] private GameObject pattern2Bullet;
    // ���� ��Ʋ��
    [SerializeField] private int pattern3Count = 8;
    [SerializeField] private float pattern3Reload = 1f;
    [SerializeField] private GameObject pattern3Bullet;

    private int pattern = 1; //���� ����
    private int patternShootCount = 0; //���������� ��� ����ߴ���
    private float shootTimer = 0.0f; //���ϸ��ε� ��
    private bool patternChange = false; //������ ��ü�ؾ��ϴ���

    public enum eumEenemyType
    {
        typeA = 10,
        typeB = 30,
        typeC = 90,
        Boss = 1000,
    }
    public eumEenemyType enemyType;


    private void Awake()
    {
        CurHp = MaxHp;
        sr = GetComponent<SpriteRenderer>();

        sprDefault = sr.sprite;
        startingPosY = transform.position.y;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        //layerDynamic = GameObject.Find("LayerDynamic").transform;
        //���̾��Ű�� ������Ʈ�� ���� ã������ ������Ʈ�� �ϴܿ� ��ġ�Ҽ��� ����(������)�� ����Ų��.
        //����Ҷ� ���ǰ� �ʿ���.
        gameManager = GameManager.Instance;
        layerDynamic = gameManager.GetLayerDynamic();

        if (haveItem == true)
        {
            sr.color = new Color(0f, 0.7f, 1.0f);
        }
        mainCam = Camera.main;
        if(isBoss == true)
        {
            gameManager.SetBossHp(CurHp, MaxHp);
        }
    }

    private void Update()
    {
        moving();
        shootPattern();
    }
    #region ������ ������
    /// <summary>
    /// ���� ������
    /// </summary>
    private void moving()
    {
        if (isBoss == false)
        {
            transform.position += -transform.up * Time.deltaTime * Speed;
        }
        else if(isBoss == true)
        {
            if (isStartingMoving == false)
            {
                bossStartMoving();
            }
            else
            {
                bossSwayMoving();
            }
        }
    }

    private void bossStartMoving()
    {
        ratioY += Time.deltaTime * 0.3f;
        if (ratioY > 1.0f)
        {
            isStartingMoving = true;
        }

        Vector3 vecDestination = transform.localPosition;
        vecDestination.y = Mathf.SmoothStep(startingPosY, 2.5f, ratioY);
        //vecDestination.y = Mathf.Lerp(startingPosY, 2.5f, ratioY);
        transform.localPosition = vecDestination;

    }

    private void bossSwayMoving()
    {
        if(isSwayRight == false) // ��������
        {
            transform.position += Vector3.left * Time.deltaTime * Speed;
        }
        else // ����������
        {
            transform.position += Vector3.right * Time.deltaTime * Speed;
        }
        checkBossMoving();
    }
    
    private void checkBossMoving()
    {
        Vector3 BossMoving = mainCam.WorldToViewportPoint(transform.position);
        if (isSwayRight == true && BossMoving.x > 0.75f)
        {
            isSwayRight = false;
        }
        else if (isSwayRight == false && BossMoving.x < 0.25f)
        {
            isSwayRight = true;
        }
    }

    private void shootPattern()
    {
        if (isBoss == false || isStartingMoving == false) //������ �ƴ�����, ���ۿ����� ���� �ʾ����� �Ѿ��� �Ƚ�
        {
            return;
        }

        shootTimer += Time.deltaTime;

        if (patternChange == true) // ���� ������
        {
            if(shootTimer >= 3.0f)
            {
                shootTimer = 0.0f;
                patternChange = false;
            }
            return;
        }

        switch (pattern)
        {
            case 1: // �������� �߻�
                {
                    if (shootTimer >= pattern1Reload)
                    {
                        shootTimer = 0.0f;
                        shootStraight();
                        if (patternShootCount >= pattern1Count)
                        {
                            //�����ε� �ڽ��� ���ڸ� ������ �������� �����غ���
                            pattern++;
                            patternChange = true;
                            patternShootCount = 0;
                        }
                    }
                }
                break;
            case 2: //����
                {
                    if (shootTimer >= pattern2Reload)
                    {
                        shootTimer = 0.0f;
                        shootShotGun();
                        if (patternShootCount >= pattern2Count)
                        {
                            //�����ε� �ڽ��� ���ڸ� ������ �������� �����غ���
                            pattern++;
                            patternChange = true;
                            patternShootCount = 0;
                        }
                    }
                }
                break;
            case 3: //���� ��Ʋ��
                {
                    if (shootTimer >= pattern3Reload)
                    {
                        shootTimer = 0.0f;
                        ShootGatling();
                        if (patternShootCount >= pattern3Count)
                        {
                            pattern = 1;
                            patternChange = true;
                            patternShootCount = 0;
                        }
                    }
                }
                break;
        }
    }

    private void createBullet(GameObject _obj, Vector3 _pos, Vector3 _rot, float _speed)
    {
        GameObject obj = Instantiate(_obj, _pos, Quaternion.Euler(_rot), layerDynamic);
        Bullet objSc = obj.GetComponent<Bullet>();
        objSc.SetDamage(false, 1, _speed);
    }

    private void shootStraight()//�������� �Ѿ� �߻�
    {
        createBullet(pattern1Bullet, transform.position, new Vector3(0, 0, 180.0f), 5);//���
        createBullet(pattern1Bullet, transform.position + new Vector3(-1f, 0f, 0f), new Vector3(0, 0, 180.0f) , 5); //����
        createBullet(pattern1Bullet, transform.position + new Vector3(1f, 0, 0f), new Vector3(0, 0, 180.0f), 5); //������

        patternShootCount++;
    }

    private void shootShotGun()
    {
        createBullet(pattern2Bullet, transform.position, new Vector3(0, 0, 180.0f),4);//���
        createBullet(pattern2Bullet, transform.position, new Vector3(0, 0, 180.0f +10), 4);
        createBullet(pattern2Bullet, transform.position, new Vector3(0, 0, 180.0f -10), 4);
        createBullet(pattern2Bullet, transform.position, new Vector3(0, 0, 180.0f +20), 4);
        createBullet(pattern2Bullet, transform.position, new Vector3(0, 0, 180.0f -20), 4);
        createBullet(pattern2Bullet, transform.position, new Vector3(0, 0, 180.0f +15), 4);
        createBullet(pattern2Bullet, transform.position, new Vector3(0, 0, 180.0f -15), 4);
        createBullet(pattern2Bullet, transform.position, new Vector3(0, 0, 180.0f -25), 4);
        createBullet(pattern2Bullet, transform.position, new Vector3(0, 0, 180.0f +25), 4);

        patternShootCount++;
    }

    private void ShootGatling()
    {
        GameObject objPlayer = gameManager.GetPlayerGameObject();
        //if (objPlayer == null)
        //{
        //    return;
        //}

        Vector3 playerPos = objPlayer == null ? new Vector3(0, -3, 0) : objPlayer.transform.position;
        float angle = Quaternion.FromToRotation(Vector3.up, playerPos - transform.position).eulerAngles.z;
        createBullet(pattern3Bullet, transform.position, new Vector3(0f, 0f, angle), 6);

        patternShootCount++;
    }

    #endregion

    #region ������ �Ծ����� ���ع޴� �ڵ�
    public void Hit(float _damage, bool _bodyslam = false)
    {
        if (isBoss == false)
        {
            CurHp -= _damage;
        }
        else if(isStartingMoving == true)
        {
            CurHp -= _damage;
        }

        if (isBoss == true && CurHp >= 0)
        {
            gameManager.SetBossHp(CurHp, MaxHp);
        }

        if (CurHp <= 0 || (_bodyslam == true && isBoss == false)) // 0�̵Ǹ� ����
        {
            Destroy(gameObject); //�������÷��Ϳ� ���
            GameObject obj = Instantiate(objExplosion, transform.position, Quaternion.identity, layerDynamic);
            Explosion objSc = obj.GetComponent<Explosion>();
            float sizeWidth = sr.sprite.rect.width;
            objSc.SetAnimationSize(sizeWidth);
            //���� �ٵ𽽷����� ���� �ڵ��� �������� ��������

            if(isDeath == false && haveItem == true && _bodyslam == false)
            {
                //�������� ���� �� ��ġ��
                gameManager.CreateItem(transform.position);
            }
            isDeath = true;

            gameManager.ShowScore((int)enemyType);
            if ( isBoss == true)
            {
                gameManager.GameContinue();
            }
        }
        else
        {
            sr.sprite = sprHit; // �Ͼ�� ��ȯ
            //���� �Ŀ� �ٽ� � ����� ��������, �Ű������� ����� ��
            Invoke("setSpriteDefault", 0.1f);
        }
    }
    #endregion

    private void setSpriteDefault()
    {
        sr.sprite = sprDefault;
    }

    public void SetHaveItem()
    {
        haveItem = true;
    }

}
