using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    public float initialSpeed = 6f;  // 시작 속도
    public float minSpeed = 2.5f;    // 너무 느려지면 보정
    public float maxSpeed = 12f;     // 너무 빨라지면 제한
    public float nudgeAngleDeg = 8f; // 갇힘 방지용 소각 회전

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // 위쪽으로 살짝 치우친 방향으로 초기 발사
        Vector2 dir = Random.insideUnitCircle.normalized;
        if (dir.y < 0f) dir.y = -dir.y;                // 반드시 위쪽
        dir = dir.normalized;
        rb.linearVelocity = dir * initialSpeed;
    }

    void FixedUpdate()
    {
        float speed = rb.linearVelocity.magnitude;

        // 속도 하한/상한 보정
        if (speed < minSpeed && speed > 0.001f)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * minSpeed;
        }
        else if (speed > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }

        // 거의 수평/수직으로 갇히는 현상 완화
        Vector2 v = rb.linearVelocity;
        if (Mathf.Abs(v.x) < 0.05f || Mathf.Abs(v.y) < 0.05f)
        {
            rb.linearVelocity = Rotate(v.normalized, nudgeAngleDeg) * Mathf.Max(speed, minSpeed);
        }
    }

    Vector2 Rotate(Vector2 v, float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        float cs = Mathf.Cos(rad);
        float sn = Mathf.Sin(rad);
        return new Vector2(v.x * cs - v.y * sn, v.x * sn + v.y * cs);
    }
}
