using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        // ������ �� Rigidbody2D ������Ʈ�� �����ɴϴ�.
        rb = GetComponent<Rigidbody2D>();

        // ������ ���۵��ڸ��� ���� �������� ���� �߻��մϴ�.
        //LaunchBall();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
            LaunchBall();
    }

    // ���� �߻��ϴ� �Լ�
    void LaunchBall()
    {
        // ���� �������� ���� �ݴϴ�. Vector2(x, y)
        Vector2 launchDirection = new Vector2(0.5f, 1f).normalized; // �ణ ������ ���� ���ϵ��� ���� ����
        float launchForce = 15f; // �߻� ���� ũ��

        // Impulse�� ���������� ū ���� ���� �� ����մϴ�. �ɺ� �߻翡 �����մϴ�.
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