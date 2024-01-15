using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TMPro.EditorUtilities;
using Newtonsoft.Json;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //싱글턴

    private Camera mainCam;
    [Header("플레이어")]
    [SerializeField] GameObject objPlayer;


    [Header("적기 생성")]
    [SerializeField] private List<GameObject> listEnemys;
    [SerializeField, Range(0.1f, 1f)] private float timerSpawn; // 적의 생성시간
    private float timer = 0.0f;
    [SerializeField] bool boolEnemySpawn = false;
    [SerializeField] Transform trsSpawnPos;
    [SerializeField] Transform layerEnemy;
    [SerializeField] Transform layerDynamic;

    [Header("아이템생성")]
    [SerializeField, Range(0.0f, 100.0f)] float dropRate = 0.0f;
    [SerializeField] private List<GameObject> listitem;

    [Header("게이지")]
    [SerializeField] Slider slider;
    [SerializeField] TMP_Text sliderText;
    [SerializeField] Image sliderFillImage;

    [SerializeField] float bossSpawnTime = 60f;
    [SerializeField] float curtime = 0.0f;
    private bool bossSpawn = false;
    [SerializeField] GameObject objBoss;
    [SerializeField] Color SliderTimeColor;
    [SerializeField] Color SliderBossHpColor;

    [Header("점수")]
    [SerializeField] TextMeshProUGUI ScoreText;
    private int curScore = 0;

    [Header("게임오버메뉴")]
    [SerializeField] GameObject objGameOverMenu; //게임오버시 출력되는 오브젝트
    [SerializeField] TMP_Text textRank; //게임오버시 랭크
    [SerializeField] TMP_Text textTotalScore; //게임오버시 점수
    [SerializeField] TMP_InputField iFName;//유저가 본인의 이름을 남기는 인풋필드
    [SerializeField] Button btnMainMenu;//메인버튼 버튼
    [SerializeField] TMP_Text textBtnMainMenu;//메인메뉴 버튼 텍스트

    public class UserScore
    {
        public int score;
        public string name = "";
    }

    private List<UserScore> listScore = new List<UserScore>();
    private string scoreKey = "scoreKey";



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        mainCam = Camera.main;
        SetNewGame();
        setScore();
        checkGameOverMenu();

        #region 알아두면 좋음
        //string rank = ""; //10개의 점수를 저장

        //listScore.Add(new UserScore() { score = 100, name = "aaa" });
        //listScore.Add(new UserScore() { score = 90, name = "bbb" });
        //listScore.Add(new UserScore() { score = 80, name = "ccc" });

        ////메인씬 , 씬
        //rank = JsonConvert.SerializeObject(listScore);
        //listScore.Clear();
        //listScore = JsonConvert.DeserializeObject<List<UserScore>>(rank);
        #endregion
    }

    private void checkGameOverMenu()
    {
        //if (objGameOverMenu.activeSelf == true) // 켜져있다
        //{
        //    objGameOverMenu.SetActive(false);//꺼짐
        //}
        objGameOverMenu.SetActive(false);
    }


    private void setScore()
    {
        if (PlayerPrefs.HasKey(scoreKey))
        {
            string savedValue = PlayerPrefs.GetString(scoreKey);
            if (savedValue == string.Empty)
            {
                clearAllScore();
            }
            else
            {
                listScore = JsonConvert.DeserializeObject<List<UserScore>>(savedValue);
                if(listScore.Count != 10)
                {
                    Debug.LogError($"리스트 스코어의 갯수가 이상합니다. \n리스트 스코어의 갯수 = {listScore.Count}");
                }
                #region 알아두면좋음
                //try
                //{
                //listScore = JsonConvert.DeserializeObject<List<UserScore>>(savedValue);
                //}
                //catch (System.Exception e)
                //{
                //clearAllScore();
                //}
                #endregion
            }
        }
        else
        {
            clearAllScore();
        }
        #region 알아두면좋음
        //string splitText = ",!_";//이문자열은 데이터가 나눠진다는 뜻
        //string testValue = "가나다라마바사";
        //testValue = testValue.Replace("{","");
        //testValue = testValue.Replace("}","");
        //testValue.sub

        //string[] splitValue = testValue.Split(splitText);
        #endregion
    }

    /// <summary>
    /// 데이터가 올바르지 않거나 없을때 새로운 데이터를 정확한 양식으로 만들고 저장합니다.
    /// </summary>
    private void clearAllScore()
    {
        listScore.Clear();
        for (int i = 0; i < 10; ++i)
        {
            listScore.Add(new UserScore());
        }
        string saveValue = JsonConvert.SerializeObject(listScore);
        PlayerPrefs.SetString(scoreKey, saveValue);
    }

    private void Update()
    {
        checkSpawn();
        checkBossTimer();
        checkSliderColor();
    }
    #region 몬스터 리스폰
    /// <summary>
    /// 몬스터 스폰 체크(확인)
    /// </summary>
    private void checkSpawn()
    {
        if (boolEnemySpawn == false || bossSpawn == true)
        {
            return;
        }
        timer += Time.deltaTime;
        if (timer >= timerSpawn)
        {
            enemySpawn();
            timer = 0.0f;
        }
    }
    #endregion

    #region 보스 리스폰
    /// <summary>
    /// 보스 리스폰 타이머
    /// </summary>
    private void checkBossTimer()
    {
        if (bossSpawn == true)
        {
            return;
        }

        curtime += Time.deltaTime;
        if (curtime >= bossSpawnTime)
        {
            bossSpawn = true;
            enemyBossSpawn();
            destoryAllEnemy();
        }

        SetSliderText();
        SetSlider();
    }

    private void destoryAllEnemy()
    {
        //보스를 제외한 모든 적을 삭제
        int count = layerEnemy.childCount;
        for (int iNum = count - 1; iNum > -1; iNum--)
        {  
            Transform objEnemy = layerEnemy.GetChild(iNum);
            Enemy objSc = objEnemy.GetComponent<Enemy>();
            if( objSc.IsBoss == false)
            {
                Destroy(objEnemy.gameObject);
            }
        }
    }

    private void enemyBossSpawn()
    {
        GameObject obj = Instantiate(objBoss, trsSpawnPos.position, Quaternion.identity, layerEnemy);
        Enemy objSc = obj.GetComponent<Enemy>();
    }

    private void checkSliderColor()
    {
        //보스출동전 타이머컬러
        if (bossSpawn == false && sliderFillImage.color != SliderTimeColor)
        {
            sliderFillImage.color = SliderTimeColor;
        }
        //보스출동했을때 타이머컬러
        else if (bossSpawn == true && sliderFillImage.color != SliderBossHpColor)
        {
            sliderFillImage.color = SliderBossHpColor;
        }

    }

    private void SetSliderText()
    {
        string timerValue = $"{(int)curtime} / {(int)bossSpawnTime}";
        sliderText.text = timerValue;
    }

    private void SetSliderDefault()
    {
        slider.maxValue = bossSpawnTime;
        slider.minValue = 0;
    }


    private void SetSlider()
    {
        slider.value = curtime;
    }

    private void SetNewGame()
    {
        curtime = 0.0f;

        SetSliderDefault();
        SetSliderText();
        SetSlider();
    }

    #endregion

    #region 적 생성
    /// <summary>
    /// 적 생성
    /// </summary>
    private void enemySpawn()
    {
        int iRand = Random.Range(0, listEnemys.Count); // 0 , 1 , 2
        GameObject objEnemy = listEnemys[iRand]; //ref

        //var limitPosX = getLimitEnemy();
        (float _min, float _max) limitPosX = getLimitEnemy(); // y?
        float xPos = Random.Range(limitPosX._min, limitPosX._max);
        Vector3 instPos = new Vector3(xPos, trsSpawnPos.position.y, 0); // 적기가 생성될 위치

        GameObject obj = Instantiate(objEnemy, instPos, Quaternion.identity, layerEnemy);
        Enemy objSc = obj.GetComponent<Enemy>();

        float rate = Random.Range(0.0f, 100.0f);
        if (rate <= dropRate)
        {
            objSc.SetHaveItem();
        }
    }
    #endregion

    #region 적기 생성의 화면 제한
    /// <summary>
    /// 적기 생성의 화면 제한
    /// </summary>
    private (float _min, float _max) getLimitEnemy()
    {
        float minValue = mainCam.ViewportToWorldPoint(new Vector3(0.1f, 0f, 0f)).x;
        float maxValue = mainCam.ViewportToWorldPoint(new Vector3(0.9f, 0f, 0f)).x;

        return (minValue, maxValue);
    }
    #endregion

    public Transform GetLayerDynamic()
    {
        return layerDynamic;
    }

    public GameObject GetPlayerGameObject()
    {
        return objPlayer;
    }

    public void CreateItem(Vector3 _createPos)
    {
        int rand = Random.Range(0, listitem.Count);
        Instantiate(listitem[rand], _createPos, Quaternion.identity, layerDynamic);
    }
    /// <summary>
    /// 보스의 현재 체력 및 최대체력을 게이지에 출력합니다.
    /// </summary>
    /// <param name="_curHp">현재체력</param>
    /// <param name="_maxHp">최대체력</param>
    public void SetBossHp(float _curHp, float _maxHp)
    {
        if (slider.maxValue > _maxHp)
        {
            slider.maxValue = _maxHp;
        }
        slider.value = _curHp;

        sliderText.text = $"{(int)_curHp} / {(int)_maxHp}";
    }

    public void GameContinue()
    {
        bossSpawnTime += 20.0f;
        bossSpawn = false;
        SetNewGame();
    }

    public void ShowScore(int _score)
    {
        curScore += _score;
        ScoreText.text = curScore.ToString("D8");
    }

    public void GameOver()
    {
        int rank = getPlayerRank();
        textTotalScore.text = curScore.ToString("D8");
        btnMainMenu.onClick.RemoveAllListeners(); //모든 이벤트를 삭제

        if (rank == -1)//현재점수만 표기하고 메인화면으로 이동
        {
            textRank.text = "순위권 외";
            iFName.gameObject.SetActive(false);
            textBtnMainMenu.text = "메인메뉴로";
            btnMainMenu.onClick.AddListener(()=>
            {
                onClickBtnMainMenu();
            });//숫자를 그냥 넣어서는 동작안됨
        }
        else//유저에게 몇등했는지를 표기하고 이름을 남길수 있도록함
        {
            textRank.text = $"{rank + 1}등";
            iFName.text = string.Empty;//"";
            iFName.gameObject.SetActive(true);
            textBtnMainMenu.text = "저장후 메인메뉴로";
            btnMainMenu.onClick.AddListener(() =>
            {
                setNewRank(rank , iFName.text);
                onClickBtnMainMenu();
            });
        }
        objGameOverMenu.SetActive(true);
    }

    /// <summary>
    /// 새 등수를 등록시키고 제일 마지막 랭크 하나를 삭제
    /// </summary>
    /// <param name="_rank">삽입될 랭크</param>
    /// <param name="_name">유저 입력한 이름</param>
    private void setNewRank(int _rank, string _name)
    {
        listScore.Insert(_rank, new UserScore() { name = _name, score = curScore });
        //11개가 저장되어있음
        listScore.RemoveAt(listScore.Count - 1); //마지막 저장된 데이터를 삭제


        string saveValue = JsonConvert.SerializeObject(listScore);
        PlayerPrefs.SetString(scoreKey, saveValue);
    }

    /// <summary>
    /// 메인메뉴로 이동
    /// </summary>
    private void onClickBtnMainMenu()
    {
        //씬변경기능
        int count = listScore.Count;
        for(int i = 0; i < count; ++i)
        {
            UserScore uScore = listScore[i];
            Debug.Log($"{i+1}등 - Name = {uScore.name}, score = {uScore.score}");
        }
    }

    private int getPlayerRank()
    {
        int count = listScore.Count;
        for(int iNum = 0; iNum < count; ++iNum)
        {
            UserScore userscore = listScore[iNum];
            if(userscore.score < curScore)
            {
                return iNum; //현재 등수를 밖으로 전달함
            }
        }

        return -1;
    }

}
