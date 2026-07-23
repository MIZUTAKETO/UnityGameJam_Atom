using UnityEngine;
using UnityEngine.UIElements;

public class MeleeEnemy : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        moveAcceleration = 5.0f;
        maxSpeed = 3.0f;
        health = 10.0f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        moveVelocity += (player.transform.position - transform.position).normalized * moveAcceleration * Time.deltaTime;

        if (moveVelocity.magnitude > maxSpeed)
        {
            moveVelocity = Vector3.Normalize(moveVelocity) * maxSpeed;
        }

        transform.Translate(moveVelocity * Time.deltaTime);
    }
}
