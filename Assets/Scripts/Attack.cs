using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Rigidbody2D rigid;
    BoxCollider2D box;
    Player player;

    private float bulletTime = 0f;
    private float TimeLimit = 4f;

    [SerializeField] private float Speed = 2f;
    [SerializeField] private float Damage = 1f;

    private void Awake()
    {
        player = GetComponent<Player>();
        rigid = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        turnBullet();
        bullet();
    }

    private void turnBullet()
    {
        
    }

    private void bullet() //�Ѿ��� ������ ���ư����ֵ��� ������ִ� �ڵ�
    {
        Vector3 pos = new Vector3(Speed, 0, 0);
        transform.position +=  pos * Time.deltaTime;
        bulletTime += Time.deltaTime;
        if (bulletTime > TimeLimit)
        {
            Destroy(gameObject);
            bulletTime = 0f;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == GameTag.Mob.ToString())
        {
            Enemy enemySc = collision.GetComponent<Enemy>();
            enemySc.Hit(Damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == GameTag.FlyingMob.ToString())
        {
            Enemy enemySc = collision.GetComponent<Enemy>();
            enemySc.Hit(Damage);
            Destroy(gameObject);
        };
    }

}
