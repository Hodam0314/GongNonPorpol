using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

//public class Monster // Ŭ������ ����
//{
//    private int iExp = 10;
//    private float fHP = 20.0f;
//    private float fDamege = 5;
//    private int iLevel = 10;
//    private float fDefencef = 10;
//    private float fSpeed = 5;
//}

public class GongNon6 : MonoBehaviour
{

    private class Monster // Ŭ������ ����
    {
        public Monster(string _name) 
        {
            sName = "NoName";
            iExp = 10;
            fHp = 20;
        }
        public Monster(string _name, int _Exp, float _fHp) // ������
        {
            sName = _name;
            iExp = _Exp;
            fHp = _fHp;
        }

        //public Monster(string _name, int _Exp, float _fHp) // ������
        //{
        //    sName = _name;
        //}

        ~Monster() // �Ҹ��� ( �� ��� x , �̷��� �ִ� ������ �˾ƵѰ� )
        {
            Debug.Log($"{sName} �����Ͱ� �����Ǿ����ϴ�.");
        }


        private string sName;
        private int iExp = 10;
        private float fHp = 20.0f;
        private float fDamege = 5;
        private int iLevel = 10;
        private float fDefencef = 10;
        private float fSpeed = 5;

        public void functionGetExp()
        {
            Debug.Log($"{sName} ������ ����ġ�� {iExp}�Դϴ�.");
        }
        public void functionSetExp(int _iExp)
        {
            iExp = _iExp;
        }
    }
    
    private Monster Orc = new Monster("��ũ"); //������ �׻� private�� ���
    private Monster Gremlin = new Monster("�׷���");
    private Monster Dragon = new Monster("�巡��");
    private Monster Slime = new Monster("������");    
    
    //private Monster Gremlin = new Monster(); //����
    //private Monster Dragon = new Monster(); //����
    //private Monster Slime = new Monster(); //����

    private void Start()
    {
        Orc.functionGetExp();
        Orc = null;

        //Orc.functionSetExp(10);
        //Gremlin.functionSetExp(5);
        //Dragon.functionSetExp(100);

        //Debug.Log(nameof(Orc));
        //Orc.functionGetExp();
        //Debug.Log(nameof(Gremlin));
        //Gremlin.functionGetExp();
        //Debug.Log(nameof(Dragon));
        //Dragon.functionGetExp();


        ////�ݹ��̺���
        //int value = 999;
        //Debug.Log(value);

        ////�ݹ��̷��۷���
        //Debug.Log(Orc.GetType());
        //Debug.Log(nameof(Orc));


        //Ex - �ݹ��� ���۷��� 
        //Orc.functionSetExp(100); // ��ũ�� ����ġ 100���� ����
        //Gremlin = Orc; //�ּҰ� ����
        //Gremlin.functionGetExp(); // �׷��� ����ġ Ȯ��

        //Orc.functionSetExp(1); // ��ũ�� ����ġ 1�� ����
        //Gremlin.functionGetExp(); // �׷��� ����ġ Ȯ��

        //�ݹ��� ���۷����� �ּҼ��� �����Ͱ� ����Ǹ� �ּҿ� �����Ͱ� �ԷµǹǷ�
        //��� �����ּҸ� ȣ���ϴ� �����ʹ� ���� �����͸� �����

    }

    // Ex 
    // �����ε� = ���� �̸��� �Լ��� ��������� �Ű������� �޸��ϴ°�
    public int Sum(int _a, int _b)
    {
        return _a + _b;
    }
    
    public int Sum(int _a, int _b, int _c)
    {
        return _a + _b + _c;
    }


}
