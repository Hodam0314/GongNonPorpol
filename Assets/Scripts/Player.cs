using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    Camera mainCam;

    [Header("�÷��̾� ����")]
    [SerializeField] private float moveSpeed = 5.0f;
    Vector3 moveDir;

    [Header("�߷� �� ����")]
    private float gravity = 9.81f;
    [SerializeField]private bool isGround = false;
    

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        moving();
        checkGravity();
    }
    private void moving()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        moveDir.y = rigid.velocity.y;
        rigid.velocity = moveDir;
    }
    
    private void checkGravity()
    {

    }
}
