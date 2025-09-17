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
        // ������ ȯ���̰ų� �� ȯ���� ��� �Է� ó��
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
        if (Input.GetMouseButton(0)) // ���콺 ���� ��ư�� �����ִ� ����
        {
            if (Input.mousePosition.x < Screen.width / 2)
                left = true;
            else
                right = true;
        }
#else
        // �����(iOS, Android) ���忡�� �Է� ó��
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.position.x < Screen.width / 2)
                    left = true;
                else
                    right = true;
            }
        }
#endif
        if (left)
        {
            JointMotor2D motor = leftPaddle.motor;
            motor.motorSpeed = -motorSpeed;
            leftPaddle.motor = motor;
        }
        else
        {
            JointMotor2D motor = leftPaddle.motor;
            motor.motorSpeed = motorSpeed;
            leftPaddle.motor = motor;
        }

        if (right)
        {
            JointMotor2D motor = rightPaddle.motor;
            motor.motorSpeed = motorSpeed;
            rightPaddle.motor = motor;
        }
        else
        {
            JointMotor2D motor = rightPaddle.motor;
            motor.motorSpeed = -motorSpeed;
            rightPaddle.motor = motor;
        }
    }
}