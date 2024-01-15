using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float MaxHp = 5; //보스 체력바 만들예정
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

    [SerializeField] bool isBoss = false; // 보스 판단기준
    public bool IsBoss
    {
        get 
        {
            return isBoss;
        }
    }

    [Header("보스용패턴")]
    private float startingPosY; // 시작위치
    private bool isStartingMoving = false; // 시작시 이동연출 확인
    private float ratioY = 0.0f; // 이동기능 연출 비율
    private bool isSwayRight = false; //현재 보스가 좌측으로 가야하는지 우로가야하는지 결정
    private Camera mainCam;

    [Header("보스패턴")]
    //앞으로 발사
    [SerializeField] private int pattern1Count = 8;
    [SerializeField] private float pattern1Reload = 1f;
    [SerializeField] private GameObject pattern1Bullet;
    //샷건
    [SerializeField] private int pattern2Count = 8;
    [SerializeField] private float pattern2Reload = 1f;
    [SerializeField] private GameObject pattern2Bullet;
    // 조준 게틀링
    [SerializeField] private int pattern3Count = 8;
    [SerializeField] private float pattern3Reload = 1f;
    [SerializeField] private GameObject pattern3Bullet;

    private int pattern = 1; //현재 패턴
    private int patternShootCount = 0; //현재패턴을 몇번 사용했는지
    private float shootTimer = 0.0f; //패턴리로드 용
    private bool patternChange = false; //패턴을 교체해야하는지

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
        //하이어라키에 오브젝트가 많고 찾으려는 오브젝트가 하단에 위치할수록 부하(느려짐)를 일으킨다.
        //사용할때 주의가 필요함.
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
    #region 적기의 움직임
    /// <summary>
    /// 적의 움직임
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
        if(isSwayRight == false) // 왼쪽으로
        {
            transform.position += Vector3.left * Time.deltaTime * Speed;
        }
        else // 오른쪽으로
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
        if (isBoss == false || isStartingMoving == false) //보스가 아닌적기, 시작연출을 하지 않았으면 총알을 안쏨
        {
            return;
        }

        shootTimer += Time.deltaTime;

        if (patternChange == true) // 패턴 변경중
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
            case 1: // 전방으로 발사
                {
                    if (shootTimer >= pattern1Reload)
                    {
                        shootTimer = 0.0f;
                        shootStraight();
                        if (patternShootCount >= pattern1Count)
                        {
                            //랜덤인데 자신의 숫자를 제외한 랜덤으로 제작해보기
                            pattern++;
                            patternChange = true;
                            patternShootCount = 0;
                        }
                    }
                }
                break;
            case 2: //샷건
                {
                    if (shootTimer >= pattern2Reload)
                    {
                        shootTimer = 0.0f;
                        shootShotGun();
                        if (patternShootCount >= pattern2Count)
                        {
                            //랜덤인데 자신의 숫자를 제외한 랜덤으로 제작해보기
                            pattern++;
                            patternChange = true;
                            patternShootCount = 0;
                        }
                    }
                }
                break;
            case 3: //조준 게틀링
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

    private void shootStraight()//전방으로 총알 발사
    {
        createBullet(pattern1Bullet, transform.position, new Vector3(0, 0, 180.0f), 5);//가운데
        createBullet(pattern1Bullet, transform.position + new Vector3(-1f, 0f, 0f), new Vector3(0, 0, 180.0f) , 5); //왼쪽
        createBullet(pattern1Bullet, transform.position + new Vector3(1f, 0, 0f), new Vector3(0, 0, 180.0f), 5); //오른쪽

        patternShootCount++;
    }

    private void shootShotGun()
    {
        createBullet(pattern2Bullet, transform.position, new Vector3(0, 0, 180.0f),4);//가운데
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

    #region 데미지 입었을때 피해받는 코드
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

        if (CurHp <= 0 || (_bodyslam == true && isBoss == false)) // 0이되면 폭파
        {
            Destroy(gameObject); //가비지컬렉터에 등록
            GameObject obj = Instantiate(objExplosion, transform.position, Quaternion.identity, layerDynamic);
            Explosion objSc = obj.GetComponent<Explosion>();
            float sizeWidth = sr.sprite.rect.width;
            objSc.SetAnimationSize(sizeWidth);
            //만약 바디슬램으로 들어온 코드라면 아이템을 주지않음

            if(isDeath == false && haveItem == true && _bodyslam == false)
            {
                //아이템을 생성 내 위치에
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
            sr.sprite = sprHit; // 하얗게 변환
            //몇초 후에 다시 어떤 기능을 실행해줘, 매개변수가 없어야 함
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
