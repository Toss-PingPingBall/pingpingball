using System.Collections;
using UnityEngine;

public class BallSizeBuff : MonoBehaviour
{
    // 중첩 버프를 안전하게 처리하기 위해 코루틴 단위로 적용/해제
    public void Apply(float multiplier, float duration)
    {
        StartCoroutine(DoBuff(multiplier, duration));
    }

    private IEnumerator DoBuff(float multiplier, float duration)
    {
        // 현재 스케일에 배율 적용 (균일 스케일 전제)
        transform.localScale *= multiplier;

        // CircleCollider2D 등 2D 콜라이더는 스케일을 따라가므로 별도 조정 불필요
        yield return new WaitForSeconds(duration);

        // 같은 배율로 되돌림 (중첩된 경우에도 정확히 원복)
        transform.localScale /= multiplier;
    }
}