using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Camera maincam;


    private void Awake()
    {
        maincam = GetComponent<Camera>();
    }

    private void Start()
    {
        
    }

}
