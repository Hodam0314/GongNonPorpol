using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    Player player;
    BoxCollider2D box;
    [SerializeField] private float timerGod = 2f;
    private float timer = 0.0f;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        box = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        God();
    }

    private void God()
    {
        if(box.isTrigger == false)
        {
            timer += Time.deltaTime;
            if(timer > timerGod)
            {
                box.isTrigger = true;
                timer = 0.0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == GameTag.Mob.ToString())
        {
            player.Hit(5.0f);
            box.isTrigger = false;
        }
        else if (collision.tag == GameTag.FlyingMob.ToString())
        {
            player.Hit(10.0f);
            box.isTrigger = false;
        }
    }
}
