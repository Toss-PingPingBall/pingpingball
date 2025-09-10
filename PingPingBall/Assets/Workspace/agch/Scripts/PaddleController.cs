using UnityEngine;

public class PaddleController : MonoBehaviour
{
    // Inspector 창에서 왼쪽, 오른쪽 패들의 HingeJoint2D를 연결해줄 변수
    public HingeJoint2D leftPaddle;
    public HingeJoint2D rightPaddle;

    // 패들이 움직이는 힘(모터의 속도)
    public float motorSpeed = 1200f;

    private JointMotor2D leftMotor, rightMotor;

    void Start()
    {
        // 모터 설정을 미리 받아옵니다.
        leftMotor = leftPaddle.motor;
        rightMotor = rightPaddle.motor;
    }

    void Update()
    {
        // 화면에 마우스 클릭(또는 터치)이 있는지 확인
        if (Input.GetMouseButtonDown(0)) // 0은 마우스 왼쪽 버튼 또는 첫 번째 터치
        {
            // 클릭된 위치가 화면의 중앙보다 왼쪽에 있는지 확인
            if (Input.mousePosition.x < Screen.width / 2)
            {
                // 왼쪽 패들 움직이기
                leftMotor.motorSpeed = -motorSpeed; // Hinge Joint의 회전 방향에 따라 부호 조절
                leftPaddle.motor = leftMotor;
            }
            else // 화면 오른쪽에 클릭이 있다면
            {
                // 오른쪽 패들 움직이기
                rightMotor.motorSpeed = motorSpeed; // Hinge Joint의 회전 방향에 따라 부호 조절
                rightPaddle.motor = rightMotor;
            }
        }

        // 화면에서 손을 뗐을 때 패들을 원위치로
        if (Input.GetMouseButtonUp(0))
        {
            // 모든 패들의 모터 속도를 반대로 하여 되돌아가게 함
            leftMotor.motorSpeed = motorSpeed;
            leftPaddle.motor = leftMotor;

            rightMotor.motorSpeed = -motorSpeed;
            rightPaddle.motor = rightMotor;
        }
    }
}