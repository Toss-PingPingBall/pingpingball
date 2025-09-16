using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject basicBlockPrefab;
    public GameObject oneShotBlockPrefab;
    public GameObject portalEntryPrefab;
    public GameObject portalExitPrefab;

    [Header("Counts")]
    public int basicBlockCount = 5;
    public int oneShotBlockCount = 4;
    public int portalPairs = 1;

    [Header("Spawn Area")]
    public Vector2 areaMin = new Vector2(-5f, -3f);
    public Vector2 areaMax = new Vector2(5f, 3f);
    public float minSpacing = 0.8f;
    public int maxPlacementTries = 50;

    [Header("Physics Overlap (optional)")]
    public LayerMask overlapMask; // 블럭 레이어를 지정하면 겹침 검사를 물리로도 수행

    private readonly List<Vector2> usedPositions = new List<Vector2>();

    void Start()
    {
        SpawnMany(basicBlockPrefab, basicBlockCount);
        SpawnMany(oneShotBlockPrefab, oneShotBlockCount);
        SpawnPortalPairs();
    }

    void SpawnMany(GameObject prefab, int count)
    {
        if (prefab == null) return;
        for (int i = 0; i < count; i++)
        {
            if (TryGetFreePosition(out Vector2 pos))
            {
                Instantiate(prefab, pos, Quaternion.identity);
            }
        }
    }

    void SpawnPortalPairs()
    {
        for (int i = 0; i < portalPairs; i++)
        {
            if (!TryGetFreePosition(out Vector2 entryPos)) continue;
            if (!TryGetFreePosition(out Vector2 exitPos)) continue;

            GameObject entry = Instantiate(portalEntryPrefab, entryPos, Quaternion.identity);
            GameObject exit  = Instantiate(portalExitPrefab,  exitPos,  Quaternion.identity);

            Portal p = entry.GetComponent<Portal>();
            if (p != null) p.exitTransform = exit.transform;
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

    bool IsOverlappingPhysics(Vector2 candidate)
    {
        if (overlapMask.value == 0) return false; // 사용 안 하면 스킵
        const float radius = 0.4f;
        return Physics2D.OverlapCircle(candidate, radius, overlapMask) != null;
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 center = new Vector3((areaMin.x + areaMax.x) * 0.5f, (areaMin.y + areaMax.y) * 0.5f, 0f);
        Vector3 size   = new Vector3(Mathf.Abs(areaMax.x - areaMin.x), Mathf.Abs(areaMax.y - areaMin.y), 0f);
        Gizmos.DrawWireCube(center, size);
    }
#endif
}