using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    [Header("점수 범위")]
    public int minScore = 1;
    public int maxScore = 3;

    [Header("수집 사운드")]
    public AudioClip pickupSound;

    [Header("수집 이펙트")]
    public ParticleSystem pickupEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        int amount = Random.Range(minScore, maxScore + 1);

        DataManager.Instance.AddItem(amount);

        if (pickupSound != null)
            AudioManager.Instance.PlaySoundEffect(pickupSound);

        if (pickupEffect != null)
            Instantiate(pickupEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}