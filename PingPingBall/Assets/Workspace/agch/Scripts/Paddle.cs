using UnityEngine;

public class Paddle : MonoBehaviour
{
    [Header("Paddle Identity")]
    [Tooltip("이 패들이 왼쪽 패들이면 체크하세요.")]
    public bool isLeftPaddle;

    [Header("Bonus Kick Settings")]
    public float tipBonusForce = 8f;
    public float middleBonusForce = 4f;
    [Tooltip("이 값보다 강하게 부딪혀야 보너스 킥이 발동됩니다.")]
    public float minImpactVelocity = 1f;

    [Header("Zone Definition")]
    public float tipZoneThreshold = 1.2f;
    public float middleZoneThreshold = 0.5f;

    private HingeJoint2D hingeJoint;

    void Start()
    {
        hingeJoint = GetComponent<HingeJoint2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // --- 조건 1: 패들이 '공을 치는 방향'으로 움직이고 있는가? ---
        bool isMovingUpwards;
        if (isLeftPaddle)
        {
            // 왼쪽 패들은 모터 속도가 음수일 때 위로 올라감
            isMovingUpwards = hingeJoint.motor.motorSpeed < 0;
        }
        else
        {
            // 오른쪽 패들은 모터 속도가 양수일 때 위로 올라감
            isMovingUpwards = hingeJoint.motor.motorSpeed > 0;
        }

        if (!isMovingUpwards)
        {
            return; // 위로 움직이는 게 아니면 아무것도 하지 않음
        }

        // --- 조건 2: 충돌 강도가 충분한가? ---
        if (collision.relativeVelocity.magnitude < minImpactVelocity)
        {
            return; // 충격이 약하면 아무것도 하지 않음
        }

        // 모든 조건을 통과하고, 부딪힌 것이 'Ball' 태그를 가진 Rigidbody라면
        if (collision.gameObject.CompareTag("Ball") && collision.rigidbody != null)
        {
            Rigidbody2D ballRigidbody = collision.rigidbody;

            Vector2 collisionPoint = collision.contacts[0].point;
            Vector2 pivotPoint = transform.TransformPoint(hingeJoint.anchor);
            float distanceFromPivot = Vector2.Distance(collisionPoint, pivotPoint);
            Vector2 forceDirection = collision.contacts[0].normal;
            float forceMagnitude = 0f;

            if (distanceFromPivot >= tipZoneThreshold)
            {
                forceMagnitude = tipBonusForce;
            }
            else if (distanceFromPivot >= middleZoneThreshold)
            {
                forceMagnitude = middleBonusForce;
            }

            if (forceMagnitude > 0)
            {
                ballRigidbody.AddForce(-forceDirection * forceMagnitude, ForceMode2D.Impulse);
            }
        }
    }
}