using UnityEngine;
using UnityEngine.UIElements;

public class RangedEnemy : Enemy
{

    const float ATTACK_RANGE = 15.0f;
    const float SHOT_COOLDOWN = 2.0f;
    float shotTimer = 0.0f;


    [SerializeField] GameObject bulletPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        moveAcceleration = 5.0f;
        maxSpeed = 3.0f;
        health = 20.0f;
        score = 250;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        Vector3 diff = player.transform.position - transform.position;

        moveVelocity += (player.transform.position - transform.position).normalized * moveAcceleration * Time.deltaTime;

        if (ATTACK_RANGE * ATTACK_RANGE < diff.sqrMagnitude)
        {

            if (moveVelocity.sqrMagnitude > maxSpeed * maxSpeed)
            {
                moveVelocity = Vector3.Normalize(moveVelocity) * maxSpeed;
            }


            transform.Translate(moveVelocity * Time.deltaTime, Space.World);
            transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);
        }

        Quaternion targetRotation = Quaternion.LookRotation(moveVelocity);

        transform.rotation = targetRotation;

        shotTimer += Time.deltaTime;

        if(shotTimer > SHOT_COOLDOWN)
        {
            Instantiate(bulletPrefab, transform.position, transform.rotation);
            shotTimer = 0.0f;
        }
    }
}
