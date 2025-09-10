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
        // ȭ�鿡 ���콺 Ŭ��(�Ǵ� ��ġ)�� �ִ��� Ȯ��
        if (Input.GetMouseButtonDown(0)) // 0�� ���콺 ���� ��ư �Ǵ� ù ��° ��ġ
        {
            // Ŭ���� ��ġ�� ȭ���� �߾Ӻ��� ���ʿ� �ִ��� Ȯ��
            if (Input.mousePosition.x < Screen.width / 2)
            {
                // ���� �е� �����̱�
                leftMotor.motorSpeed = -motorSpeed; // Hinge Joint�� ȸ�� ���⿡ ���� ��ȣ ����
                leftPaddle.motor = leftMotor;
            }
            else // ȭ�� �����ʿ� Ŭ���� �ִٸ�
            {
                // ������ �е� �����̱�
                rightMotor.motorSpeed = motorSpeed; // Hinge Joint�� ȸ�� ���⿡ ���� ��ȣ ����
                rightPaddle.motor = rightMotor;
            }
        }

        // ȭ�鿡�� ���� ���� �� �е��� ����ġ��
        if (Input.GetMouseButtonUp(0))
        {
            // ��� �е��� ���� �ӵ��� �ݴ�� �Ͽ� �ǵ��ư��� ��
            leftMotor.motorSpeed = motorSpeed;
            leftPaddle.motor = leftMotor;

            rightMotor.motorSpeed = -motorSpeed;
            rightPaddle.motor = rightMotor;
        }
    }
}