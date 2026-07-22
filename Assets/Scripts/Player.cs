using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //移動速度
    float moveSpeed = 5.0f;

    //カメラ感度
    //float lookSpeed = 100.0f;

    //プレイヤーの移動
    Vector2 movePlayer;
    Vector3 moveVelocity;
    Vector3 moveDirection;

    //カメラの回転
    Vector2 rotateCamera;

    //Transform player;
    //float xRotation;

    //ゲームカメラ
    //[SerializeField] GameObject MainCamera;
    public MainCamera cameraScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       cameraScript = Camera.main.GetComponent<MainCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    //プレイヤーの移動
    public void PlayerMove()
    {
        //左スティックの入力を取得
        movePlayer = Gamepad.current.leftStick.ReadValue();

        //右スティックの入力を取得
        //rotateCamera = Gamepad.current.rightStick.ReadValue();

        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        //プレイヤーの移動
        Vector3 moveDirection = camForward * movePlayer.y + camRight * movePlayer.x;

        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        //カメラの回転
        //cameraScript.CameraRotate(xRotation,rotateCamera,lookSpeed);

        //移動方向を向く
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                10.0f * Time.deltaTime
            );
        }
    }
}
