using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Rigidbody2D rigid;
    BoxCollider2D boxCollider2D;
    private float verticalVelocity;

    [Header("지상형 몬스터")]
    [SerializeField] private float mobMaxHp = 5.0f;
    [SerializeField] private float mobDamage = 2f;
    private float curmobHp = 0f;

    [Header("비행형 몬스터")]
    [SerializeField] private float flyingmobMaxHp = 10.0f;
    [SerializeField] private float flyingmobDamage = 3f;



    [Header("적 설정")]
    [SerializeField] private float enemySpeed = 1f;
    [SerializeField] private bool isGround = false;
    [SerializeField] BoxCollider2D checkGround;
    [SerializeField] LayerMask ground;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == GameTag.Player.ToString())
        {
            turning();
        }
    }

    private void Awake()
    {

        rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        moving();
        checkingGround();
    }

    private void moving()
    {
        rigid.velocity = new Vector3(enemySpeed, rigid.velocity.y);
    }

    private void checkingGround()
    {
        if(checkGround.IsTouchingLayers(ground) == false)
        {
            turning();
        }
        
    }

    private void turning()
    {
        Vector3 trs = transform.localScale;
        trs.x *= -1;
        transform.localScale = trs;

        enemySpeed *= -1;
    }
}
