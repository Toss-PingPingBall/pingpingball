using UnityEngine;

public class Paddle : MonoBehaviour
{
    [Header("Speed Multipliers")]
    public float tipMultiplier = 1.5f; // �е� �� �κ� ���� [cite: 98]
    public float middleMultiplier = 1.2f; // �е� �߰� �κ� ���� [cite: 99]

    [Header("Zone Definition")]
    [Tooltip("�Ǻ����κ��� �� �Ÿ����� �ָ� '��'���� ����")]
    public float tipZoneThreshold = 1.2f; // ���� ��, �е� ���̿� �°� ���� �ʿ�
    [Tooltip("�Ǻ����κ��� �� �Ÿ����� �ָ� '�߰�'���� ����")]
    public float middleZoneThreshold = 0.5f; // ���� ��, �е� ���̿� �°� ���� �ʿ�

    private HingeJoint2D hingeJoint;

    void Start()
    {
        // �ڽ��� HingeJoint2D ������Ʈ�� ������
        hingeJoint = GetComponent<HingeJoint2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // �ε��� ������Ʈ�� 'Ball' �±׸� ������ �ִ��� Ȯ��
        if (collision.gameObject.CompareTag("Ball"))
        {
            // ���� BallSpeedManager ��ũ��Ʈ�� ������
            BallSpeedManager ballSpeedManager = collision.gameObject.GetComponent<BallSpeedManager>();
            if (ballSpeedManager == null) return;

            // 1. �浹 ����(World-space)�� ������
            Vector2 collisionPoint = collision.contacts[0].point;

            // 2. �е��� ȸ����(Pivot) ��ġ(World-space)�� ���
            Vector2 pivotPoint = transform.TransformPoint(hingeJoint.anchor);

            // 3. ȸ����� �浹 ���� ������ �Ÿ��� ���
            float distanceFromPivot = Vector2.Distance(collisionPoint, pivotPoint);

            // 4. �Ÿ��� ���� �ٸ� �ӵ� ������ ����
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
                // ������ �ӵ� ��ȭ ���� (1.0��) [cite: 100]
            }
        }
    }
}