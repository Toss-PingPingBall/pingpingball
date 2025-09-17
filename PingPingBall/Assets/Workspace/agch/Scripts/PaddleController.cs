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
        // 1. 매 프레임, 우선 양쪽 패들이 눌렸는지 여부를 false로 초기화합니다.
        bool leftPressed = false;
        bool rightPressed = false;

        // 2. 현재 화면의 모든 터치를 검사합니다.
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                // 터치 위치가 왼쪽이면 leftPressed를 true로 설정
                if (touch.position.x < Screen.width / 2)
                {
                    leftPressed = true;
                }
                // 터치 위치가 오른쪽이면 rightPressed를 true로 설정
                else
                {
                    rightPressed = true;
                }
            }
        }

        // 3. 최종적으로 결정된 상태에 따라 패들 모터를 제어합니다.
        // 왼쪽이 눌렸다면 (leftPressed == true)
        if (leftPressed)
        {
            leftMotor.motorSpeed = -motorSpeed; // 위로 올리는 힘
        }
        // 왼쪽이 눌리지 않았다면
        else
        {
            leftMotor.motorSpeed = motorSpeed; // 아래로 내리는 힘 (원위치)
        }

        // 오른쪽이 눌렸다면 (rightPressed == true)
        if (rightPressed)
        {
            rightMotor.motorSpeed = motorSpeed; // 위로 올리는 힘
        }
        // 오른쪽이 눌리지 않았다면
        else
        {
            rightMotor.motorSpeed = -motorSpeed; // 아래로 내리는 힘 (원위치)
        }

        // 4. 계산된 모터 값을 실제 패들에 적용합니다.
        leftPaddle.motor = leftMotor;
        rightPaddle.motor = rightMotor;
    }
}