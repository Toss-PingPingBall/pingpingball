using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform exitTransform;
    [Range(0f, 1f)] public float enterSpeedReduce = 0.2f; // -0.2
    public float exitSpeedBoost = 0.3f;                   // +0.3
    public float cooldown = 0.25f;                        // 재진입 방지

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ball")) return;
        if (exitTransform == null) return;

        var cd = other.GetComponent<PortalCooldown>();
        if (cd != null && Time.time - cd.lastTeleportTime < cooldown) return;

        var rb = other.attachedRigidbody;
        if (rb != null) rb.linearVelocity *= (1f - enterSpeedReduce);

        other.transform.position = exitTransform.position;

        if (rb != null) rb.linearVelocity *= (1f + exitSpeedBoost);

        if (cd == null) cd = other.gameObject.AddComponent<PortalCooldown>();
        cd.lastTeleportTime = Time.time;
    }
}

public class PortalCooldown : MonoBehaviour
{
    [HideInInspector] public float lastTeleportTime = -999f;
}
