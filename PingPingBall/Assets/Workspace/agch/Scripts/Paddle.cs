using UnityEngine;

public class Paddle : MonoBehaviour
{
    // [Header("Paddle Identity")] // AddForce 로직을 사용하지 않으므로 잠시 주석 처리
    // [Tooltip("이 패들이 왼쪽 패들이면 체크하세요.")]
    // public bool isLeftPaddle;

    // --- 부위별 AddForce 관련 로직을 모두 제거 또는 주석 처리했습니다 ---

    // [Header("Bonus Kick Settings")]
    // public float tipBonusForce = 8f;
    // public float middleBonusForce = 4f;
    // public float minImpactVelocity = 1f;

    // [Header("Zone Definition")]
    // public float tipZoneThreshold = 1.2f;
    // public float middleZoneThreshold = 0.5f;

    // private HingeJoint2D hingeJoint; // OnCollisionEnter2D를 사용하지 않으므로 필요 없음

    /*
    // Start와 OnCollisionEnter2D 함수 전체를 주석 처리하여 비활성화합니다.
    void Start()
    {
        // hingeJoint = GetComponent<HingeJoint2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 이 안의 모든 로직이 비활성화됩니다.
    }
    */

    // 이 스크립트에는 이제 아무런 로직도 남아있지 않습니다.
    // 패들의 모든 움직임은 PaddleController.cs와 Rigidbody2D, HingeJoint2D 컴포넌트가 처리합니다.
}