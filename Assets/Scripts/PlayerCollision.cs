using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] GameObject gameplayScene;
    [SerializeField] GameObject playerGameObject;

    Player player;

    float meleeEnemyAttackTimer = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameplayScene = GameObject.Find("GameplayScene");
        playerGameObject = gameplayScene.GetComponent<GameplayScene>().player;
        player = playerGameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Bullet"))
        {
            Debug.Log(player.isInvincible);
            if (player.isInvincible) return;

            player.audioManager.playerDamagedSound.Play();

            Vector3 knockBackVelocity = transform.forward * 0.1f;

            gameplayScene.GetComponent<GameplayScene>().combo = 0;

            StartCoroutine(player.KnockBackCoroutine(knockBackVelocity, 1.0f));

            Bullet bullet = other.GetComponent<Bullet>();

            if (bullet != null)
            {
                bullet.Hit();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (player.isInvincible) return;

        if(other.CompareTag("MeleeEnemy"))
        {
            MeleeEnemy enemy = other.GetComponent<MeleeEnemy>();

            enemy.attackTimer += Time.deltaTime;

            if(enemy.attackTimer > 0.2f)
            {
                player.audioManager.playerDamagedSound.Play();

                Vector3 knockBackVelocity = transform.forward * 0.1f;

                gameplayScene.GetComponent<GameplayScene>().combo = 0;

                StartCoroutine(player.KnockBackCoroutine(knockBackVelocity, 0.5f));
            }
        }
        else if (other.CompareTag("BurlyEnemy"))
        {

            BurlyEnemy enemy = other.GetComponent<BurlyEnemy>();

            enemy.attackTimer += Time.deltaTime;

            if (enemy.attackTimer > 0.7f)
            {
                player.audioManager.playerDamagedSound2.Play();

                Vector3 knockBackVelocity = transform.forward * 0.6f;

                gameplayScene.GetComponent<GameplayScene>().combo = 0;

                StartCoroutine(player.KnockBackCoroutine(knockBackVelocity, 1.0f));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MeleeEnemy"))
        {
            MeleeEnemy enemy = other.GetComponent<MeleeEnemy>();

            enemy.attackTimer = 0.0f;
        }
        else if (other.CompareTag("BurlyEnemy"))
        {
            BurlyEnemy enemy = other.GetComponent<BurlyEnemy>();

            enemy.attackTimer = 0.0f;
        }
    }
}
