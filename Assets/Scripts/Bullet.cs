using UnityEngine;

public class Bullet : MonoBehaviour
{

    const float MOVE_SPEED = 0.1f;
    float lifeTime = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //等速直線運動
        Vector3 velocity = transform.forward * MOVE_SPEED;
        transform .position += velocity;

        lifeTime -= Time.deltaTime;

        if(lifeTime < 0.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
