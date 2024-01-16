using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HelpManager : MonoBehaviour
{
    [SerializeField] Button btnNext;
    [SerializeField] Button btnBefore;
    [SerializeField] Button btnExit;
    [SerializeField] GameObject Page1; 
    [SerializeField] GameObject Page2; 
    [SerializeField] GameObject Page3; 


    private void Awake()
    {
        btnExit.onClick.AddListener(() =>
        {

            SceneManager.LoadSceneAsync((int)enumScene.StartScene);
            
        });

        btnBefore.onClick.AddListener(() =>
        {
            if(Page1.activeSelf == true)
            {
                return;
            }
            else if (Page2.activeSelf == true)
            {
                Page2.SetActive(false);
                Page1.SetActive(true);
            }
            else if (Page3.activeSelf == true)
            {
                Page3.SetActive(false);
                Page2.SetActive(true);
            }
        });

        btnNext.onClick.AddListener(() =>
        {
            if(Page1.activeSelf == true)
            {
                Page1.SetActive(false);
                if(Page2.activeSelf == false)
                {
                Page2.SetActive(true);
                }
            }
            else if (Page2.activeSelf == true)
            {
                Page2.SetActive(false);
                if(Page3.activeSelf == false)
                {
                Page3.SetActive(true);
                }
            }
            else if (Page3.activeSelf == true)
            {
                return;
            }
        });

    }


}
