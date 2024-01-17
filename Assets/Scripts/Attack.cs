using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    BoxCollider2D box;
    [SerializeField] private float Speed = 2f;
    [SerializeField] private float Damage = 3f;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }

    private void OnBecameInvisible()// ī�޶� ������ ���̴ٰ� �������ʰ� �� ���
    {
        Destroy(gameObject);
    }
}
