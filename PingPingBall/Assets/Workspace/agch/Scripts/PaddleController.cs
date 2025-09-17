using UnityEngine;

public class PaddleController : MonoBehaviour
{
    // Inspector â���� ����, ������ �е��� HingeJoint2D�� �������� ����
    public HingeJoint2D leftPaddle;
    public HingeJoint2D rightPaddle;

    // �е��� �����̴� ��(������ �ӵ�)
    public float motorSpeed = 1200f;

    private JointMotor2D leftMotor, rightMotor;

    void Start()
    {
        // ���� ������ �̸� �޾ƿɴϴ�.
        leftMotor = leftPaddle.motor;
        rightMotor = rightPaddle.motor;
    }

    void Update()
    {
        // 1. �� ������, �켱 ���� �е��� ���ȴ��� ���θ� false�� �ʱ�ȭ�մϴ�.
        bool leftPressed = false;
        bool rightPressed = false;

        // 2. ���� ȭ���� ��� ��ġ�� �˻��մϴ�.
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                // ��ġ ��ġ�� �����̸� leftPressed�� true�� ����
                if (touch.position.x < Screen.width / 2)
                {
                    leftPressed = true;
                }
                // ��ġ ��ġ�� �������̸� rightPressed�� true�� ����
                else
                {
                    rightPressed = true;
                }
            }
        }

        // 3. ���������� ������ ���¿� ���� �е� ���͸� �����մϴ�.
        // ������ ���ȴٸ� (leftPressed == true)
        if (leftPressed)
        {
            leftMotor.motorSpeed = -motorSpeed; // ���� �ø��� ��
        }
        // ������ ������ �ʾҴٸ�
        else
        {
            leftMotor.motorSpeed = motorSpeed; // �Ʒ��� ������ �� (����ġ)
        }

        // �������� ���ȴٸ� (rightPressed == true)
        if (rightPressed)
        {
            rightMotor.motorSpeed = motorSpeed; // ���� �ø��� ��
        }
        // �������� ������ �ʾҴٸ�
        else
        {
            rightMotor.motorSpeed = -motorSpeed; // �Ʒ��� ������ �� (����ġ)
        }

        // 4. ���� ���� ���� ���� �е鿡 �����մϴ�.
        leftPaddle.motor = leftMotor;
        rightPaddle.motor = rightMotor;
    }
}