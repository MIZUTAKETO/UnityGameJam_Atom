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
    [SerializeField] GameObject gameplaySceneObject;
    [SerializeField] Renderer playerModelRenderer;

    Renderer[] playerModelRenderers;

    GameplayScene gameplayScene;


    Animator animator;

    int attackNum = 0;
    float attackNumResetTimer = 0;
    float attackCoolDown = 2.0f;

    bool isKnockBacking = false;
    float knockBackTimer = 0.0f;

    public bool isInvincible = false;
    float invincibleTimer = 0.0f;

    bool isBlinking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackHitBox.enabled = false;

        animator = GetComponentInChildren<Animator>();

        gameplaySceneObject = GameObject.Find("GameplayScene");

        gameplayScene = gameplaySceneObject.GetComponent<GameplayScene>();

        playerModelRenderers = GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isInvincible && !isBlinking)
        {
            StartCoroutine(Blink());
        }

        if (isKnockBacking) return;

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
        if(Gamepad.current != null)
        {
            movePlayer = Gamepad.current.leftStick.ReadValue();
        }
        else
        {
            if (Keyboard.current.wKey.isPressed)
            {
                movePlayer.y = 1.0f;
            }
            else if (Keyboard.current.sKey.isPressed)
            {
                movePlayer.y = -1.0f;
            }
            else
            {
                movePlayer.y = 0.0f;
            }

            if (Keyboard.current.dKey.isPressed)
            {
                movePlayer.x = 1.0f;
            }
            else if (Keyboard.current.aKey.isPressed)
            {
                movePlayer.x = -1.0f;
            }
            else
            {
                movePlayer.x = 0.0f;
            }
        }

        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        //プレイヤーの移動
        Vector3 moveDirection = camForward * movePlayer.y + camRight * movePlayer.x;

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x,1.0f,transform.position.z);

        if(transform.position.x > 250.0f)
        {
            transform.position = new Vector3(250.0f,transform.position.y,transform.position.z);
        }
        else if (transform.position.x < -250.0f)
        {
            transform.position = new Vector3(-250.0f, transform.position.y, transform.position.z);
        }

        if (transform.position.z > 250.0f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 250.0f);
        }
        else if (transform.position.z < -250.0f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -250.0f);
        }

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

        //無敵時間が0より大きければ
        if(invincibleTimer > 0.0f)
        {
            invincibleTimer -= Time.deltaTime;
        }
        else
        {
            if(isInvincible)
            {
                Debug.Log("無敵解除！");
                isInvincible = false;
            }
        }
    }

    public void Attack()
    {
        if(attackCoolDown >= 0.1f)
        {
            if (Gamepad.current != null)
            {
                if (Gamepad.current.xButton.wasPressedThisFrame)
                {
                    NormalAttack();
                }
                else if (Gamepad.current.rightShoulder.wasPressedThisFrame)
                {
                    FirstSkill();
                }
                else if (Gamepad.current.leftShoulder.wasPressedThisFrame)
                {
                    SecondSkill();
                }
            }
            else
            {
                if (Keyboard.current.hKey.wasPressedThisFrame)
                {
                    NormalAttack();
                }
                else if (Keyboard.current.jKey.wasPressedThisFrame)
                {
                    FirstSkill();
                }
                else if (Keyboard.current.kKey.wasPressedThisFrame)
                {
                    SecondSkill();
                }
            }
        }
    }

    void NormalAttack()
    {
        Debug.Log("攻撃のボタンを押したよ！");
        if (attackNum == 0)
        {
            animator.Play("attack", 0, 0.0f);
            attackNum = 1;
            attackNumResetTimer = 0.0f;
        }
        else if (attackNum == 1)
        {
            animator.Play("attack2", 0, 0.0f);
            attackNum = 0;
        }

        attackCoolDown = 0.0f;

        StartCoroutine(NormalAttackCoroutine());
    }

    void FirstSkill()
    {
        if (gameplayScene.comboGage >= GameplayScene.MAX_COMBO_GAGE / 2)
        {
            gameplayScene.comboGage -= GameplayScene.MAX_COMBO_GAGE / 2;
            animator.Play("slashWave", 0, 0.0f);
            Instantiate(slashWavePrefab, transform.position, transform.rotation);
        }
    }

    void SecondSkill()
    {
        if (gameplayScene.comboGage >= GameplayScene.MAX_COMBO_GAGE)
        {
            gameplayScene.comboGage -= GameplayScene.MAX_COMBO_GAGE;
            animator.Play("spinningSlash", 0, 0.0f);
            Instantiate(spinSlashWavePrefab, transform.position, Quaternion.identity);
        }
    }
    private IEnumerator NormalAttackCoroutine()
    {
        attackHitBox.enabled = true;

        yield return new WaitForSeconds(0.2f);

        attackHitBox.enabled = false;
    }

    public IEnumerator KnockBackCoroutine(Vector3 knockBackVelocity,float knockBackTime)
    {
        isKnockBacking = true;

        isInvincible = true;

        knockBackTimer = 0.0f;

        while (knockBackTimer < 1.0f)
        {
            knockBackTimer += Time.deltaTime / knockBackTime;

            float t  = 1 - (1 - knockBackTimer) * (1 - knockBackTimer);

            transform.position += Vector3.Lerp(knockBackVelocity, Vector3.zero, t);
            yield return null;
        }



        invincibleTimer = 1.0f;


        isKnockBacking = false;
    }

    IEnumerator Blink()
    {
        isBlinking = true;

        foreach(Renderer r in playerModelRenderers)
        {
            r.enabled = false;
        }

        yield return new WaitForSeconds(0.1f);

        foreach (Renderer r in playerModelRenderers)
        {
            r.enabled = true;
        }

        yield return new WaitForSeconds(0.1f);

        isBlinking = false;
    }
}
