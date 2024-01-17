using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Rigidbody2D rigid;
    BoxCollider2D boxCollider2D;
    private float verticalVelocity;
    private float curHp = 0f;

    [Header("Àû ¼³Á¤")]
    [SerializeField] private float MobHp = 5f;
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
        curHp = MobHp;
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
