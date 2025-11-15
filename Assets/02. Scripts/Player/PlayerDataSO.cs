using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/Player Data")]
public class PlayerDataSO : ScriptableObject
{
    [Header("이동")]
    public float moveSpeed = 8f;

    [Header("점프")]
    public float jumpForce = 7f;

    [Header("지면 체크")]
    public float groundCheckRadius = 0.3f;
    public LayerMask groundLayer;
}