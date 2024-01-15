using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    private Material matBottom;
    private Material matMiddle;
    private Material matTop;

    [SerializeField] private float speedBottom;
    [SerializeField] private float speedMiddle;
    [SerializeField] private float speedTop;

    private void Start() //�� �ѹ��� �����ϴ� �Լ�
    {
        //��� ����� = 4byte
        //SpriteRender sprBottom = transform.GetComponentInChildren<SpriteRenderer>(); // InChildren = ���� �ڽ����κ��� �ڷḦ ã�ƿ� But �̸����� ã�°� �ƴϱ⶧���� �ʿ���°� ã�ƿü�������.
        //SpriteRender sprBottom = transform.GetComponentInParent<SpriteRenderer>(); // InParent =

        Transform trsBottom = transform.Find("SpriteBottom");
        SpriteRenderer sprBottom = trsBottom.GetComponent<SpriteRenderer>();
        matBottom = sprBottom.material;

        Transform trsMiddle = transform.Find("SpriteMid");
        SpriteRenderer sprMiddle = trsMiddle.GetComponent<SpriteRenderer>();
        matMiddle = sprMiddle.material;

        Transform trsTop = transform.Find("SpriteTop");
        SpriteRenderer sprTop = trsTop.GetComponent<SpriteRenderer>();
        matTop = sprTop.material;
    }

    private void Update() //�����Ӹ��� ȣ��Ǵ� �Լ�
    {
        Vector2 vecBottom = matBottom.mainTextureOffset;// x, y , z ���� ��ǥ�� �����Ҷ� ���� �Լ� = Vector ���������� Vector2 , Vector3 �ڿ� ���ڰ� ����. Vector2 = (float , float)
        Vector2 vecMiddle = matMiddle.mainTextureOffset;
        Vector2 vecTop = matTop.mainTextureOffset;

        vecBottom += new Vector2(0, speedBottom * Time.deltaTime); // Time.deltaTime = ��𼭵� ������ �������� ����������.
        vecMiddle += new Vector2(0, speedMiddle * Time.deltaTime);
        vecTop += new Vector2(0, speedTop * Time.deltaTime);

        vecBottom.y = Mathf.Repeat(vecBottom.y, 1.0f);
        vecMiddle.y = Mathf.Repeat(vecMiddle.y, 1.0f);
        vecTop.y = Mathf.Repeat(vecTop.y, 1.0f);

        matBottom.mainTextureOffset = vecBottom;
        matMiddle.mainTextureOffset = vecMiddle;
        matTop.mainTextureOffset = vecTop;

        //Mathf.Repeat(vecBottom.y, 1.0f); //Mathf = �������� �Լ����� �������ִ�.

    }
}
