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

    //private void Awake() //��ŸƮ���� ���� �����
    //{
    //    //�� ��ũ��Ʈ ������ �����ؾ��ϴ� �ڱ��ڽ� �����͸� ���ؼ� ���� ���
    //}

    private void Start()
    {
        //int[] GongNon = new int[7] { 1, 2, 3, 4, 5, 6, 7 } ; // 0 ~ 6 ���ڴ� �׻� 0���� ����

        //GongNon = new int[10]; // 0 ~ 4���� ���� ���ο� ũ�� ����� ������ �����ص� �����Ͱ� ����
        //for (int iNum = 0; iNum < GongNon.Length; ++iNum)
        //{
        //    Debug.Log(GongNon[iNum]);
        //}


        //int[] points = new int[] { 83, 99, 52, 93, 15 };
        //85�� �̻��� �ڷ����� ����׷� ����ϴ� �Լ��� ����� ������
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
    //        Debug.Log($"������ �ùٸ��� �ʽ��ϴ�. �Էµ� Ÿ�� ������ {_target}�̾����ϴ�.");
    //        return;
    //    }
    //    bool find = false;
    //}
}


