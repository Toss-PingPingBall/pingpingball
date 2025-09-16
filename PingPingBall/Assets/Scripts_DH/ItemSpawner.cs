using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject sizeUpItemPrefab;

    [Header("Counts")]
    public int itemCount = 2;

    [Header("Spawn Area")]
    public Vector2 areaMin = new Vector2(-3f, -5f);
    public Vector2 areaMax = new Vector2(3f, 5f);
    public float minSpacing = 0.8f;     // 아이템 간 최소 간격
    public int maxPlacementTries = 40;  // 위치 찾기 최대 시도

    [Header("Physics Overlap (optional)")]
    public LayerMask overlapMask;
    // 겹치면 안 되는 레이어?

    private readonly List<Vector2> usedPositions = new List<Vector2>();

    void Start()
    {
        SpawnItems();
    }

    void SpawnItems()
    {
        if (sizeUpItemPrefab == null)
        {
            Debug.LogError("SizeUpItem Prefab이 비어 있습니다.");
            return;
        }

        for (int i = 0; i < itemCount; i++)
        {
            if (TryGetFreePosition(out Vector2 pos))
            {
                Instantiate(sizeUpItemPrefab, pos, Quaternion.identity);
            }
        }
    }

    bool TryGetFreePosition(out Vector2 pos)
    {
        for (int tries = 0; tries < maxPlacementTries; tries++)
        {
            Vector2 candidate = new Vector2(
                Random.Range(areaMin.x, areaMax.x),
                Random.Range(areaMin.y, areaMax.y)
            );

            if (IsFarEnough(candidate) && !IsOverlappingPhysics(candidate))
            {
                usedPositions.Add(candidate);
                pos = candidate;
                return true;
            }
        }
        pos = Vector2.zero;
        return false;
    }

    bool IsFarEnough(Vector2 candidate)
    {
        foreach (var p in usedPositions)
        {
            if (Vector2.Distance(candidate, p) < minSpacing) return false;
        }
        return true;
    }

    // 레이어 겹치기 관리
    bool IsOverlappingPhysics(Vector2 candidate)
    {
        if (overlapMask.value == 0) return false;
        const float radius = 0.35f;
        return Physics2D.OverlapCircle(candidate, radius, overlapMask) != null;
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 center = new Vector3((areaMin.x + areaMax.x) * 0.5f, (areaMin.y + areaMax.y) * 0.5f, 0f);
        Vector3 size   = new Vector3(Mathf.Abs(areaMax.x - areaMin.x), Mathf.Abs(areaMax.y - areaMin.y), 0f);
        Gizmos.DrawWireCube(center, size);
    }
#endif
}