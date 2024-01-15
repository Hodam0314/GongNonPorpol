using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class GongNon4 : MonoBehaviour
{
    // Start is called before the first frame update

    //private void subject1()
    //{
    //    for (int iNum = 9; iNum > 1; iNum--)
    //    {
    //        function(iNum);
    //    }
    //}

    //private void function(int _n)
    //{
    //    for (int iNum = 1; iNum < 10; ++iNum)
    //    {
    //        Debug.Log($"{_n} x {iNum} = {_n * iNum}");
    //    }
    //}
    //private void Start()
    //{
    //    subject2();
    //}
    //private void subject1()
    //{
    //    for (int iNum = 9; iNum > 1; iNum--)
    //    {
    //        function(iNum);
    //    }
    //}
    //private void subject2()
    //{
    //    int aaa = 1;
    //    for (int iNum = 7; iNum >0; iNum--) 
    //    {
    //        aaa *= -1;
    //        Debug.Log($"aaa = {aaa}, result = {aaa * iNum}");
    //    }
    //}
    //private void function(int _n)
    //{
    //    for (int iNum = 1; iNum < 10; ++iNum)
    //    {
    //        Debug.Log($"{_n} x {iNum} = {_n * iNum}");
    //    }
    //}

    //private void Awake() //스타트보다 먼저 실행됨
    //{
    //    //이 스크립트 내에서 정의해야하는 자기자신 데이터를 위해서 자주 사용
    //}

    private void Start()
    {
        //int[] GongNon = new int[7] { 1, 2, 3, 4, 5, 6, 7 } ; // 0 ~ 6 숫자는 항상 0부터 시작

        //GongNon = new int[10]; // 0 ~ 4까지 저장 새로운 크기 변경시 기존에 설정해둔 데이터값 삭제
        //for (int iNum = 0; iNum < GongNon.Length; ++iNum)
        //{
        //    Debug.Log(GongNon[iNum]);
        //}


        //int[] points = new int[] { 83, 99, 52, 93, 15 };
        //85점 이상인 자료형만 디버그로 출력하는 함수를 만들어 보세요
        //for (int a = 0; a < points.Length; a++)
        //{
        //    if (points[a] >= 85)
        //    {
        //        Debug.Log(points[a]);
        //    }
        //}        
        //int b = 0;
        //for (int a = 0; a < 5; a++)
        //{
        //    if (points[a] >= a)
        //    {
        //        b += points[a];
        //    }
        //}
        //b /= points.Length;
        //Debug.Log(b);

        int[] points = new int[] { 83, 99, 52, 93, 15 };
        int[] points2 = new int[] { 83, 99, 52 };
        int[] points3 = new int[] { 83, 99, 52, 93, 15, 55, 86 };
        int[] points4 = new int[0];

        Debug.Log(points);
    }
    //    overpoint(points, 85);
    //    overpoint(points2, 90);
    //    overpoint(points3, 55);

    //}
    //private void overpoint(int[] _value, int _target)
    //{
    //    if (_target < 0 || _target > 100)
    //    {
    //        Debug.Log($"점수가 올바르지 않습니다. 입력된 타겟 점수는 {_target}이었습니다.");
    //        return;
    //    }
    //    bool find = false;
    //}
}


