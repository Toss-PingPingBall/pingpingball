using UnityEngine;

using System;

public class AL_PaddleController : MonoBehaviour
{
    // Inspector 창에서 왼쪽, 오른쪽 패들의 HingeJoint2D를 연결해줄 변수
    public HingeJoint2D leftPaddle;
    public HingeJoint2D rightPaddle;

    // 패들이 움직이는 힘(모터의 속도)
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
        // 에디터 환경이거나 웹 환경의 경우 입력 처리
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
        if (Input.GetMouseButton(0)) // 마우스 왼쪽 버튼이 눌려있는 동안
        {
            if (Input.mousePosition.x < Screen.width / 2)
                left = true;
            else
                right = true;
        }
#else
        // 모바일(iOS, Android) 빌드에서 입력 처리
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