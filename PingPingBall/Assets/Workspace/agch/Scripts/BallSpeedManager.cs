using UnityEngine;

public class BallSpeedManager : MonoBehaviour
{
    [Header("Speed Limit Settings")]
    [Tooltip("공이 절대로 넘을 수 없는 속도 상한선")]
    public float maxSpeedLimit = 25f;

    [Header("Keyboard Test Settings")]
    [Tooltip("↑ 키를 눌렀을 때 속도가 곱해지는 배율")]
    public float speedUpMultiplier = 1.5f;
    [Tooltip("↓ 키를 눌렀을 때 속도가 곱해지는 배율")]
    public float speedDownMultiplier = 0.7f;

    private Rigidbody2D rb;
    private float currentMaxSpeed = 0f; // 최고 속도 기록용 (디버그)

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // 현재 속도가 설정된 제한 속도를 초과하는지 확인
        if (rb.linearVelocity.magnitude > maxSpeedLimit)
        {
            // 방향은 유지한 채, 속도만 최대 속도로 고정
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeedLimit;
        }
    }

    void Update()
    {
        // 위쪽 화살표 키를 누르면 속도 증가
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.linearVelocity *= speedUpMultiplier;
            Debug.Log($"UP Arrow Pressed! Speed multiplied by {speedUpMultiplier}.");
        }

        // 아래쪽 화살표 키를 누르면 속도 감소
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rb.linearVelocity *= speedDownMultiplier;
            Debug.Log($"DOWN Arrow Pressed! Speed multiplied by {speedDownMultiplier}.");
        }


        // 디버그용 속도 추적
        float currentSpeed = rb.linearVelocity.magnitude;
        if (currentSpeed > currentMaxSpeed)
        {
            currentMaxSpeed = currentSpeed;
        }
        Debug.Log($"현재 속도: {currentSpeed:F2} / 기록된 최고 속도: {currentMaxSpeed:F2}");
    }
}