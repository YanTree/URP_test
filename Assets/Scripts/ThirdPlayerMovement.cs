using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPlayerMovement : MonoBehaviour
{
    private CharacterController controller;        // 获取 Controller 组件
    private Animator animator;                     // 获取 Animator 组件

    [SerializeField] float speed = 12.0f;          // 设置玩家移动速度
    [SerializeField] float gravity = -9.81f;       // 重力加速度
    [SerializeField] float jumpHeight = 2.0f;      // 跳一次有多高
    //[SerializeField] float turnSmoothTime = 0.2f;  // 设置平滑时间

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckerRadius = 0.4f;
    [SerializeField] LayerMask groundMask;

    private Vector3 velocity;
    private Vector2 _look;
    bool isGround;
    bool shouldMove;

    private float turnSmoothVelocity;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics.CheckSphere(groundCheck.position, groundCheckerRadius, groundMask); // 检测是否在 Ground 上

        if (isGround && velocity.y < 0.0f)
        {
            velocity.y = -2.0f;
        }

        float horizontal = Input.GetAxis("Horizontal"); // 返回 -1(A)/1(D)
        float vertical = Input.GetAxis("Vertical");     // 返回 -1(S)/1(W)
        Vector3 direction = new Vector3(horizontal, 0.0f, vertical).normalized; // 归一化处理

        shouldMove = direction.magnitude >= 0.1f;       // 判断是否在移动

        if (direction.magnitude >= 0.1f)
        {
            controller.Move(direction * speed * Time.deltaTime); // 移动
        }

        if (Input.GetButtonDown("Jump") && isGround)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);            // 跳跃

        animator.SetBool("Isrun", shouldMove);
    }
}
