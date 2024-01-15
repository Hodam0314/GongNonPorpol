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
    public static GameManager Instance; //�̱���

    private Camera mainCam;
    [Header("�÷��̾�")]
    [SerializeField] GameObject objPlayer;


    [Header("���� ����")]
    [SerializeField] private List<GameObject> listEnemys;
    [SerializeField, Range(0.1f, 1f)] private float timerSpawn; // ���� �����ð�
    private float timer = 0.0f;
    [SerializeField] bool boolEnemySpawn = false;
    [SerializeField] Transform trsSpawnPos;
    [SerializeField] Transform layerEnemy;
    [SerializeField] Transform layerDynamic;

    [Header("�����ۻ���")]
    [SerializeField, Range(0.0f, 100.0f)] float dropRate = 0.0f;
    [SerializeField] private List<GameObject> listitem;

    [Header("������")]
    [SerializeField] Slider slider;
    [SerializeField] TMP_Text sliderText;
    [SerializeField] Image sliderFillImage;

    [SerializeField] float bossSpawnTime = 60f;
    [SerializeField] float curtime = 0.0f;
    private bool bossSpawn = false;
    [SerializeField] GameObject objBoss;
    [SerializeField] Color SliderTimeColor;
    [SerializeField] Color SliderBossHpColor;

    [Header("����")]
    [SerializeField] TextMeshProUGUI ScoreText;
    private int curScore = 0;

    [Header("���ӿ����޴�")]
    [SerializeField] GameObject objGameOverMenu; //���ӿ����� ��µǴ� ������Ʈ
    [SerializeField] TMP_Text textRank; //���ӿ����� ��ũ
    [SerializeField] TMP_Text textTotalScore; //���ӿ����� ����
    [SerializeField] TMP_InputField iFName;//������ ������ �̸��� ����� ��ǲ�ʵ�
    [SerializeField] Button btnMainMenu;//���ι�ư ��ư
    [SerializeField] TMP_Text textBtnMainMenu;//���θ޴� ��ư �ؽ�Ʈ

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

        #region �˾Ƶθ� ����
        //string rank = ""; //10���� ������ ����

        //listScore.Add(new UserScore() { score = 100, name = "aaa" });
        //listScore.Add(new UserScore() { score = 90, name = "bbb" });
        //listScore.Add(new UserScore() { score = 80, name = "ccc" });

        ////���ξ� , ��
        //rank = JsonConvert.SerializeObject(listScore);
        //listScore.Clear();
        //listScore = JsonConvert.DeserializeObject<List<UserScore>>(rank);
        #endregion
    }

    private void checkGameOverMenu()
    {
        //if (objGameOverMenu.activeSelf == true) // �����ִ�
        //{
        //    objGameOverMenu.SetActive(false);//����
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
                    Debug.LogError($"����Ʈ ���ھ��� ������ �̻��մϴ�. \n����Ʈ ���ھ��� ���� = {listScore.Count}");
                }
                #region �˾Ƶθ�����
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
        #region �˾Ƶθ�����
        //string splitText = ",!_";//�̹��ڿ��� �����Ͱ� �������ٴ� ��
        //string testValue = "�����ٶ󸶹ٻ�";
        //testValue = testValue.Replace("{","");
        //testValue = testValue.Replace("}","");
        //testValue.sub

        //string[] splitValue = testValue.Split(splitText);
        #endregion
    }

    /// <summary>
    /// �����Ͱ� �ùٸ��� �ʰų� ������ ���ο� �����͸� ��Ȯ�� ������� ����� �����մϴ�.
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
    #region ���� ������
    /// <summary>
    /// ���� ���� üũ(Ȯ��)
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

    #region ���� ������
    /// <summary>
    /// ���� ������ Ÿ�̸�
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
        //������ ������ ��� ���� ����
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
        //�����⵿�� Ÿ�̸��÷�
        if (bossSpawn == false && sliderFillImage.color != SliderTimeColor)
        {
            sliderFillImage.color = SliderTimeColor;
        }
        //�����⵿������ Ÿ�̸��÷�
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

    #region �� ����
    /// <summary>
    /// �� ����
    /// </summary>
    private void enemySpawn()
    {
        int iRand = Random.Range(0, listEnemys.Count); // 0 , 1 , 2
        GameObject objEnemy = listEnemys[iRand]; //ref

        //var limitPosX = getLimitEnemy();
        (float _min, float _max) limitPosX = getLimitEnemy(); // y?
        float xPos = Random.Range(limitPosX._min, limitPosX._max);
        Vector3 instPos = new Vector3(xPos, trsSpawnPos.position.y, 0); // ���Ⱑ ������ ��ġ

        GameObject obj = Instantiate(objEnemy, instPos, Quaternion.identity, layerEnemy);
        Enemy objSc = obj.GetComponent<Enemy>();

        float rate = Random.Range(0.0f, 100.0f);
        if (rate <= dropRate)
        {
            objSc.SetHaveItem();
        }
    }
    #endregion

    #region ���� ������ ȭ�� ����
    /// <summary>
    /// ���� ������ ȭ�� ����
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
    /// ������ ���� ü�� �� �ִ�ü���� �������� ����մϴ�.
    /// </summary>
    /// <param name="_curHp">����ü��</param>
    /// <param name="_maxHp">�ִ�ü��</param>
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
        btnMainMenu.onClick.RemoveAllListeners(); //��� �̺�Ʈ�� ����

        if (rank == -1)//���������� ǥ���ϰ� ����ȭ������ �̵�
        {
            textRank.text = "������ ��";
            iFName.gameObject.SetActive(false);
            textBtnMainMenu.text = "���θ޴���";
            btnMainMenu.onClick.AddListener(()=>
            {
                onClickBtnMainMenu();
            });//���ڸ� �׳� �־�� ���۾ȵ�
        }
        else//�������� ����ߴ����� ǥ���ϰ� �̸��� ����� �ֵ�����
        {
            textRank.text = $"{rank + 1}��";
            iFName.text = string.Empty;//"";
            iFName.gameObject.SetActive(true);
            textBtnMainMenu.text = "������ ���θ޴���";
            btnMainMenu.onClick.AddListener(() =>
            {
                setNewRank(rank , iFName.text);
                onClickBtnMainMenu();
            });
        }
        objGameOverMenu.SetActive(true);
    }

    /// <summary>
    /// �� ����� ��Ͻ�Ű�� ���� ������ ��ũ �ϳ��� ����
    /// </summary>
    /// <param name="_rank">���Ե� ��ũ</param>
    /// <param name="_name">���� �Է��� �̸�</param>
    private void setNewRank(int _rank, string _name)
    {
        listScore.Insert(_rank, new UserScore() { name = _name, score = curScore });
        //11���� ����Ǿ�����
        listScore.RemoveAt(listScore.Count - 1); //������ ����� �����͸� ����


        string saveValue = JsonConvert.SerializeObject(listScore);
        PlayerPrefs.SetString(scoreKey, saveValue);
    }

    /// <summary>
    /// ���θ޴��� �̵�
    /// </summary>
    private void onClickBtnMainMenu()
    {
        //��������
        int count = listScore.Count;
        for(int i = 0; i < count; ++i)
        {
            UserScore uScore = listScore[i];
            Debug.Log($"{i+1}�� - Name = {uScore.name}, score = {uScore.score}");
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
                return iNum; //���� ����� ������ ������
            }
        }

        return -1;
    }

}
