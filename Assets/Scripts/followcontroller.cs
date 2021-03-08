using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followcontroller : MonoBehaviour
{
    private Vector2 _move;
    private Vector2 _look;

    public Vector3 nextPosition;
    private Quaternion nextRotation;

    [SerializeField] float rotationPower = 3f;
    [SerializeField] float rotationLerp = 0.5f;

    //[SerializeField] float speed = 1f;


    public GameObject followTransform;

    void MouseInput()            // 返回鼠标在屏幕空间的值
    {
        _look.x = Input.GetAxis("Mouse X");
        _look.y = Input.GetAxis("Mouse Y");
    }
    void MoveInput()             // 返回 WASD 的输入值
    {
        _move.x = Input.GetAxis("Horizontal");
        _move.y = Input.GetAxis("Vertical");
    }
    private void Update()
    {
        MouseInput();
        MoveInput();


        #region Follow Transform Rotation

        //根据 Mouse X 值来旋转 Follw Target
        followTransform.transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);

        #endregion

        #region Vertical Rotation
        followTransform.transform.rotation *= Quaternion.AngleAxis(_look.y * rotationPower, Vector3.right);

        var angles = followTransform.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTransform.transform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }

        followTransform.transform.localEulerAngles = angles;
        #endregion

        nextRotation = Quaternion.Lerp(followTransform.transform.rotation, nextRotation, Time.deltaTime * rotationLerp);

        if (_move.x == 0 && _move.y == 0) // 当玩家没有移动时，玩家保持不动
        {
            nextPosition = transform.position;
            return;
        }
        /*
        float moveSpeed = speed / 100f;
        Vector3 position = (transform.forward * _move.y * moveSpeed) + (transform.right * _move.x * moveSpeed);
        nextPosition = transform.position + position;
        */

        //Set the player rotation based on the look transform
        transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);

        //reset the y rotation of the look transform
        followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
    }
}