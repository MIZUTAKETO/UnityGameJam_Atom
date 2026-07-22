using UnityEngine;
using UnityEngine.UIElements;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject game;

    Vector3 moveVelocity = Vector3.zero;
    const float MOVE_ACCELERATION = 5.0f;
    const float MAX_SPEED = 3.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitMeleeEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        moveVelocity += (player.transform.position - transform.position).normalized * MOVE_ACCELERATION * Time.deltaTime;

        if(moveVelocity.magnitude > MAX_SPEED)
        {
            moveVelocity = Vector3.Normalize(moveVelocity) * MAX_SPEED;
        }

        transform.Translate(moveVelocity * Time.deltaTime);
    }

    void InitMeleeEnemy()
    {
        game = GameObject.Find("Game");
        player = game.GetComponent<Game>().GetPlayer();
    }
}
