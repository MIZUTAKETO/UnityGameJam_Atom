using UnityEngine;
using UnityEngine.InputSystem;

public class Result : MonoBehaviour
{
    Vector2 CursorMove;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CursorMove = Gamepad.current.leftStick.ReadValue();

        //カーソルが0.5より大きいとき(上入力)
        if (CursorMove.y > 0.5)
        {
            transform.position = new Vector3(460.0f, 260.0f);
        }

        //カーソルが-0.5より小さいとき(下入力)
        if(CursorMove.y < -0.5)
        {
            transform.position = new Vector3(460.0f, 180.0f);
        }
    }
}
