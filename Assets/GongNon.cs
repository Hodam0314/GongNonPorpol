using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GongNon : MonoBehaviour
{
    // Start is called before the first frame update


    //< , > , == , != , <= , >=
    //&& And���� , ��� ������ ���϶�
    // || Or ���� : �� ������ �ϳ��� ���϶�

    private void Start() // ��ũ��Ʈ�� ����ɶ� �� �ѹ��� ����Ǵ� �Լ�

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
            Debug.Log("üĿ�� Ʈ�翴���ϴ�");
        }
        else if (playerHP > 40 && playerHP < 50) // if = ���࿡ ~�ϸ� �̶�� �����߰� ,  
        {
            Debug.Log("�÷��̾�� 10�� �������� �޾ҽ��ϴ�");
            playerHP -= 10;
        }
        else if (playerHP > 40) // ���� if���� Ʋ������� ����Ǵ� ��ũ��Ʈ
        {
            Debug.Log("�÷��̾��� ü���� 10��ŭ ȸ���Ǿ����ϴ�.");
            playerHP += 10;
        }
        else
        {
            Debug.Log("�÷��̾ �׾����ϴ�.");
            playerHP = 0;
        }
            Debug.Log($"�÷��̾��� ü�� = {playerHP}");
        }
}
