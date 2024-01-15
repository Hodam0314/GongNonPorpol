using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

    public enum enumScene
{
    GameScene,
    StartScene,
    HelpScene,
}
public class ScenceManager : MonoBehaviour
{


    [SerializeField] Button btnStart;
    [SerializeField] Button btnHelp;
    [SerializeField] Button btnEnd;
    void Awake()
    {
        btnStart.onClick.AddListener(() =>
        {

            SceneManager.LoadSceneAsync((int)enumScene.GameScene);
            
        });

        btnHelp.onClick.AddListener(() =>
        {

            SceneManager.LoadSceneAsync((int)enumScene.HelpScene);
        });

        btnEnd.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        });

    }


}

