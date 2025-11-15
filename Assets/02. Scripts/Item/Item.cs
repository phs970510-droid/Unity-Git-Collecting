using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    public ItemDataSO data;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        //점수 범위는 SO
        int amount = Random.Range(data.minScore, data.maxScore + 1);

        DataManager.Instance.AddItem(amount);

        //사운드
        if (data.pickupSound != null)
            AudioManager.Instance.PlaySoundEffect(data.pickupSound);

        //이펙트
        if (data.pickupEffect != null)
            Instantiate(data.pickupEffect, transform.position, Quaternion.identity);

        //풀로 복귀
        ItemSpawner spawner = FindObjectOfType<ItemSpawner>();
        spawner.ReturnToPool(this.gameObject);
    }
}