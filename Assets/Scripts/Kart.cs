using UnityEngine;

// 내가 지정한 방향으로 이동하고 싶다.
// Position의 값이 바뀌어야한다.
public class Kart : MonoBehaviour
{
    // string : "내용", int : 정수(소수점 X)
    // float : 실수(소수점 O), Vector3 : 벡터(x,y,z)

    public Vector3 dir = new Vector3(0, 0, 1);
    public float speed = 1.0f;  // 이동 속력 m/s

    public float jumpPower = 5f; // 점프(수직)할 때 힘
    public bool isGrounded = false; // boolean : true(1) or false(0) 땅에 닿았을때에만 jump해라!
    public float gravity = -9.81f; // 중력
    public float yVelocity = 0;  // y의 변화

    CharacterController controller;     //변수선언 : 빈 그릇

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();  // 그릇에 데이터를 담기.
    }

    // Update is called once per frame
    void Update()
    {
        //내가 입력한 방향으로 이동하고 싶다!
        float h = Input.GetAxis("Horizontal"); // a 나 d 를 누를 때(a 누르면 -1, d 누르면 +1)
        float v = Input.GetAxis("Vertical"); // s 나 w 를 누를 때(s 누르면 -1, w 누르면 +1)

        dir = new Vector3(h, 0, v); // 아래 dir.Normalize()를 꼭 써줘야한다.
        dir.Normalize(); // 정규화 Normalizer (방향을 유지하면서 벡터의 길이를 1로 고정함)

        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        dir.Normalize();

        // 이동하는 중에 점프하고 싶다!

        // 지금 Player(캐릭터컨트롤러)가 바닥에 닿아있다면,
        if (controller.collisionFlags == CollisionFlags.Below)
        {
            isGrounded = true; // 바닥에 닿으면
            yVelocity = 0; // 아래로 못내려가게 해라 (속도 => 0)
        }
        // 바닥에 닿아있는게 맞고, 그 상태에서 점프키를 누른게 맞다면, 
        if (isGrounded == true && Input.GetButtonDown("Jump"))
        {
            yVelocity = jumpPower;
            isGrounded = false; // 바닥에 닿은게 아니면
        }
        yVelocity = yVelocity + gravity * Time.deltaTime; // 중력값을 받게 해라.
        dir.y = yVelocity;

        //위치를 계속해서 바꾼다.
        //P = p0 + v(방향,길이) * t(시간)
        //transform.position = transform.position + dir * speed * Time.deltaTime;
        controller.Move(dir * speed * Time.deltaTime);  // CaracterController 쓸때는 꼭 써야함.
        // transform.position += dir;
        // transform.Translate(dir * speed * Time.deltaTime);
        // 내가 지정한 방향으로 이동하고 싶다!
    }
}