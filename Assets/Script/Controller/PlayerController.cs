using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家控制器
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// 移动速度
    /// </summary>
    public float MoveSpeed = 5.0f;
    /// <summary>
    /// 转向速度
    /// </summary>
    public float RotateSpeed = 1.0f;

    private KeyCode lockMouseButton = KeyCode.Escape;
    private bool lockMouseFlag = true;

    private void Awake()
    {
        Cursor.visible = true;
    }

    private void FixedUpdate()
    {
        Rotate();
        Move();
    }

    private void Rotate() 
    {
        if (Input.GetKeyDown(lockMouseButton))
        {
            lockMouseFlag = !lockMouseFlag;
            Cursor.visible = lockMouseFlag;
        }
        if (lockMouseFlag)
        {
            float h = Input.GetAxis("Mouse X");
            float v = Input.GetAxis("Mouse Y");

            Vector3 horizontal = new Vector3(0, h, 0);
            Vector3 vertical = new Vector3(-v, 0, 0);

            if (h > 0.1 || h < -0.1)
            {
                horizontal *= RotateSpeed;
                transform.Rotate(horizontal, Space.World);
            }

            if (v > 0.1 || v < -0.1) 
            {
                vertical *= RotateSpeed;
                transform.Rotate(vertical, Space.Self);
                //transform.rotation = Quaternion.Euler(Mathf.Clamp(transform.rotation.x, -90f, 90f), transform.rotation.y, 0);
            }

        }
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float ud = 0f;
        if (Input.GetKey(KeyCode.Space))
        {
            ud += MoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ud -= MoveSpeed * Time.deltaTime;
        }

        Vector3 Displacement = new Vector3(h, 0, v);

        if (h > 0.1 || h < -0.1 || v > 0.1 || v < -0.1)
        {
            Displacement *= MoveSpeed * Time.deltaTime;
        }

        Displacement = new Vector3(Displacement.x, ud, Displacement.z);

        transform.Translate(Displacement, Space.Self);
    }
}
