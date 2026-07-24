using UnityEngine;
using UnityEngine.InputSystem;

public class TitleScene : MonoBehaviour
{
    [SerializeField] GameObject sceneLoaderObj;

    SceneLoader sceneLoader;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(sceneLoaderObj == null)
        {
            sceneLoaderObj = GameObject.Find("SceneLoader");
            sceneLoader = sceneLoaderObj.GetComponent<SceneLoader>();
            return;
        }

        if (Gamepad.current.aButton.wasPressedThisFrame)
        {
            sceneLoader.ChangeScene(SceneLoader.GameScene.Gameplay);
        }
    }
}
