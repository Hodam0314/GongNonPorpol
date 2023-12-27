using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;

    void Update()
    {
        checkPlayer();
    }
    private void checkPlayer()
    {
        if( player != null)
        {
            Vector3 pos = player.transform.position;
            pos.z = -10;
            transform.position = pos;
        }
    }
}
