using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GongNon3 : MonoBehaviour
{
    [SerializeField]private int iNum = 0;
    [SerializeField]private int iNum2 = 0;
    // Start is called before the first frame update
    private void Start() //스크립트가 실행될때 단 한번만 실행되는 함수
    {
        //int a = 1;     // 1~9까지 출력
        //while (a <= 9)
        //{
        //    Debug.Log(a);
        //    a++;
        //}


        //int a = 9;   // 9~1까지 출력
        //while (a >= 1)
        //{
        //    Debug.Log(a);
        //    a--;
        //}


        //while (true) //구구단 만들기
        //{
        //}

        //Debug.Log(iNum);
        int TestValue = 100;
        StudyFunction("안녕하세요", TestValue);
    }

    /// <summary>
    /// 디버그를 출력하게 해주는 함수입니다.
    /// </summary>
    /// <param name="GongNon"></param>
    /// <param name="value2"></param>
        private void StudyFunction(string GongNon, int value2)
        {

            Debug.Log(GongNon);
        }
}
