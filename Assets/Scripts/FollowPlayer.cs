using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;

    private void Update()
    {
        checkPlayer();
    }
    private void checkPlayer()
    {
        if (player != null)
        {
            Vector3 pos = player.transform.position;
            pos.y += 2.5f;
            transform.position = pos;
        }
    }
}
