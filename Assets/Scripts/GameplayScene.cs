using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class GameplayScene : MonoBehaviour
{

    [SerializeField] public GameObject player;
    [SerializeField] public GameObject meleeEnemyPrefab;
    [SerializeField] public GameObject rangedEnemyPrefab;
    [SerializeField] public GameObject burlyEnemyPrefab;
    [SerializeField] public GameObject borderPrefab;
    [SerializeField] public GameObject playerPrefab;

    [SerializeField] GameObject sceneLoaderObj;

    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text comboText;

    [SerializeField] Image[] comboGageSprites;

    [SerializeField] Image skillOneImage;
    [SerializeField] Image skillTwoImage;

    [SerializeField] Sprite activeSkillOneSprite;
    [SerializeField] Sprite activeSkillTwoSprite;
    [SerializeField] Sprite inactiveSkillOneSprite;
    [SerializeField] Sprite inactiveSkillTwoSprite;

    SceneLoader sceneLoader;

    const float SPAWN_RADIUS = 40.0f;
    const float SPAWN_INTERVAL = 0.1f;
    const float BORDER_EFFECT_INTERVAL = 0.8f;

    float spawnTimer;
    float gameTimer;
    float borderEffectTimer;
    public float comboResetTimer;

    public int combo;
    public int comboGage;
    public const int MAX_COMBO_GAGE = 40;

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

            while(Mathf.Abs(player.transform.position.x + Mathf.Cos(spawnAngle) * SPAWN_RADIUS) > 250.0f || Mathf.Abs(player.transform.position.z + Mathf.Sin(spawnAngle) * SPAWN_RADIUS) > 250.0f)
            {
                spawnAngle += Mathf.PI / 4.0f;
            }

            int enemyType = Random.Range(0, 101);

            if(enemyType < 97)
            {
                Instantiate(meleeEnemyPrefab, player.transform.position + new Vector3(Mathf.Cos(spawnAngle) * SPAWN_RADIUS, 0.0f, Mathf.Sin(spawnAngle) * SPAWN_RADIUS), Quaternion.identity);
            }
            else if(enemyType >= 97 && enemyType < 99)
            {
                Instantiate(rangedEnemyPrefab, player.transform.position + new Vector3(Mathf.Cos(spawnAngle) * SPAWN_RADIUS, 0.0f, Mathf.Sin(spawnAngle) * SPAWN_RADIUS), Quaternion.identity);
            }
            else
            {
                Instantiate(burlyEnemyPrefab, player.transform.position + new Vector3(Mathf.Cos(spawnAngle) * SPAWN_RADIUS, 0.0f, Mathf.Sin(spawnAngle) * SPAWN_RADIUS), Quaternion.identity);
            }

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
            if((comboGage / (MAX_COMBO_GAGE / 10)) - 1 >= i)
            {
                comboGageSprites[i].enabled = true;
            }
            else
            {
                comboGageSprites[i].enabled = false;
            }
        }

        if(comboGage >= MAX_COMBO_GAGE / 2)
        {
            skillOneImage.sprite = activeSkillOneSprite;
        }
        else
        {
            skillOneImage.sprite = inactiveSkillOneSprite;
        }

        if (comboGage >= MAX_COMBO_GAGE)
        {
            skillTwoImage.sprite = activeSkillTwoSprite;
        }
        else
        {
            skillTwoImage.sprite = inactiveSkillTwoSprite;
        }

        if(combo > 0)
        {
            comboResetTimer -= Time.deltaTime;
            if(comboResetTimer <= 0.0f)
            {
                combo = 0;
                comboResetTimer = 5.0f;
            }
        }
    }

    void InitGameplay()
    {
        spawnTimer = 0.0f;
        gameTimer = 60.0f;
        comboResetTimer = 5.0f;

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
    public static int highScore = 0;
}
