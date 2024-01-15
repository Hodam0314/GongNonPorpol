using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float timeDestroy = 0.5f;
    private float damage = 0.0f;
    private bool playerBullet = false; // �÷��̾ �� �Ѿ��̷��� true

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(playerBullet == true && collision.gameObject.tag == GameTag.Enemy.ToString())
        {
            Enemy enemySc = collision.GetComponent<Enemy>();
            enemySc.Hit(damage);
            Destroy(gameObject);
        }
        else if (playerBullet == false && collision.gameObject.tag == GameTag.Player.ToString())
        {
            Player playerSc = collision.GetComponent<Player>();
            playerSc.Hit(damage);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()// ī�޶� ������ ���̴ٰ� �������ʰ� �� ���
    {
        Destroy(gameObject);
    }

    //private void Start()
    //{
    //    Destroy(gameObject, timeDestroy);
    //}


    void Update()
    {
        //vector3(0,1,0) ����
        //transform.position += new Vector3(0, 1 , 0f) * Time.deltaTime * speed;
        transform.position += transform.up * Time.deltaTime * speed; // ������Ʈ�� ���� ���� (��) �� �̵��ϰ� �������ش� , (������Ʈ�� ȸ���ص� ȸ���� ���� ���� ����)
    }
    public void SetDamage(bool _isPlayer, float _damage, float _speed = -1)
    {
        playerBullet = _isPlayer;
        damage = _damage;
        if(_speed != -1)
        {
            speed = _speed;
        }
    }

}
