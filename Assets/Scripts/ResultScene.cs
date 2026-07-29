using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class ResultScene : MonoBehaviour
{
    [SerializeField] GameObject sceneLoaderObj;
    [SerializeField] GameObject cursor;

    [SerializeField] TMP_Text scoreText;

    SceneLoader sceneLoader;

    int selectNum = 0;
    bool isTilted = false;

    Vector2 stickInput;

    bool AfterResult = false;
    public AudioSource RestartSE;
    public AudioSource BackTitleSE;
    public AudioSource MoveSE;
    [SerializeField] float RestartwaitTime;
    [SerializeField] float BackTitlewaitTime;

    [SerializeField] TMP_Text highScoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = GameData.score.ToString();
        highScoreText.text = GameData.highScore.ToString();
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

        stickInput = Gamepad.current.leftStick.ReadValue();

        //カーソルが0.5より大きいとき(上入力)
        if (stickInput.y > 0.5 && !isTilted)
        {
            MoveSE.Play();

            selectNum--;
            if(selectNum < 0)
            {
                selectNum = 1;
            }

            isTilted = true;
        }

        //カーソルが-0.5より小さいとき(下入力)
        if (stickInput.y < -0.5 && !isTilted)
        {
            MoveSE.Play();

            selectNum++;
            selectNum = selectNum % 2;

            isTilted = true;
        }
       
        if(stickInput.y >= -0.5 && stickInput.y <= 0.5 && isTilted)
        {
            isTilted = false;
        }

        switch(selectNum)
        {
            case 0:
                cursor.transform.position = new Vector3(Screen.width / 2.0f, Screen.width / 3.5f);
                break;
            case 1:
                cursor.transform.position = new Vector3(Screen.width / 2.0f, Screen.width / 5.0f);
                break;
            default:
                break;
        }

        if(selectNum == 0)
        {
            if (AfterResult == false)
            {
                if (Gamepad.current.aButton.wasPressedThisFrame)
                {
                    StartCoroutine(Restart());
                }
            }
        }
        else if(selectNum == 1)
        {
            if (AfterResult == false)
            {
                if (Gamepad.current.aButton.wasPressedThisFrame)
                {
                    StartCoroutine(BackTitle());
                }
            }
        }
    }

    IEnumerator Restart()
    {
        AfterResult = true;
        RestartSE.Play();

        // 効果音が終わるまで待つ
        yield return new WaitForSeconds(RestartwaitTime);

        //鳴り終わったらシーン切り替え
        sceneLoader.ChangeScene(SceneLoader.GameScene.Gameplay);
    }

    IEnumerator BackTitle()
    {
        AfterResult = true;
        BackTitleSE.Play();

        // 効果音が終わるまで待つ
        yield return new WaitForSeconds(BackTitlewaitTime);

        //鳴り終わったらシーン切り替え
        sceneLoader.ChangeScene(SceneLoader.GameScene.Title);
    }
}
