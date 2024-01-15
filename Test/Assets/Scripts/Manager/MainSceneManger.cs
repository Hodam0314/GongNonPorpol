using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static GameManager;
using Newtonsoft.Json;

public class MainSceneManger : MonoBehaviour
{
    [SerializeField] private Button btnGameStart;
    [SerializeField] private Button btnRank;
    [SerializeField] private Button btnexit;

    [SerializeField] private GameObject objRankContents; //��ư�� �������� ��ŷ������Ʈ�� ���ִ¿뵵;
    [SerializeField] private GameObject fabRankContents;

    [SerializeField] private Button btnExitRankContents;//��ŷ������Ʈ�� ����
    [SerializeField] private Transform trsContents;//��ŷ�����յ��� ����� ����

    private List<UserScore> listScore = new List<UserScore>();
    private string scoreKey = "scoreKey";


    private enum enumScenes
    {
        MainScene,
        PlayScene,
    }


    private void Awake()
    {
        #region ��ư����
        btnGameStart.onClick.AddListener(()=>
        {
            //���� ����
            //���ξ� -> �÷��̾����� ����
            SceneManager.LoadSceneAsync((int)enumScenes.PlayScene);
        });
        btnRank.onClick.AddListener(() =>
        {
            //��ŷ�� ���õ� ������Ʈ�� ���ش�
            objRankContents.SetActive(true);
        });
        btnexit.onClick.AddListener(() =>
        {
            //������ ������ ���¿����� ������ ��������
            //�����Ϳ��� ���ÿ��� �����͸� Stop���� ����
            //��ó�� => ������ ���� Ȥ�� �÷����Ҷ� ���ǿ� �������������� ���°�ó�� ó���ع����� "#if"
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();//���� ����
#endif
        });
        btnExitRankContents.onClick.AddListener(() =>
        {
            objRankContents.SetActive(false);
        });
        #endregion
    }

    private void initRanking()
    {
        clearAllRanking();
        setScore();//����Ʈ�ȿ� 10���� ������ ����
    }
    
    private void clearAllRanking()//��ũ ������Ʈ�� �־��ٸ� ����
    {
        int count = trsContents.childCount;
        for(int iNum = count -1; iNum > -1; --iNum)
        {
            Destroy(trsContents.GetChild(iNum).gameObject);
        }
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
                if (listScore.Count != 10)
                {
                    Debug.LogError($"����Ʈ ���ھ��� ������ �̻��մϴ�. \n����Ʈ ���ھ��� ���� = {listScore.Count}");
                }
            }
        }
        else
        {
            clearAllScore();
        }
    }
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

    private void createRankContents()
    {
        int Count = listScore.Count;
        for( int i = 0; i < Count; ++i)
        {
            UserScore data = listScore[i];

            GameObject obj = Instantiate(fabRankContents, trsContents);
            RankContents objsc = obj.GetComponent<RankContents>();
            objsc.SetRankContents($"{i + 1}", data.score.ToString("D8"), data.name);
        }
    }
}
