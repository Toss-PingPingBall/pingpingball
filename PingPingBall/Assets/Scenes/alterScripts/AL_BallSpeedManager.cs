using UnityEngine;

public class AL_BallSpeedManager : MonoBehaviour
{
    [Header("Speed Limit Settings")]
    [Tooltip("공이 절대로 넘을 수 없는 속도 상한선")]
    public float maxSpeedLimit = 25f;

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
        // 디버그용 속도 추적
        float currentSpeed = rb.linearVelocity.magnitude;
        if (currentSpeed > currentMaxSpeed)
        {
            currentMaxSpeed = currentSpeed;
        }

        if (transform.localPosition.y < -40)
        {
            Vector3 pos = transform.localPosition;
            pos.x = -9.5f;
            pos.y = -16f;
            transform.localPosition = pos;
            gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 부딪힌 오브젝트의 태그를 확인
        string tag = collision.gameObject.tag;

        // 태그가 "Block" 이거나 "Wall" 일 때 (Block은 일단 보류)
        if (tag == "Block" || tag == "Wall")
        {
            // 1. 충돌 지점의 법선 벡터(표면에 수직인 방향) 참조
            Vector2 normal = collision.contacts[0].normal;

            // 2. 법선 벡터를 90도 회전시켜 표면과 평행한 '미끄러질 방향'을 계산
            Vector2 slideDirection = new Vector2(-normal.y, normal.x);

            // 3. 계산된 '미끄러질 방향'으로 아주 작은 힘 부여(제자리에서만 튀기는 버그 방지용)
            rb.AddForce(slideDirection * 0.01f, ForceMode2D.Impulse);
        }
    }
}