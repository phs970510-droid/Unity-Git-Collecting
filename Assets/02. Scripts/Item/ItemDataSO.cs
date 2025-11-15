using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Game/Item Data")]
public class ItemDataSO : ScriptableObject
{
    [Header("점수 범위")]
    public int minScore;
    public int maxScore;

    [Header("수집 사운드")]
    public AudioClip pickupSound;

    [Header("수집 이펙트")]
    public ParticleSystem pickupEffect;
}