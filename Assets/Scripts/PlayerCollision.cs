using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] GameObject gameplayScene;
    [SerializeField] GameObject playerGameObject;

    Player player;

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
        Debug.Log(player.isInvincible);
        if (player.isInvincible) return;

        if (other.CompareTag("Bullet"))
        {
            Vector3 knockBackVelocity = transform.forward * 0.1f;

            StartCoroutine(player.KnockBackCoroutine(knockBackVelocity, 1.0f));

            Bullet bullet = other.GetComponent<Bullet>();

            if (bullet != null)
            {
                bullet.Hit();
            }
        }
    }
}
