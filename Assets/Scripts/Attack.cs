using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Rigidbody2D rigid;
    Enemy enemy;
    BoxCollider2D box;
    [SerializeField] private float Speed = 2f;
    [SerializeField] private float Damage = 3f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
        box = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        bullet();
    }

    private void bullet() //총알이 앞으로 날아갈수있도록 만들어주는 코드
    {
        Vector3 pos = new Vector3(Speed, 0, 0);
        rigid.velocity = pos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == GameTag.Mob.ToString())
        {
            enemy.Hit(Damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == GameTag.FlyingMob.ToString())
        {
            enemy.Hit(Damage);
            Destroy(gameObject);
        };
    }

}
