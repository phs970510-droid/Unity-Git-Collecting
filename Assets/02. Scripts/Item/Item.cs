using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    [Header("점수 범위")]
    public int minScore = 1;
    public int maxScore = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        int amount = Random.Range(minScore, maxScore + 1);

        Destroy(gameObject);
    }
}