using UnityEngine;

public class GameplayScene : MonoBehaviour
{

    [SerializeField] public GameObject player;
    [SerializeField] public GameObject enemyPrefab;
    [SerializeField] public GameObject borderPrefab;
    [SerializeField] public GameObject playerPrefab;

    [SerializeField] GameObject sceneLoaderObj;

    SceneLoader sceneLoader;

    const float SPAWN_RADIUS = 20.0f;
    const float SPAWN_INTERVAL = 0.5f;
    const float BORDER_EFFECT_INTERVAL = 0.8f;

    float spawnTimer;
    float gameTimer;
    float borderEffectTimer;

    int score;
    int combo;
    int comboGage;

    void Awake()
    {
        InitGameplay();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (sceneLoaderObj == null)
        {
            sceneLoaderObj = GameObject.Find("SceneLoader");
            sceneLoader = sceneLoaderObj.GetComponent<SceneLoader>();
            return;
        }

        spawnTimer += Time.deltaTime;
        borderEffectTimer += Time.deltaTime;

        gameTimer -= Time.deltaTime;

        if (spawnTimer >= SPAWN_INTERVAL)
        {
            float spawnAngle = Random.Range(0.0f, Mathf.PI * 2.0f);
            Instantiate(enemyPrefab, player.transform.position + new Vector3(Mathf.Cos(spawnAngle) * SPAWN_RADIUS, 0.0f, Mathf.Sin(spawnAngle) * SPAWN_RADIUS), Quaternion.identity);

            spawnTimer = 0.0f;
        }

        if (borderEffectTimer >= BORDER_EFFECT_INTERVAL)
        {
            Instantiate(borderPrefab, Vector3.zero, Quaternion.identity);

            borderEffectTimer = 0.0f;
        }

        if (gameTimer <= 5.0f)
        {
            Debug.Log("あと少し！");
        }

        if (gameTimer <= 0.0f)
        {
            sceneLoader.ChangeScene(SceneLoader.GameScene.Result);
        }
    }

    void InitGameplay()
    {
        spawnTimer = 0.0f;
        gameTimer = 10.0f;

        player = Instantiate(playerPrefab, new Vector3(0.0f, 0.5f, 0.0f), Quaternion.identity);

        Debug.Log("初期化されたよ！");
    }

    public GameObject GetPlayer()
    {
        return player;
    }

}
