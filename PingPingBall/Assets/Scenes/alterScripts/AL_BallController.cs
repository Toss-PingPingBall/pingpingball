using UnityEngine;

public class AL_BallController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 preBuffScale; // ������ ���� ���� ������ ������(ũ��)�� �����Ͽ� ���� �� ���
    private float effectTimer = 0f; // ������ ȿ�� ���� �ð��� �����ϴ� Ÿ�̸�
    private bool isSizeUp = false; // �� ũ�� Ȯ�� ȿ�� ���� ����
    private SpriteRenderer spriteRenderer; // ���� ������ �����ϱ� ���� ������Ʈ

    // ��� ����
    private readonly Color ORIGINAL_COLOR = Color.magenta; // ���� ���� ���� (��ũ��)
    private const float BLINKING_TIME = 2.0f; // ȿ�� ���� �� �����Ÿ��� �����ϴ� ���� �ð�
    private const float SIZE_UP_FACTOR = 1.5f; // ũ�� Ȯ�� ����


    void Start()
    {
        // ������Ʈ �ʱ�ȭ
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // GameManager�� �� ũ�� ���� �̺�Ʈ�� ����
        GameManager.instance.onBallSizeChanged += OnBallSizeChanged;

        // ���� �� ���� ������ ���� �������� ����
        if (spriteRenderer != null)
        {
            spriteRenderer.color = ORIGINAL_COLOR;
        }
    }

    private void OnDestroy()
    {
        // ������Ʈ �ı� �� �̺�Ʈ ���� ���� (�޸� ���� ����)
        if (GameManager.instance != null)
            GameManager.instance.onBallSizeChanged -= OnBallSizeChanged;
    }

    void Update()
    {
        // ũ�� Ȯ�� ȿ���� ���� ���� ���
        if (isSizeUp)
        {
            effectTimer -= Time.deltaTime; // Ÿ�̸� ����

            // Ÿ�̸Ӱ� 0 ���ϰ� �Ǹ� ȿ�� ���� ó�� ��û
            if (effectTimer <= 0)
            {
                GameManager.instance.cancelBallSizeUp();
            }
            // ȿ�� ���� �� �����Ÿ��� �ð� ������ ������ ���
            else if (effectTimer <= BLINKING_TIME)
            {
                if (spriteRenderer != null)
                {
                    // �����̴� Ƚ���� �ʴ� �� 2ȸ�� ����
                    if (Mathf.FloorToInt(effectTimer * 4) % 2 == 0)
                        spriteRenderer.color = ORIGINAL_COLOR;
                    else
                        spriteRenderer.color = Color.yellow; // �����̴� ����
                }
            }
        }
    }

    // GameManager�κ��� ũ�� ���� �̺�Ʈ ���� �� ó��
    private void OnBallSizeChanged(float duration)
    {
        // duration�� 0���� ũ�� ȿ�� ���� �Ǵ� �ð� ����
        if (duration > 0f)
        {
            // ���� ũ�� Ȯ�밡 ������� �ʾҴٸ� (���� 1ȸ�� ����)
            if (!isSizeUp)
            {
                // ���� �������� ���� �����Ϸ� ���� (���� �� ���)
                preBuffScale = transform.localScale;

                // X, Y�ุ Ȯ�� ������ �����Ͽ� ũ�� ����
                transform.localScale = new Vector3(preBuffScale.x * SIZE_UP_FACTOR, preBuffScale.y * SIZE_UP_FACTOR, preBuffScale.z);
            }

            isSizeUp = true;
            effectTimer = duration; // ���� �ð� ������Ʈ (��ȹ�� �� �ð� �ʱ�ȭ)

            // �������� ���߰� ���� �������� ����
            if (spriteRenderer != null)
                spriteRenderer.color = ORIGINAL_COLOR;
        }
        // duration�� 0�̸� ȿ�� ���� ��û (GameManager.cancelBallSizeUp ȣ�� ��)
        else // duration == 0f (Cancellation)
        {
            // ũ�� Ȯ�밡 ���� ���̾����� ���� ũ��� ����
            if (isSizeUp)
            {
                transform.localScale = preBuffScale;

                // ���� ���� �������� ����
                if (spriteRenderer != null)
                    spriteRenderer.color = ORIGINAL_COLOR;
            }
            isSizeUp = false;
            effectTimer = 0f;
        }
    }

    // ������/���� �� Ʈ���� �浹 �� GameManager�� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.invokeCollisionProcess(collision.gameObject);
    }

    // ��� �� �Ϲ� �浹 �� GameManager�� ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.instance.invokeCollisionProcess(collision.gameObject);
    }
}