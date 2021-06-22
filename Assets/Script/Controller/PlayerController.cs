using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ҿ�����
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// �ƶ��ٶ�
    /// </summary>
    public float MoveSpeed = 5.0f;
    /// <summary>
    /// ת���ٶ�
    /// </summary>
    public float RotateSpeed = 5.0f;

    private KeyCode lockMouseButton = KeyCode.Escape;
    private bool lockMouseFlag = true;

    private void FixedUpdate()
    {
        Rotate();
        Move();
    }

    private void Rotate() 
    {
        //if (Input.GetKeyDown(lockMouseButton)) {
        //    lockMouseFlag = !lockMouseFlag;
        //    Cursor.visible = lockMouseFlag;
        //}
        //if (lockMouseFlag) 
        //{
        //    float h = Input.GetAxis("Mouse X");
        //    float v = Input.GetAxis("Mouse Y");

        //    Vector3 direction = new Vector3(-v, h);

        //    if (h > 0.1 || h < -0.1 || v > 0.1 || v < -0.1)
        //    {
        //        print( h + "    " + v);
        //        direction *= RotateSpeed * Time.deltaTime;
        //    }


            if(Input.GetKey(KeyCode.RightArrow))
                transform.Rotate(new Vector3(0, 0.1f* RotateSpeed, 0), Space.World);
            else if(Input.GetKey(KeyCode.LeftArrow))
                transform.Rotate(new Vector3(0, -0.1f* RotateSpeed, 0), Space.World);

            if (Input.GetKey(KeyCode.UpArrow))
                transform.Rotate(new Vector3(0.1f* RotateSpeed, 0, 0), Space.Self);
            else if (Input.GetKey(KeyCode.DownArrow))
                transform.Rotate(new Vector3(-0.1f* RotateSpeed, 0, 0), Space.Self);

            //transform.Rotate(new Vector3(0.1f, 0, 0), Space.Self);




            //transform.rotation *= Quaternion.Euler(direction.x, Mathf.Clamp(direction.y, -30f, 30f), 0);
            //transform.Rotate(new Vector3(direction.x, Mathf.Clamp(direction.y, -30f, 30f), 0), Space.Self);
            //transform.forward = new Vector3(0, 0, 1);
        //}
    }

    private void Move()
    {
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        //float ud = 0f;
        //if (Input.GetKey(KeyCode.Space)) 
        //{
        //    ud += MoveSpeed * Time.deltaTime;
        //}
        //if(Input.GetKey(KeyCode.LeftShift))
        //{
        //    ud -= MoveSpeed * Time.deltaTime;
        //}

        //Vector3 Displacement = new Vector3(h, 0, v);

        //if (h > 0.1 || h < -0.1 || v > 0.1 || v < -0.1)
        //{
        //    Displacement *= MoveSpeed * Time.deltaTime;
        //}

        //Displacement = new Vector3(Displacement.x, ud, Displacement.z);

        //transform.Translate(Displacement, Space.World);
    }
}
