using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GongNon : MonoBehaviour
{
    // Start is called before the first frame update


    //< , > , == , != , <= , >=
    //&& And연산 , 모든 조건이 참일때
    // || Or 연산 : 두 조건중 하나라도 참일때

    private void Start() // 스크립트가 실행될때 단 한번만 실행되는 함수

    {
        int playerHP = 45;
        bool checker = false;

        GameObject objPlayer = null;

        bool Test = playerHP > 40;

        if (objPlayer != null && objPlayer)
        {
            objPlayer.SetActive(true);
        }

        if (checker == true)
        {
            Debug.Log("체커가 트루였습니다");
        }
        else if (playerHP > 40 && playerHP < 50) // if = 만약에 ~하면 이라는 조건추가 ,  
        {
            Debug.Log("플레이어는 10의 데미지를 받았습니다");
            playerHP -= 10;
        }
        else if (playerHP > 40) // 위의 if문이 틀렸을경우 실행되는 스크립트
        {
            Debug.Log("플레이어의 체력이 10만큼 회복되었습니다.");
            playerHP += 10;
        }
        else
        {
            Debug.Log("플레이어가 죽었습니다.");
            playerHP = 0;
        }
            Debug.Log($"플레이어의 체력 = {playerHP}");
        }
}
