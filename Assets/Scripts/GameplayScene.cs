using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class GameplayScene : MonoBehaviour
{

    [SerializeField] public GameObject player;
    [SerializeField] public GameObject enemyPrefab;
    [SerializeField] public GameObject borderPrefab;
    [SerializeField] public GameObject playerPrefab;

    [SerializeField] GameObject sceneLoaderObj;

    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text comboText;

    [SerializeField] Image[] comboGageSprites;

    SceneLoader sceneLoader;

    const float SPAWN_RADIUS = 40.0f;
    const float SPAWN_INTERVAL = 0.1f;
    const float BORDER_EFFECT_INTERVAL = 0.8f;

    float spawnTimer;
    float gameTimer;
    float borderEffectTimer;

    public int combo;
    public int comboGage;

    void Awake()
    {
        InitGameplay();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < comboGageSprites.Length; i++)
        {
            comboGageSprites[i].enabled = false;
        }
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

        int minutes = (int)((gameTimer + 1) / 60);
        int seconds = (int)((gameTimer + 1) % 60);

        timerText.text = $"{minutes:00}:{seconds:00}";

        scoreText.text = GameData.score.ToString();

        comboText.text = combo.ToString();

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

        if (gameTimer <= 0.0f)
        {
            sceneLoader.ChangeScene(SceneLoader.GameScene.Result);
        }

        for(int i = 0; i < 10; i++)
        {
            if((comboGage / 6) - 1 >= i)
            {
                comboGageSprites[i].enabled = true;
            }
            else
            {
                comboGageSprites[i].enabled = false;
            }
        }
    }

    void InitGameplay()
    {
        spawnTimer = 0.0f;
        gameTimer = 60.0f;

        GameData.score = 0;

        player = Instantiate(playerPrefab, new Vector3(0.0f, 0.5f, 0.0f), Quaternion.identity);

        Debug.Log("初期化されたよ！");
    }

    public GameObject GetPlayer()
    {
        return player;
    }

}

public static class GameData
{
    public static int score = 0;
}
