using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GongNon3 : MonoBehaviour
{
    [SerializeField]private int iNum = 0;
    [SerializeField]private int iNum2 = 0;
    // Start is called before the first frame update
    private void Start() //��ũ��Ʈ�� ����ɶ� �� �ѹ��� ����Ǵ� �Լ�
    {
        //int a = 1;     // 1~9���� ���
        //while (a <= 9)
        //{
        //    Debug.Log(a);
        //    a++;
        //}


        //int a = 9;   // 9~1���� ���
        //while (a >= 1)
        //{
        //    Debug.Log(a);
        //    a--;
        //}


        //while (true) //������ �����
        //{
        //}

        //Debug.Log(iNum);
        int TestValue = 100;
        StudyFunction("�ȳ��ϼ���", TestValue);
    }

    /// <summary>
    /// ����׸� ����ϰ� ���ִ� �Լ��Դϴ�.
    /// </summary>
    /// <param name="GongNon"></param>
    /// <param name="value2"></param>
        private void StudyFunction(string GongNon, int value2)
        {

            Debug.Log(GongNon);
        }
}
