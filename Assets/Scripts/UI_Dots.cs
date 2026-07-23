using Unity.VisualScripting;
using UnityEngine;

public class UI_Dots : MonoBehaviour
{
    [SerializeField] float MoveSpeedY;
    [SerializeField] float MoveSpeedX;
    Vector3 DotPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DotPosition = new Vector3(Random.Range(0.0f, 900.0f),Random.Range(-500.0f, 0.0f));
        transform.position = DotPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(MoveSpeedX, MoveSpeedY) * Time.deltaTime;
        if (transform.position.x < DotPosition.x + 1000.0f)
        {
            MoveSpeedX *= -1;
        }

        if (transform.position.y >= 600.0f)
        {
            transform .position = DotPosition;
        }
    }
}
