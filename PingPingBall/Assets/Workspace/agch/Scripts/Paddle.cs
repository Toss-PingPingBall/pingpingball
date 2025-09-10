using UnityEngine;

public class Paddle : MonoBehaviour
{
    [Header("Speed Multipliers")]
    public float tipMultiplier = 1.5f; // 패들 끝 부분 배율 [cite: 98]
    public float middleMultiplier = 1.2f; // 패들 중간 부분 배율 [cite: 99]

    [Header("Zone Definition")]
    [Tooltip("피봇으로부터 이 거리보다 멀면 '끝'으로 간주")]
    public float tipZoneThreshold = 1.2f; // 예시 값, 패들 길이에 맞게 조절 필요
    [Tooltip("피봇으로부터 이 거리보다 멀면 '중간'으로 간주")]
    public float middleZoneThreshold = 0.5f; // 예시 값, 패들 길이에 맞게 조절 필요

    private HingeJoint2D hingeJoint;

    void Start()
    {
        // 자신의 HingeJoint2D 컴포넌트를 가져옴
        hingeJoint = GetComponent<HingeJoint2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 부딪힌 오브젝트가 'Ball' 태그를 가지고 있는지 확인
        if (collision.gameObject.CompareTag("Ball"))
        {
            // 공의 BallSpeedManager 스크립트를 가져옴
            BallSpeedManager ballSpeedManager = collision.gameObject.GetComponent<BallSpeedManager>();
            if (ballSpeedManager == null) return;

            // 1. 충돌 지점(World-space)을 가져옴
            Vector2 collisionPoint = collision.contacts[0].point;

            // 2. 패들의 회전축(Pivot) 위치(World-space)를 계산
            Vector2 pivotPoint = transform.TransformPoint(hingeJoint.anchor);

            // 3. 회전축과 충돌 지점 사이의 거리를 계산
            float distanceFromPivot = Vector2.Distance(collisionPoint, pivotPoint);

            // 4. 거리에 따라 다른 속도 배율을 적용
            if (distanceFromPivot >= tipZoneThreshold)
            {
                Debug.Log("Hit Paddle Tip!");
                ballSpeedManager.MultiplySpeed(tipMultiplier);
            }
            else if (distanceFromPivot >= middleZoneThreshold)
            {
                Debug.Log("Hit Paddle Middle!");
                ballSpeedManager.MultiplySpeed(middleMultiplier);
            }
            else
            {
                Debug.Log("Hit Paddle Base.");
                // 안쪽은 속도 변화 없음 (1.0배) [cite: 100]
            }
        }
    }
}