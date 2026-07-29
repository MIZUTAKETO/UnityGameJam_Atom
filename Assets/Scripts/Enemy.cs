using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected GameObject player;
    [SerializeField] protected GameObject gameplaySceneObject;

    GameplayScene gameplayScene;

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
        gameplaySceneObject = GameObject.Find("GameplayScene");
        player = gameplaySceneObject.GetComponent<GameplayScene>().GetPlayer();

        gameplayScene = gameplaySceneObject.GetComponent<GameplayScene>();
    }

    protected void Dead(bool isKilledBySkill)
    {
        if(isKilledBySkill)
        {
            GameData.score += score;
            gameplayScene.combo++;

            gameplayScene.comboResetTimer = 5.0f;

            Destroy(gameObject);
        }
        else
        {
            GameData.score += score;
            gameplayScene.combo++;

            gameplayScene.comboResetTimer = 5.0f;

            if (gameplayScene.comboGage < GameplayScene.MAX_COMBO_GAGE)
            {
                gameplayScene.comboGage++;
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
