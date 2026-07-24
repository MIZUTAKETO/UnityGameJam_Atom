using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected GameObject player;
    [SerializeField] protected GameObject game;

    protected Vector3 moveVelocity = Vector3.zero;
    protected float moveAcceleration;
    protected float maxSpeed;

    protected float health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        InitCommon();
    }

    protected virtual void Update()
    {
        if(health <= 0.0f)
        {
            Dead();
        }
    }

    void InitCommon()
    {
        game = GameObject.Find("GameplayScene");
        player = game.GetComponent<GameplayScene>().GetPlayer();
    }

    protected void Dead()
    {
        Destroy(gameObject);
    }

    public void Damage(float damage)
    {
        health -= damage;
    }
}
