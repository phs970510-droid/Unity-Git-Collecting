using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
   [SerializeField] private Camera cam;
    [SerializeField] private Transform playerBody;
    [SerializeField] private float sensitivity = 3f;

    private float pitch = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mx = Input.GetAxis("Mouse X") * sensitivity;
        float my = Input.GetAxis("Mouse Y") * sensitivity;

        //플레이어 자체 좌우 회전
        if (playerBody != null)
        {
            playerBody.Rotate(Vector3.up * mx);
        }

        //카메라 상하 회전
        pitch -= my;
        pitch = Mathf.Clamp(pitch, -85f, 85f);
        cam.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}
