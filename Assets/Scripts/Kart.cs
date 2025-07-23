using UnityEngine;

// ���� ������ �������� �̵��ϰ� �ʹ�.
// Position�� ���� �ٲ����Ѵ�.
public class Kart : MonoBehaviour
{
    // string : "����", int : ����(�Ҽ��� X)
    // float : �Ǽ�(�Ҽ��� O), Vector3 : ����(x,y,z)

    public Vector3 dir = new Vector3(0, 0, 1);
    public float speed = 1.0f;  // �̵� �ӷ� m/s

    public float jumpPower = 5f; // ����(����)�� �� ��
    public bool isGrounded = false; // boolean : true(1) or false(0) ���� ����������� jump�ض�!
    public float gravity = -9.81f; // �߷�
    public float yVelocity = 0;  // y�� ��ȭ

    CharacterController controller;     //�������� : �� �׸�

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();  // �׸��� �����͸� ���.
    }

    // Update is called once per frame
    void Update()
    {
        //���� �Է��� �������� �̵��ϰ� �ʹ�!
        float h = Input.GetAxis("Horizontal"); // a �� d �� ���� ��(a ������ -1, d ������ +1)
        float v = Input.GetAxis("Vertical"); // s �� w �� ���� ��(s ������ -1, w ������ +1)

        dir = new Vector3(h, 0, v); // �Ʒ� dir.Normalize()�� �� ������Ѵ�.
        dir.Normalize(); // ����ȭ Normalizer (������ �����ϸ鼭 ������ ���̸� 1�� ������)

        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        dir.Normalize();

        // �̵��ϴ� �߿� �����ϰ� �ʹ�!

        // ���� Player(ĳ������Ʈ�ѷ�)�� �ٴڿ� ����ִٸ�,
        if (controller.collisionFlags == CollisionFlags.Below)
        {
            isGrounded = true; // �ٴڿ� ������
            yVelocity = 0; // �Ʒ��� ���������� �ض� (�ӵ� => 0)
        }
        // �ٴڿ� ����ִ°� �°�, �� ���¿��� ����Ű�� ������ �´ٸ�, 
        if (isGrounded == true && Input.GetButtonDown("Jump"))
        {
            yVelocity = jumpPower;
            isGrounded = false; // �ٴڿ� ������ �ƴϸ�
        }
        yVelocity = yVelocity + gravity * Time.deltaTime; // �߷°��� �ް� �ض�.
        dir.y = yVelocity;

        //��ġ�� ����ؼ� �ٲ۴�.
        //P = p0 + v(����,����) * t(�ð�)
        //transform.position = transform.position + dir * speed * Time.deltaTime;
        controller.Move(dir * speed * Time.deltaTime);  // CaracterController ������ �� �����.
        // transform.position += dir;
        // transform.Translate(dir * speed * Time.deltaTime);
        // ���� ������ �������� �̵��ϰ� �ʹ�!
    }
}