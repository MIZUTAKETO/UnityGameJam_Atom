using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //移動速度
    float moveSpeed = 5.0f;

    //プレイヤーの移動
    Vector2 movePlayer;

    //攻撃力
     public float attackDamage = 10.0f;

    [SerializeField] private SphereCollider attackHitBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackHitBox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        Attack();
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

        //向いた先に攻撃判定を置く。
        attackHitBox.transform.position = transform.position + transform.forward * 1.0f;
    }

    public void Attack()
    {
        if(Gamepad.current.xButton.wasPressedThisFrame)
        {
            Debug.Log("攻撃のボタンを押したよ！");
            StartCoroutine(NormalAttack());
        }
    }

    private IEnumerator NormalAttack()
    {
        attackHitBox.enabled = true;

        yield return new WaitForSeconds(0.1f);

        attackHitBox.enabled = false;
    }
}
