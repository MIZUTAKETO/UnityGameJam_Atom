using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected GameObject player;
    [SerializeField] protected GameObject gameplayScene;

    protected Vector3 moveVelocity = Vector3.zero;
    protected float moveAcceleration;
    protected float maxSpeed;

    protected float health;

    protected int score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        InitCommon();
    }

    protected virtual void Update()
    {

    }

    void InitCommon()
    {
        gameplayScene = GameObject.Find("GameplayScene");
        player = gameplayScene.GetComponent<GameplayScene>().GetPlayer();
    }

    protected void Dead(bool isKilledBySkill)
    {
        if(isKilledBySkill)
        {
            GameData.score += score;
            gameplayScene.GetComponent<GameplayScene>().combo++;
            Destroy(gameObject);
        }
        else
        {
            GameData.score += score;
            gameplayScene.GetComponent<GameplayScene>().combo++;
            if (gameplayScene.GetComponent<GameplayScene>().comboGage < 60)
            {
                gameplayScene.GetComponent<GameplayScene>().comboGage++;
            }
            Destroy(gameObject);
        }

    }

    public void Damage(float damage,bool isDamagedBySkill)
    {
        health -= damage;

        if (health <= 0.0f)
        {
            Dead(isDamagedBySkill);
        }
    }
}
