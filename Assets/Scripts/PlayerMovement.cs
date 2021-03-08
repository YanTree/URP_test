using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;             // 获取 Controller 组件
    private Animator animator;                          // 获取 animator 组件
    [SerializeField] float gravity = -9.81f;            // 重力加速度
    [SerializeField] bool enableMobileInput = false;
    [SerializeField] FixedJoystick joystick;

    [Header("玩家参数")]
    [SerializeField] float speed = 12.0f;               // 设置玩家移动速度
    [SerializeField] float jumpHeight = 2.0f;           // 跳一次有多高
    [SerializeField] float rotationSmoothTime = 0.25f;  // 设置玩家旋转的平滑时间
    [SerializeField] float moveSmoothTime = 0.2f;       // 设置玩家移动的平滑时间

    [Header("地面检测")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckerRadius = 0.4f;
    [SerializeField] LayerMask groundMask;

    [Header("按钮检测")]
    [SerializeField] FixedButton shouldJump;

    // 线性插值时，用到的重载变量
    private float turnSmoothVelocity;
    private float moveSmoothVelocity;
    private float currentVelocity;

    Transform cameraTransform;                          // 获取摄像机的 transform
    Vector3 velocity;
    float magnitude = 0.25f;
    Vector3 input = Vector3.zero;                       // 输入  
    Vector3 direction;                                  // 角色移动方向
    AudioSource auido;


    // bool 参数
    bool isGround;                                      // 是否在地面上
    bool shoulMove;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        auido = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // 检测是否在 Ground 上
        isGround = Physics.CheckSphere(groundCheck.position, groundCheckerRadius, groundMask); 

        if (isGround && velocity.y < 0.0f)
        {
            velocity.y = -2.0f;
        }

        // 读取 Joystick / 键盘 输入（WASD）
        if (enableMobileInput)
        {
            input = new Vector3(joystick.input.x, 0.0f, joystick.input.y);
        }
        else
        {
            input = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        }

        direction = input.normalized;

        // player 旋转，根据键盘的输入值
        if(direction != Vector3.zero)
        {
            // 以键盘输入(WASD)，转化成角度
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            // 线性插值
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSmoothTime);
        }

        // player 移动
        float targetVelocity = direction.magnitude * speed;
        currentVelocity = Mathf.SmoothDamp(currentVelocity, targetVelocity, ref moveSmoothVelocity, moveSmoothTime);
        controller.Move(transform.forward * currentVelocity * Time.deltaTime);
        //transform.Translate(transform.forward * currentVelocity * Time.deltaTime, Space.World);

        // player 跳跃
        if (shouldJump.sholudJump() && isGround)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
            FindObjectOfType<AudioManager>().Play("Jump");
        }

        // player 自由落体运动
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);            // 跳跃

        // 判断是否播放动画
        shoulMove = targetVelocity > magnitude;

        // 播放动画
        animator.SetBool("Isrun", shoulMove);

        if(isGround)
            FindObjectOfType<AudioManager>().PlaySound("Run", shoulMove);
    }

    void PlaySound(AudioSource audio)
    {
        if (!audio.isPlaying)
            auido.Play();
        else
            audio.Stop();
    }
    /*[SerializeField] float speed = 10.0f;
    [SerializeField] float gravity = -9.81f;       // 重力加速度
    [SerializeField] float jumpHeight = 2.0f;      // 跳一次有多高

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckerRadius = 0.4f;
    [SerializeField] LayerMask groundMask;

    private CharacterController controller;
    private Vector3 velocity;

    Animator animator;
    bool isGround;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics.CheckSphere(groundCheck.position, groundCheckerRadius, groundMask); // 检测是否在 Ground 上

        if (isGround && velocity.y < 0.0f)
        {
            velocity.y = -2.0f;
        }

        // Reading the Input 
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0.0f, vertical);

        // Moving 
        if (movement.magnitude > 0.3f)
        {
            movement.Normalize();
            movement *= speed * Time.deltaTime;
            transform.Translate(movement, Space.World);
        }

        // Animating
        float velocityZ = Vector3.Dot(movement.normalized, transform.forward);
        float velocityX = Vector3.Dot(movement.normalized, transform.right);

        animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
        animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
    }*/
}
