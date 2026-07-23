using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject playerPrefab;

    const float SPAWN_RADIUS = 20.0f;
    const float SPAWN_INTERVAL = 1.0f;

    float spawnTimer;
    float gameTimer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        InitGame();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if(spawnTimer >= SPAWN_INTERVAL)
        {
            float spawnAngle = Random.Range(0.0f, Mathf.PI * 2.0f);
            Instantiate(enemyPrefab, player.transform.position + new Vector3(Mathf.Cos(spawnAngle) * SPAWN_RADIUS, 0.0f, Mathf.Sin(spawnAngle) * SPAWN_RADIUS), Quaternion.identity);

            spawnTimer = 0.0f;
        }
    }

    void InitGame()
    {
        spawnTimer = 0.0f;
        gameTimer = 90.0f;

        player = Instantiate(playerPrefab, new Vector3(0.0f, 0.5f, 0.0f), Quaternion.identity);
    }

    public GameObject GetPlayer()
    {
        return player;
    }
}
