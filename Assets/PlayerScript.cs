using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    //移動速度
    float moveSpeed = 5.0f;

    //プレイヤーの移動
    Vector2 movePlayer;

    //カメラの移動
    Vector2 moveCamera;

    //カメラの回転をするためにカメラオブジェクトを使えるようにしたいな

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //プレイヤーの移動
    public void PlayerMove()
    {
        //左スティックの入力を取得
        movePlayer = Gamepad.current.leftStick.ReadValue();
        //右スティックの入力を取得
        moveCamera = Gamepad.current.rightStick.ReadValue();

        transform.position += new Vector3(movePlayer.x, 0, movePlayer.y) * moveSpeed * Time.deltaTime;
    }
}
