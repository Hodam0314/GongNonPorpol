using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
    public void DoDestroyEvent()
    {
        Destroy(gameObject);
    }
}
