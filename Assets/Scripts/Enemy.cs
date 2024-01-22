using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Rigidbody2D rigid;
    BoxCollider2D boxCollider2D;
    private float verticalVelocity;

    [Header("�� ����")]
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

    private void moving()//������ �������� ����ϴ� �ڵ�
    {
        rigid.velocity = new Vector3(enemySpeed, rigid.velocity.y);
    }

    private void checkingGround()//���Ͱ� ���� ����ִ��� üũ����
    {
        if(checkGround.IsTouchingLayers(ground) == false)
        {
            turning();
        }
        
    }

    public void Hit(float _damage)//���Ͱ� �������� �Դ� �ڵ�
    {
        MobHp -= _damage;

        if (MobHp <= 0)
        {
            Instantiate(objBoom, transform.position, Quaternion.identity, TrashLayer);
            Destroy(gameObject);
        }
    }

    private void turning()//���̾ƴ� ������ ��������� ������ ��ȯ������
    {
        Vector3 trs = transform.localScale;
        trs.x *= -1;
        transform.localScale = trs;

        enemySpeed *= -1;
    }
}
