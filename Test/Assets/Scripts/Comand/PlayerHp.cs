using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    Transform trsPlayer; // 플레이어의 트랜스폼
    [SerializeField] private Image imgForntHp; // 실제 HP
    [SerializeField] private Image imgMidHp; // 연출용 HP



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
        checkPlayerHp(); // 만약 MidHP가 ForntHP와 값이 다르다면 같게 , 천천히
        isDestroying();
    }
    #region 플레이어를 따라다니는 HP 게이지
    /// <summary>
    /// 플레이어를 따라다니는 HP게이지
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

    #region 플레이어 HP 증가 및 감소 확인코드
    /// <summary>
    /// 플레이어 HP 증가 및 감소 확인코드
    /// </summary>
    private void checkPlayerHp()
    {
        float amountFront = imgForntHp.fillAmount;
        float amountMid = imgMidHp.fillAmount;

        if (amountFront < amountMid)//mid가 깍여야함.
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
