using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Rigidbody2D rigid;
    BoxCollider2D boxCollider2D;
    private float verticalVelocity;

    [Header("적 설정")]
    [SerializeField] private float MobHp = 5f;
    [SerializeField] private float enemySpeed = 1f;
    [SerializeField] private bool isGround = false;
    [SerializeField] BoxCollider2D checkGround;
    [SerializeField] LayerMask ground;
    [SerializeField] Sprite sprHit;

    [SerializeField] GameObject objBoom;
    [SerializeField] Transform TrashLayer;

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

    private void moving()//몬스터의 움직임을 담당하는 코드
    {
        rigid.velocity = new Vector3(enemySpeed, rigid.velocity.y);
    }

    private void checkingGround()//몬스터가 땅을 밟고있는지 체크해줌
    {
        if(checkGround.IsTouchingLayers(ground) == false)
        {
            turning();
        }
        
    }

    public void Hit(float _damage)//몬스터가 데미지를 입는 코드
    {
        MobHp -= _damage;

        if (MobHp <= 0)
        {
            Instantiate(objBoom, transform.position, Quaternion.identity, TrashLayer);
            Destroy(gameObject);
        }
    }

    private void turning()//땅이아닌 지점에 가까워지면 방향을 전환시켜줌
    {
        Vector3 trs = transform.localScale;
        trs.x *= -1;
        transform.localScale = trs;

        enemySpeed *= -1;
    }
}
