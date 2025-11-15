using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("아이템 프리팹")]
    public GameObject[] itemPrefab;

    [Header("스폰 영역 설정 (X,Z 기준)")]
    public Vector2 areaMin = new Vector2(-10f, -10f);
    public Vector2 areaMax = new Vector2(10f, 10f);

    [Header("지면 레이어 설정")]
    public LayerMask groundLayer;

    [Range(1, 50)]
    public int spawnCount = 10;

    public float minDistance = 1.5f;  // 겹침 방지
    public LayerMask itemLayer;

    private Queue<GameObject> itemPool = new Queue<GameObject>();

    private void Awake()
    {
        //풀 초기 생성
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject obj = Instantiate(itemPrefab[Random.Range(0, itemPrefab.Length)]);
            obj.SetActive(false);
            itemPool.Enqueue(obj);
        }
    }

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
                GameObject item = itemPool.Dequeue();
                item.transform.position = spawnPos;
                item.transform.rotation = Quaternion.identity;
                item.SetActive(true);
                placed++;
            }
        }
    }
    public void ReturnToPool(GameObject item)
    {
        item.SetActive(false);
        itemPool.Enqueue(item);
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
