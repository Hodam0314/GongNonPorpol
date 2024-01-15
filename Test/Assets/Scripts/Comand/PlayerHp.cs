using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    Transform trsPlayer; // �÷��̾��� Ʈ������
    [SerializeField] private Image imgForntHp; // ���� HP
    [SerializeField] private Image imgMidHp; // ����� HP



    private void Start()
    {
        GameManager manager = GameManager.Instance;
        GameObject obj = manager.GetPlayerGameObject();
        Player objSc = obj.GetComponent<Player>();
        objSc.SetPlayerHp(this);
        trsPlayer = obj.transform;
    }

    private void Update()
    {
        checkPlayerPos();
        checkPlayerHp(); // ���� MidHP�� ForntHP�� ���� �ٸ��ٸ� ���� , õõ��
        isDestroying();
    }
    #region �÷��̾ ����ٴϴ� HP ������
    /// <summary>
    /// �÷��̾ ����ٴϴ� HP������
    /// </summary>
    private void checkPlayerPos()
    {
        if(trsPlayer == null)
        {
            return;
        }

        transform.position = trsPlayer.position - new Vector3(0, 0.65f, 0);

    }
    #endregion

    #region �÷��̾� HP ���� �� ���� Ȯ���ڵ�
    /// <summary>
    /// �÷��̾� HP ���� �� ���� Ȯ���ڵ�
    /// </summary>
    private void checkPlayerHp()
    {
        float amountFront = imgForntHp.fillAmount;
        float amountMid = imgMidHp.fillAmount;

        if (amountFront < amountMid)//mid�� �￩����.
        {
            imgMidHp.fillAmount -= Time.deltaTime * 0.5f;
            if (imgMidHp.fillAmount <= imgForntHp.fillAmount)
            {
                imgMidHp.fillAmount = imgForntHp.fillAmount;
            }
            else if (amountFront > amountMid)
            {
                imgMidHp.fillAmount = imgForntHp.fillAmount;
            }
        }
    }
    #endregion


    private void isDestroying()
    {
        if (imgMidHp.fillAmount <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetPlayerHp(float _curHp, float _maxHp)
    {
        imgForntHp.fillAmount = (float)_curHp / _maxHp;
    }
}
