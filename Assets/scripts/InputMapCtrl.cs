using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMapCtrl : MonoBehaviour
{
    private Rigidbody2D player_body;
    private Animator player_animator;

    public PlayerProps player;
    public MyInput myInput;
    public Joystick VJoystick;

    private void Awake()
    {
        //綁定frame rate，否則手機上會有明顯卡頓
        Application.targetFrameRate = 60;
        //实例化input system生成的MyInput脚本
        myInput = new MyInput();
        //初始化私有變數
        player_body = GetComponent<Rigidbody2D>();
        player_animator = transform.Find("Char").GetComponent<Animator>();
    }
    void OnEnable()
    {
        //使用前需要将myInput开启
        myInput.Dirs.Enable();
    }
    void OnDisable()
    {
        //使用完需要将myInput关闭
        myInput.Dirs.Disable();
    }
    //将获取Move输入方法写在Update方法中逐帧调用
    private void Update()
    {
        getCameraControlInput();
    }
    //使用rigidbody2D實現有碰撞偵測的位移行為時，必須在FixedUpdate方法中調用
    void FixedUpdate()
    {
        // 確認是否有有效的輸入來執行移動
        if (!GetInput_Move())  // 如果鍵盤或手柄沒有輸入
        {
            GetInput_MoveByVJoystick();  // 則檢查虛擬搖桿輸入
        }

        //GetInput_Jump(); // 跳躍輸入
    }

    // 獲取 Move 鍵盤或手柄輸入，返回是否檢測到有效輸入
    private bool GetInput_Move()
    {
        Vector2 moveVector2 = myInput.Dirs.Move.ReadValue<Vector2>();

        // 設置動畫參數
        SetAnimParamsByVector(moveVector2);

        if (moveVector2 != Vector2.zero) // 檢查輸入是否非零
        {
            PlayerMove(moveVector2.x, moveVector2.y, "kb or gp");
            //Debug.Log($"h:{moveVector2.x}, v:{moveVector2.y}");
            //Debug.Log($"total:{Mathf.Abs(moveVector2.x) + Mathf.Abs(moveVector2.y)}");
            return true;  // 有有效輸入
        }

        return false;  // 無有效輸入
    }

    // 獲取虛擬搖桿的移動輸入
    private void GetInput_MoveByVJoystick()
    {
        Vector2 moveVector2 = new Vector2(VJoystick.Horizontal, VJoystick.Vertical);

        // 設置動畫參數
        SetAnimParamsByVector(moveVector2);

        if (moveVector2 != Vector2.zero)
        {
            PlayerMove(moveVector2.x, moveVector2.y, "joystick");
        }
    }
    private void SetAnimParamsByVector(Vector2 moveVector2)
    {
        player_animator.SetFloat("horizontal", moveVector2.x);
        player_animator.SetFloat("vertical", moveVector2.y);
        player_animator.SetFloat("movespeed", Mathf.Abs(moveVector2.x) + Mathf.Abs(moveVector2.y));

        if (moveVector2 != Vector2.zero) // 檢查輸入是否非零
        {
            if (moveVector2.x < 0)
            {
                player_animator.SetFloat("mirror", 0);
            }
            else if (moveVector2.x > 0)
            {
                player_animator.SetFloat("mirror", 1);
            }
        }
    }
    private void PlayerMove(float horizontal, float vertical, string source)
    {
        //每次都從PlayerProps中取值
        float movespeed = player.Movespeed;
        // 執行移動
        Vector2 pos = player_body.position; // 使用 Rigidbody2D 的 position
        float moveH = movespeed * Time.fixedDeltaTime * horizontal;
        float moveV = movespeed * Time.fixedDeltaTime * vertical;
        pos.x = pos.x + moveH; // 使用 moveVector2.x 進行移動
        pos.y = pos.y + moveV; // 使用 moveVector2.y 進行移動

        // 使用 Rigidbody2D 的 MovePosition 方法來移動角色
        player_body.MovePosition(pos);
        //Debug.Log($"{source}:({moveH},{moveV})→目前位置:{pos.x},{pos.y}");
    }
    //获取Jump输入方法
    //private void GetInput_Jump()
    //{
    //    //将读取到的Jump返回值赋值给isJump 
    //    bool isJump = myInput.Dirs.Jump.IsPressed();
    //    //判断是否有按下对应的Jump按键
    //    if (isJump)
    //    {
    //        PlayerJump();
    //    }
    //}
    public void Jump(bool jumpState)
    {
        //player_animator.SetBool("Jump", jumpState);//未添加跳躍動畫控制
        player_animator.enabled = !jumpState;
        player_body.simulated = !jumpState;//跳躍時包含碰撞與位移都取消模擬
    }
    //public void OnJump(InputAction.CallbackContext ctx) //UnityEngine event
    //{
    //    if (ctx.phase == InputActionPhase.Performed)
    //    {
    //        Debug.Log("jump");
    //    }
    //}

    public void OnJump(InputValue value) //message or broadcast
    {
        bool isAction1Pressed = value.isPressed;
        Debug.Log(isAction1Pressed);
        if (isAction1Pressed)
        {
            Jump(true);
        }
    }

    //获取CameraControl输入方法
    private void getCameraControlInput()
    {
        //将读取到的CameraControl返回值赋值给cameraOffset 
        Vector2 cameraOffset = myInput.Dirs.Camera.ReadValue<Vector2>();
        //判断是否有鼠标是否有产生偏移
        if (cameraOffset != Vector2.zero)
        {
            //将获取到的鼠标偏移值打印出来
            Debug.Log(cameraOffset);
        }
    }
}
