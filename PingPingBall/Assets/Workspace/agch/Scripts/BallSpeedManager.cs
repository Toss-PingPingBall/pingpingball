using UnityEngine;

public class BallSpeedManager : MonoBehaviour
{
    // --- Inspector���� ������ ������ ---
    [Header("Speed Settings (From GDD)")]
    public float minSpeed = 0.6f; // �ּ� �ӵ� [cite: 74]
    public float maxSpeed = 2.5f; // �ִ� �ӵ� [cite: 75]
    public float initialSpeed = 1.0f; // �ʱ� �ӵ� (�⺻ �ӵ�) [cite: 73]

    // --- ���� ���� ---
    private Rigidbody2D rb;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // ���� ���� �� �ʱ� �ӵ��� ����
        SetInitialVelocity();
    }

    void FixedUpdate()
    {
        // ���� ������Ʈ �ֱ⸶�� �ӵ��� üũ�ϰ� ���� ���� ���� ����
        LimitSpeed();
    }

    // �ʱ� �ӵ� ���� �Լ�
    private void SetInitialVelocity()
    {
        currentSpeed = initialSpeed;
        // ������ �ʱ� �������� �߻� (���� ���� ���� ������ �°� ���� ����)
        rb.linearVelocity = Random.insideUnitCircle.normalized * currentSpeed;
    }

    // �ӵ� ���� �Լ�
    private void LimitSpeed()
    {
        // ���� �ӵ��� ���
        currentSpeed = rb.linearVelocity.magnitude;

        // �ּ�/�ִ� �ӵ��� ������� Ȯ��
        if (currentSpeed > maxSpeed || currentSpeed < minSpeed && currentSpeed > 0) // �ӵ��� 0�϶��� ����
        {
            // �ӵ��� ���� ���� Ŭ����(clamping)
            float clampedSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);
            // ������ ������ ä, ������ �ӵ��� �ٽ� ����
            rb.linearVelocity = rb.linearVelocity.normalized * clampedSpeed;
        }
    }

    // --- �ٸ� ��ũ��Ʈ���� ȣ���� ���� �Լ��� ---

    // �ӵ��� Ư�� ������ ��� �����ϴ� �Լ�
    public void SetSpeed(float newSpeed)
    {
        rb.linearVelocity = rb.linearVelocity.normalized * newSpeed;
    }

    // ���� �ӵ��� ���� ���ϰų� ���� �Լ�
    public void AddSpeed(float amount)
    {
        float newSpeed = rb.linearVelocity.magnitude + amount;
        rb.linearVelocity = rb.linearVelocity.normalized * newSpeed;
    }

    // ���� �ӵ��� Ư�� ������ ���ϴ� �Լ� (�е�, ������ � ���)
    public void MultiplySpeed(float multiplier)
    {
        // 0���� ���� ������ ���� �ӵ��� �����Ǵ� ���� ����
        if (multiplier < 0) return;

        float newSpeed = rb.linearVelocity.magnitude * multiplier;
        rb.linearVelocity = rb.linearVelocity.normalized * newSpeed;
    }
}