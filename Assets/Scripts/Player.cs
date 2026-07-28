using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //移動速度
    float moveSpeed = 10.0f;

    //プレイヤーの移動
    Vector2 movePlayer;

    //攻撃力
     public float attackDamage = 10.0f;

    [SerializeField] private SphereCollider attackHitBox;
    [SerializeField] GameObject slashWavePrefab;
    [SerializeField] GameObject spinSlashWavePrefab;
    [SerializeField] GameObject gamePlayScene;


    Animator animator;

    int attackNum = 0;
    float attackNumResetTimer = 0;
    float attackCoolDown = 2.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackHitBox.enabled = false;

        animator = GetComponentInChildren<Animator>();

        gamePlayScene = GameObject.Find("GameplayScene");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        Attack();

        if(attackNum == 1)
        {
            attackNumResetTimer += Time.deltaTime;
            if(attackNumResetTimer > 1.0f)
            {
                attackNum = 0;
                attackNumResetTimer = 0.0f;
            }
        }

        if(attackCoolDown < 0.2f)
        {
            attackCoolDown += Time.deltaTime;
        }
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
        transform.position = new Vector3(transform.position.x,1.0f,transform.position.z);

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

            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        //向いた先に攻撃判定を置く。
        attackHitBox.transform.position = transform.position + transform.forward * 1.4f;
    }

    public void Attack()
    {
        if(Gamepad.current.xButton.wasPressedThisFrame && attackCoolDown >= 0.2f)
        {
            Debug.Log("攻撃のボタンを押したよ！");
            if(attackNum == 0)
            {
                animator.Play("attack",0,0.0f);
                attackNum = 1;
                attackNumResetTimer = 0.0f;
            }
            else if(attackNum == 1)
            {
                animator.Play("attack2",0,0.0f);
                attackNum = 0;
            }

            attackCoolDown = 0.0f;

            StartCoroutine(NormalAttack());
        }
        else if (Gamepad.current.rightShoulder.wasPressedThisFrame && attackCoolDown >= 0.2f)
        {
            if(gamePlayScene.GetComponent<GameplayScene>().comboGage >= 30)
            {
                gamePlayScene.GetComponent<GameplayScene>().comboGage -= 30;
                animator.Play("slashWave", 0, 0.0f);
                Instantiate(slashWavePrefab, transform.position, transform.rotation);
            }
        }
        else if (Gamepad.current.leftShoulder.wasPressedThisFrame && attackCoolDown >= 0.2f)
        {
            if(gamePlayScene.GetComponent<GameplayScene>().comboGage >= 60)
            {
                gamePlayScene.GetComponent<GameplayScene>().comboGage -= 60;
                animator.Play("spinningSlash", 0, 0.0f);
                Instantiate(spinSlashWavePrefab, transform.position, Quaternion.identity);
            }
        }


    }

    private IEnumerator NormalAttack()
    {
        attackHitBox.enabled = true;

        yield return new WaitForSeconds(0.1f);

        attackHitBox.enabled = false;
    }
}
