using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

//public class Monster // 클래스를 정의
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

    private class Monster // 클래스를 정의
    {
        public Monster(string _name) 
        {
            sName = "NoName";
            iExp = 10;
            fHp = 20;
        }
        public Monster(string _name, int _Exp, float _fHp) // 생성자
        {
            sName = _name;
            iExp = _Exp;
            fHp = _fHp;
        }

        //public Monster(string _name, int _Exp, float _fHp) // 생성자
        //{
        //    sName = _name;
        //}

        ~Monster() // 소멸자 ( 잘 사용 x , 이런게 있다 정도만 알아둘것 )
        {
            Debug.Log($"{sName} 데이터가 삭제되었습니다.");
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
            Debug.Log($"{sName} 몬스터의 경험치는 {iExp}입니다.");
        }
        public void functionSetExp(int _iExp)
        {
            iExp = _iExp;
        }
    }
    
    private Monster Orc = new Monster("오크"); //변수는 항상 private로 사용
    private Monster Gremlin = new Monster("그램린");
    private Monster Dragon = new Monster("드래곤");
    private Monster Slime = new Monster("슬라임");    
    
    //private Monster Gremlin = new Monster(); //변수
    //private Monster Dragon = new Monster(); //변수
    //private Monster Slime = new Monster(); //변수

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


        ////콜바이벨류
        //int value = 999;
        //Debug.Log(value);

        ////콜바이레퍼런스
        //Debug.Log(Orc.GetType());
        //Debug.Log(nameof(Orc));


        //Ex - 콜바이 레퍼런스 
        //Orc.functionSetExp(100); // 오크의 경험치 100으로 수정
        //Gremlin = Orc; //주소가 복제
        //Gremlin.functionGetExp(); // 그램린 경험치 확인

        //Orc.functionSetExp(1); // 오크의 경험치 1로 수정
        //Gremlin.functionGetExp(); // 그램린 경험치 확인

        //콜바이 레퍼런스는 주소속의 데이터가 변경되면 주소에 데이터가 입력되므로
        //모든 같은주소를 호출하는 데이터는 같은 데이터를 출력함

    }

    // Ex 
    // 오버로딩 = 같은 이름의 함수를 사용하지만 매개변수를 달리하는것
    public int Sum(int _a, int _b)
    {
        return _a + _b;
    }
    
    public int Sum(int _a, int _b, int _c)
    {
        return _a + _b + _c;
    }


}
