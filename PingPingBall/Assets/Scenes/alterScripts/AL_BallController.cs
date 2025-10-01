using UnityEngine;

public class AL_BallController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 preBuffScale; // 아이템 버프 적용 직전의 스케일(크기)을 저장하여 복원 시 사용
    private float effectTimer = 0f; // 아이템 효과 남은 시간을 추적하는 타이머
    private bool isSizeUp = false; // 공 크기 확대 효과 적용 여부
    private SpriteRenderer spriteRenderer; // 공의 색상을 변경하기 위한 컴포넌트

    // 상수 정의
    private readonly Color ORIGINAL_COLOR = Color.magenta; // 공의 원래 색상 (핑크색)
    private const float BLINKING_TIME = 2.0f; // 효과 종료 전 깜빡거리기 시작하는 남은 시간
    private const float SIZE_UP_FACTOR = 1.5f; // 크기 확대 배율


    void Start()
    {
        // 컴포넌트 초기화
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // GameManager의 공 크기 변경 이벤트를 구독
        GameManager.instance.onBallSizeChanged += OnBallSizeChanged;

        // 시작 시 공의 색상을 원래 색상으로 설정
        if (spriteRenderer != null)
        {
            spriteRenderer.color = ORIGINAL_COLOR;
        }
    }

    private void OnDestroy()
    {
        // 오브젝트 파괴 시 이벤트 구독 해제 (메모리 누수 방지)
        if (GameManager.instance != null)
            GameManager.instance.onBallSizeChanged -= OnBallSizeChanged;
    }

    void Update()
    {
        // 크기 확대 효과가 적용 중인 경우
        if (isSizeUp)
        {
            effectTimer -= Time.deltaTime; // 타이머 감소

            // 타이머가 0 이하가 되면 효과 종료 처리 요청
            if (effectTimer <= 0)
            {
                GameManager.instance.cancelBallSizeUp();
            }
            // 효과 종료 전 깜빡거리는 시간 구간에 진입한 경우
            else if (effectTimer <= BLINKING_TIME)
            {
                if (spriteRenderer != null)
                {
                    // 깜빡이는 횟수를 초당 약 2회로 설정
                    if (Mathf.FloorToInt(effectTimer * 4) % 2 == 0)
                        spriteRenderer.color = ORIGINAL_COLOR;
                    else
                        spriteRenderer.color = Color.yellow; // 깜빡이는 색상
                }
            }
        }
    }

    // GameManager로부터 크기 변경 이벤트 수신 및 처리
    private void OnBallSizeChanged(float duration)
    {
        // duration이 0보다 크면 효과 적용 또는 시간 갱신
        if (duration > 0f)
        {
            // 아직 크기 확대가 적용되지 않았다면 (최초 1회만 실행)
            if (!isSizeUp)
            {
                // 현재 스케일을 원래 스케일로 저장 (복원 시 사용)
                preBuffScale = transform.localScale;

                // X, Y축만 확대 배율을 적용하여 크기 변경
                transform.localScale = new Vector3(preBuffScale.x * SIZE_UP_FACTOR, preBuffScale.y * SIZE_UP_FACTOR, preBuffScale.z);
            }

            isSizeUp = true;
            effectTimer = duration; // 지속 시간 업데이트 (재획득 시 시간 초기화)

            // 깜빡임을 멈추고 원래 색상으로 복귀
            if (spriteRenderer != null)
                spriteRenderer.color = ORIGINAL_COLOR;
        }
        // duration이 0이면 효과 해제 요청 (GameManager.cancelBallSizeUp 호출 시)
        else // duration == 0f (Cancellation)
        {
            // 크기 확대가 적용 중이었으면 원래 크기로 복원
            if (isSizeUp)
            {
                transform.localScale = preBuffScale;

                // 색상도 원래 색상으로 복원
                if (spriteRenderer != null)
                    spriteRenderer.color = ORIGINAL_COLOR;
            }
            isSizeUp = false;
            effectTimer = 0f;
        }
    }

    // 아이템/코인 등 트리거 충돌 시 GameManager에 보고
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.invokeCollisionProcess(collision.gameObject);
    }

    // 블록 등 일반 충돌 시 GameManager에 보고
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.instance.invokeCollisionProcess(collision.gameObject);
    }
}