using UnityEngine;

using System;

public class AL_PaddleController : MonoBehaviour
{
    // Inspector â���� ����, ������ �е��� HingeJoint2D�� �������� ����
    public HingeJoint2D leftPaddle;
    public HingeJoint2D rightPaddle;

    // �е��� �����̴� ��(������ �ӵ�)
    public float motorSpeed = 1000f;

    private JointMotor2D leftMotor, rightMotor;

    void Start()
    {
        leftMotor = leftPaddle.motor;
        rightMotor = rightPaddle.motor;
    }

    void Update()
    {
        bool left = false;
        bool right = false;
        int tcnt = Input.touchCount;
        for (int i = 0; i < tcnt; i++)
        {
            if (Input.GetTouch(i).position.x < Screen.width / 2)
                left = true;
            else
                right = true;
        }

        // ȭ�鿡 ���콺 Ŭ��(�Ǵ� ��ġ)�� �ִ��� Ȯ��
        // Ŭ���� ��ġ�� ȭ���� �߾Ӻ��� ���ʿ� �ִ��� Ȯ��
        if (left)
        {
            // ���� �е� �����̱�
            JointMotor2D motor = leftPaddle.motor;
            motor.motorSpeed = -motorSpeed; // Hinge Joint�� ȸ�� ���⿡ ���� ��ȣ ����
            leftPaddle.motor = motor;
        }
        if (right) // ȭ�� �����ʿ� Ŭ���� �ִٸ�
        {
            // ������ �е� �����̱�
            JointMotor2D motor = rightPaddle.motor;
            motor.motorSpeed = motorSpeed; // Hinge Joint�� ȸ�� ���⿡ ���� ��ȣ ����
            rightPaddle.motor = motor;
        }

        // ȭ�鿡�� ���� ���� �� �е��� ����ġ��
        // ��� �е��� ���� �ӵ��� �ݴ�� �Ͽ� �ǵ��ư��� ��
        if (!left)
        {
            JointMotor2D motor = leftPaddle.motor;
            motor.motorSpeed = motorSpeed;
            leftPaddle.motor = motor;
        }
        if (!right)
        {
            JointMotor2D motor = rightPaddle.motor;
            motor.motorSpeed = -motorSpeed;
            rightPaddle.motor = motor;
        }
    }
}