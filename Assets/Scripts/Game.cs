using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject borderPrefab;
    [SerializeField] GameObject playerPrefab;

    const float SPAWN_RADIUS = 20.0f;
    const float SPAWN_INTERVAL = 0.5f;
    const float BORDER_EFFECT_INTERVAL = 0.8f;

    float spawnTimer;
    float gameTimer;
    float borderEffectTimer;
    int score;
    int combo;
    int comboGage;


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
        borderEffectTimer += Time.deltaTime;

        if(spawnTimer >= SPAWN_INTERVAL)
        {
            float spawnAngle = Random.Range(0.0f, Mathf.PI * 2.0f);
            Instantiate(enemyPrefab, player.transform.position + new Vector3(Mathf.Cos(spawnAngle) * SPAWN_RADIUS, 0.0f, Mathf.Sin(spawnAngle) * SPAWN_RADIUS), Quaternion.identity);

            spawnTimer = 0.0f;
        }

        if (borderEffectTimer >= BORDER_EFFECT_INTERVAL)
        {
            Instantiate(borderPrefab,Vector3.zero,Quaternion.identity);

            borderEffectTimer = 0.0f;
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
