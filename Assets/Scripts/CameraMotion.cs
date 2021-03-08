using UnityEngine;

public class CameraMotion : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] bool enableMobileInput = false;
    [SerializeField] FixedTouchField touchField;
    [Header("相机旋转参数")]
    [SerializeField] float distanceToPlayer = 5.0f;      // 设置相机到角色的距离
    [SerializeField] float rotationSensitivity = 5.0f;   // 设置旋转的灵敏度
    [SerializeField] float rotationSmoothTime = 0.15f;   // 设置旋转平滑时间

    // 旋转轴
    private float Yaxis;
    private float Xaxis;

    // 上下旋转的最小值和最大值
    private float rotationMin = -10.0f; 
    private float rotationMax = 80.0f;

    // 线性插值时，用到的重载变量
    private Vector3 targetRotation;
    private Vector3 turnSmoothVelocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // 读取 FixedTouchField / 鼠标 输入
        if (enableMobileInput)
        {
            Yaxis += touchField.TouchDist.x * rotationSensitivity;
            Xaxis -= touchField.TouchDist.y * rotationSensitivity;
        }
        else
        {
            Yaxis += Input.GetAxis("Mouse X") * rotationSensitivity;
            Xaxis -= Input.GetAxis("Mouse Y") * rotationSensitivity;
        }


        // 旋转摄像机
        Xaxis = Mathf.Clamp(Xaxis, rotationMin, rotationMax);        // 限制摄像机上下的旋转范围(rotationMax ~ rotationMin)
        Vector3 rotation = new Vector3(Xaxis, Yaxis, 0.0f);          // 旋转的目标值
        targetRotation = Vector3.SmoothDamp(targetRotation, rotation, ref turnSmoothVelocity, rotationSmoothTime);  // 线性插值，使旋转更加平滑
        transform.eulerAngles = targetRotation;                      // 最后给摄像机赋值，旋转摄像机

        // 设置摄像机位置(绕 Target 目标旋转)
        transform.position = target.position - transform.forward * distanceToPlayer;
    }
}
