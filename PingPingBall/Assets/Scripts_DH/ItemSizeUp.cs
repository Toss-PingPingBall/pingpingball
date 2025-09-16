using UnityEngine;

public class ItemSizeUp : MonoBehaviour
{
    [Header("Effect")]
    public float scaleMultiplier = 1.8f;   // 공 크기 배율 (1.5~2.0 권장)
    public float durationSeconds = 10f;    // 지속 시간

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ball")) return;

        // 공에 크기 버프 컨트롤러가 없으면 추가
        var buff = other.GetComponent<BallSizeBuff>();
        if (buff == null) buff = other.gameObject.AddComponent<BallSizeBuff>();

        buff.Apply(scaleMultiplier, durationSeconds);

        // 아이템은 즉시 소멸
        Destroy(gameObject);
    }
}
