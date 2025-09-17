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
        int tcnt = Input.touchCount;
        for (int i = 0; i < tcnt; i++)
        {
            if (Input.GetTouch(i).position.x < Screen.width / 2)
                left = true;
            else
                right = true;
        }

        // 화면에 마우스 클릭(또는 터치)이 있는지 확인
        // 클릭된 위치가 화면의 중앙보다 왼쪽에 있는지 확인
        if (left)
        {
            // 왼쪽 패들 움직이기
            JointMotor2D motor = leftPaddle.motor;
            motor.motorSpeed = -motorSpeed; // Hinge Joint의 회전 방향에 따라 부호 조절
            leftPaddle.motor = motor;
        }
        if (right) // 화면 오른쪽에 클릭이 있다면
        {
            // 오른쪽 패들 움직이기
            JointMotor2D motor = rightPaddle.motor;
            motor.motorSpeed = motorSpeed; // Hinge Joint의 회전 방향에 따라 부호 조절
            rightPaddle.motor = motor;
        }

        // 화면에서 손을 뗐을 때 패들을 원위치로
        // 모든 패들의 모터 속도를 반대로 하여 되돌아가게 함
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