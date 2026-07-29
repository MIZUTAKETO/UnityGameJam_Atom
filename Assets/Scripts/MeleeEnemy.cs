using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class MeleeEnemy : Enemy
{
    public float attackTimer = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        moveAcceleration = 5.0f;
        maxSpeed = 3.0f;
        health = 10.0f;
        score = 100;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        moveVelocity += (player.transform.position - transform.position).normalized * moveAcceleration * Time.deltaTime;

        if (moveVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            moveVelocity = Vector3.Normalize(moveVelocity) * maxSpeed;
        }

        transform.Translate(moveVelocity * Time.deltaTime,Space.World);

        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);

        Vector3 direction = moveVelocity;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            10.0f * Time.deltaTime
        );
    }
}
