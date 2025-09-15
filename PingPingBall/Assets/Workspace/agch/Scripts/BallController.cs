using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
            LaunchBall();
    }

    // 공을 발사하는 함수
    void LaunchBall()
    {
        // 위쪽 방향으로 힘을 줌. Vector2(x, y)
        Vector2 launchDirection = new Vector2(0.5f, 1f).normalized; // 약간 오른쪽 위를 향하도록 방향 설정
        float launchForce = 15f; // 발사 힘의 크기

        // Impulse는 순간적으로 큰 힘을 가할 때 사용
        rb.AddForce(launchDirection * launchForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
        }
    }
}