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
        //string Value = "HelloWorld";//10글자
        //char[];
        //특정 문자열을 삭제하고 올바로 출력될수 있도록 string 자료형을 만드세요
        //l삭제해서 문자를 만드세요
        //toString
        //HeoWord;

        //swap
        //char[];
        //이 문자가 거꾸로 나오도록 정리하고 출력될수 있도록 string 자료형을 만드세요
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
        //이중 배열, 이차원배열
        // 0, 1, 2
        // 3, 4, 5
        //->
        // 3, 4, 5
        // 0, 1, 2

        //debug로 출력 할때는
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

        //오름차순으로 정렬
        //정렬하는 함수쓰면 안됨

        //int[][] arrins = new int[3][];
        //arrins[0] = new int[2];
        //arrins[1] = new int[2];
        //arrins[2] = new int[2];
        //arrins[0][0] = 10;
    }
}
