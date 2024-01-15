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

    private void Start() //단 한번만 동작하는 함수
    {
        //축소 저장용 = 4byte
        //SpriteRender sprBottom = transform.GetComponentInChildren<SpriteRenderer>(); // InChildren = 나의 자식으로부터 자료를 찾아옴 But 이름으로 찾는게 아니기때문에 필요없는걸 찾아올수도있음.
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

    private void Update() //프레임마다 호출되는 함수
    {
        Vector2 vecBottom = matBottom.mainTextureOffset;// x, y , z 등의 좌표를 저장할때 쓰는 함수 = Vector 갯수에따라 Vector2 , Vector3 뒤에 숫자가 증가. Vector2 = (float , float)
        Vector2 vecMiddle = matMiddle.mainTextureOffset;
        Vector2 vecTop = matTop.mainTextureOffset;

        vecBottom += new Vector2(0, speedBottom * Time.deltaTime); // Time.deltaTime = 어디서든 동일한 프레임을 가지게해줌.
        vecMiddle += new Vector2(0, speedMiddle * Time.deltaTime);
        vecTop += new Vector2(0, speedTop * Time.deltaTime);

        vecBottom.y = Mathf.Repeat(vecBottom.y, 1.0f);
        vecMiddle.y = Mathf.Repeat(vecMiddle.y, 1.0f);
        vecTop.y = Mathf.Repeat(vecTop.y, 1.0f);

        matBottom.mainTextureOffset = vecBottom;
        matMiddle.mainTextureOffset = vecMiddle;
        matTop.mainTextureOffset = vecTop;

        //Mathf.Repeat(vecBottom.y, 1.0f); //Mathf = 수학적인 함수들을 가지고있다.

    }
}
