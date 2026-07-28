using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MainCamera : MonoBehaviour
{
    //public Pivot CameraPivot;
    [SerializeField] GameObject player;

    //カメラ追従遅延
    [SerializeField] float smoothTime = 0.01f;

    [SerializeField] GameObject game;

    private Vector3 velocity = Vector3.zero;

    float cameraPositionAngle = 0.0f;

    const float CAMERA_POSITION_RADIUS = 14.0f;
    const float DEADZONE = 0.2f;
    const float CAMERA_MOVE_SPEED = 0.75f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitCamera();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LateUpdate()
    {
        float moveCam = Gamepad.current.rightStick.ReadValue().x;

        if (Mathf.Abs(moveCam) > DEADZONE)
        {
            cameraPositionAngle -= moveCam * CAMERA_MOVE_SPEED * Mathf.Deg2Rad;
        }

        Vector3 offset = new Vector3(Mathf.Cos(cameraPositionAngle) * CAMERA_POSITION_RADIUS, 10.0f, Mathf.Sin(cameraPositionAngle) * CAMERA_POSITION_RADIUS);

        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + offset, ref velocity, smoothTime);

        transform.LookAt(player.transform.position + Vector3.up * 1.0f);
    }

    void InitCamera()
    {
        game = GameObject.Find("GameplayScene");
        player = game.GetComponent<GameplayScene>().GetPlayer();
    }
}
