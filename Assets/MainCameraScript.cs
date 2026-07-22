using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MainCameraScript : MonoBehaviour
{
    //public Pivot CameraPivot;
    public Transform player;
    Vector3 CameraPosition = new Vector3(10.0f, 4.0f, 10.0f);

    //カメラ追従遅延
    [SerializeField] float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + CameraPosition;
    }

    public void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.position, ref velocity, smoothTime);
        transform.LookAt(player);
    }

    //public void CameraRotate(float xRotation,Vector2 rotateCamera,float lookSpeed)
    //{
    //    xRotation -= rotateCamera.y * lookSpeed * Time.deltaTime;
    //    xRotation = Mathf.Clamp(xRotation, -80.0f, 80.0f);

    //    transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    //}
}
