using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GongNon5 : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        //string Value = "HelloWorld";//10����
        //char[];
        //Ư�� ���ڿ��� �����ϰ� �ùٷ� ��µɼ� �ֵ��� string �ڷ����� ���弼��
        //l�����ؼ� ���ڸ� ���弼��
        //toString
        //HeoWord;

        //swap
        //char[];
        //�� ���ڰ� �Ųٷ� �������� �����ϰ� ��µɼ� �ֵ��� string �ڷ����� ���弼��
        //dlroWolleH

        //int aaa = 10;
        //int bbb = 20;
        //int ccc = aaa;

        //aaa = bbb;
        //bbb = ccc;

        //Debug.Log($"{aaa}, {bbb}");


        //int[,] arr2lnt = new int[2, 3] { { 0, 1, 2 }, { 3, 4, 5 } };
        //int value = arr2lnt[0, 0];

        //arr2lnt[0, 0] = arr2lnt[1, 0];
        //arr2lnt[1, 0] = value;
        //string Temp = "";
        //string AAA = "";
        //for (int a = 0; a < 3; a++)
        //{
        //    Temp += arr2lnt[0, a];
        //    AAA += arr2lnt[1, a];
        //    if ( a < 2)
        //    {
        //        AAA += ",";
        //        Temp += ",";
        //    }
        //}
        //Debug.Log(AAA);
        //Debug.Log(Temp);






        //Debug.Log(arr2lnt.GetLength(0));
        //Debug.Log(arr2lnt.GetLength(1));
        //���� �迭, �������迭
        // 0, 1, 2
        // 3, 4, 5
        //->
        // 3, 4, 5
        // 0, 1, 2

        //debug�� ��� �Ҷ���
        //3, 4, 5
        //0, 1, 2

        //int[,] arr2lnt = new int[2, 3] { { 0, 1, 2 }, { 3, 4, 5 } };
        //string Non = "";
        //string Nn = "";

        //for (int a = 0; a < 3; a++)
        //{
        //    int Gong = arr2lnt[0, a];
        //    arr2lnt[0, a] = arr2lnt[1, a];
        //    arr2lnt[1, a] = Gong;
        //    Non += Gong;
        //    Nn += arr2lnt[0, a];
        //    if (a < 2)
        //    {
        //        Non += ",";
        //        Nn += ",";
        //    }
        //}
        //Debug.Log($"{Nn}\n{Non}");

        int[] arrint = { 0, 9, 7, 2, 1, 3, 4, 5, 8, 6 };
        string Gong = "";
        for (int a = 0; a < 9; a++)
        {
            for (int b = 1; b <9; b++)
            {
                if (arrint[a] > arrint[b] )
                {
                    arrint[b] = arrint[a];
                    Gong += arrint[a];
                }
            }

        }
        Debug.Log(Gong);

        //������������ ����
        //�����ϴ� �Լ����� �ȵ�

        //int[][] arrins = new int[3][];
        //arrins[0] = new int[2];
        //arrins[1] = new int[2];
        //arrins[2] = new int[2];
        //arrins[0][0] = 10;
    }
}
