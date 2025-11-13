using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("아이템 프리팹")]
    public GameObject itemPrefab;

    [Header("스폰 영역 설정 (X,Z 기준)")]
    public Vector2 areaMin = new Vector2(-10f, -10f);
    public Vector2 areaMax = new Vector2(10f, 10f);

    [Header("지면 레이어 설정")]
    public LayerMask groundLayer;

    [Range(1, 50)]
    public int spawnCount = 10;

    public float minDistance = 1.5f;  // 겹침 방지
    public LayerMask itemLayer;

    private void Start()
    {
        SpawnItems();
    }

    private void SpawnItems()
    {
        int placed = 0;
        int safety = 500;

        while (placed < spawnCount && safety > 0)
        {
            safety--;

            // 1) XZ 범위에서 랜덤 위치 선택
            float x = Random.Range(areaMin.x, areaMax.x);
            float z = Random.Range(areaMin.y, areaMax.y);

            // 2) 높은 곳에서 Raycast 발사
            Vector3 rayOrigin = new Vector3(x, 100f, z);
            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, Vector3.down, out hit, 200f, groundLayer))
            {
                Vector3 spawnPos = hit.point;

                // 3) 주변에 아이템 겹치면 무시
                if (Physics.CheckSphere(spawnPos, minDistance, itemLayer))
                    continue;

                // 4) 아이템 생성
                Instantiate(itemPrefab, spawnPos, Quaternion.identity);
                placed++;
            }
            // else → 평평한 지면 못 찾으면 재시도 (레이어 설정 문제)
        }
    }

    //에디터에서 영역 표시용 기즈모
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Vector3 center = new Vector3(
            (areaMin.x + areaMax.x) * 0.5f,
            0f,
            (areaMin.y + areaMax.y) * 0.5f
        );

        Vector3 size = new Vector3(
            Mathf.Abs(areaMax.x - areaMin.x),
            1f,
            Mathf.Abs(areaMax.y - areaMin.y)
        );

        Gizmos.DrawWireCube(center, size);
    }
}
