using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("플레이어 데이터")]
    public PlayerDataSO data;

    [Header("바닥 체크")]
    public Transform groundCheck;                               //발밑 위치를 나타내는 트랜스폼

    public Rigidbody rb;

    private bool isGrounded;
    private bool jumpRequested;

    private float horizontal;
    private float vertical;

    public Joystick joystick;

    public Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (joystick == null)
            joystick = FindObjectOfType<Joystick>();
    }

    // Update is called once per frame
    void Update()
    {
        //horizontal = Input.GetAxis("Horizontal");
        //vertical = Input.GetAxis("Vertical");

        horizontal = joystick.inputDir.x;
        vertical = joystick.inputDir.y;

        float speed = new Vector3(horizontal, 0, vertical).magnitude;
        animator.SetFloat("Speed", speed);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpRequested = true;
            animator.SetBool("IsJumping", true);
        }
    }
    private void FixedUpdate()
    {
        PlayerMove();

        isGrounded = Physics.CheckSphere(
             groundCheck.position,
             data.groundCheckRadius,
             data.groundLayer
         );

        if (jumpRequested && isGrounded)
        {
            rb.AddForce(Vector3.up * data.jumpForce, ForceMode.VelocityChange);
            jumpRequested = false;
        }

        if (isGrounded)
            animator.SetBool("IsJumping", false);
    }
    void PlayerMove()
    {
        Vector3 currentVel = rb.velocity;

        Vector3 moveDir = (transform.forward * vertical) + (transform.right * horizontal);

        Vector3 newVel = new Vector3(moveDir.x * data.moveSpeed, currentVel.y, moveDir.z * data.moveSpeed);

        rb.velocity = Vector3.Lerp(rb.velocity, newVel, Time.deltaTime * 10.0f);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, data.groundCheckRadius);
    }
}
