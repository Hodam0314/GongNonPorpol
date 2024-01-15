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

    [SerializeField] private GameObject objRankContents; //버튼이 눌러지면 랭킹오브젝트를 켜주는용도;
    [SerializeField] private GameObject fabRankContents;

    [SerializeField] private Button btnExitRankContents;//랭킹오브젝트를 꺼줌
    [SerializeField] private Transform trsContents;//랭킹프리팹들이 저장될 공간

    private List<UserScore> listScore = new List<UserScore>();
    private string scoreKey = "scoreKey";


    private enum enumScenes
    {
        MainScene,
        PlayScene,
    }


    private void Awake()
    {
        #region 버튼설정
        btnGameStart.onClick.AddListener(()=>
        {
            //씬이 변경
            //메인씬 -> 플레이씬으로 변경
            SceneManager.LoadSceneAsync((int)enumScenes.PlayScene);
        });
        btnRank.onClick.AddListener(() =>
        {
            //랭킹에 관련된 오브젝트를 켜준다
            objRankContents.SetActive(true);
        });
        btnexit.onClick.AddListener(() =>
        {
            //게임을 빌드한 상태에서는 게임을 종료해줌
            //에디터에서 사용시에는 에디터를 Stop으로 변경
            //전처리 => 게임을 빌드 혹은 플레이할때 조건에 부합하지않으면 없는것처럼 처리해버린다 "#if"
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();//게임 종료
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
        setScore();//리스트안에 10개의 데이터 저장
    }
    
    private void clearAllRanking()//랭크 오브젝트가 있었다면 삭제
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
                    Debug.LogError($"리스트 스코어의 갯수가 이상합니다. \n리스트 스코어의 갯수 = {listScore.Count}");
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
