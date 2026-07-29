using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleScene : MonoBehaviour
{
    [SerializeField] GameObject sceneLoaderObj;

    SceneLoader sceneLoader;

    public AudioSource audioSource;
    bool AfterB = false;
    [SerializeField] float waitTime;

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

        if (AfterB == false)
        {
            if (Gamepad.current.aButton.wasPressedThisFrame)
            {
                StartCoroutine(TitleSE());
            }
        }
    }

    //Bボタンが押されたら実行
    IEnumerator TitleSE()
    {
        AfterB = true;
        audioSource.Play();

        // 効果音が終わるまで待つ
        yield return new WaitForSeconds(waitTime);

        //鳴り終わったらシーン切り替え
        sceneLoader.ChangeScene(SceneLoader.GameScene.Gameplay);
    }
}