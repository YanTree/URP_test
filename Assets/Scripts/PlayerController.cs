using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// first controller
public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    [SerializeField] float Speed = 40.0f;     // 移动速度
    [SerializeField] float gravity = -9.81f;  // 重力加速度
    [SerializeField] float jumpHeight = 2.0f; // 跳一次有多高

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckerRadius = 0.4f;
    [SerializeField] LayerMask groundMask;

    private Vector3 velocity;
    bool isGround;

    // Update is called once per frame
    void Update()
    {
        isGround = Physics.CheckSphere(groundCheck.position, groundCheckerRadius, groundMask);

        if(isGround && velocity.y < 0.0f)
        {
            velocity.y = -2.0f;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        controller.Move(move * Speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGround)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}

