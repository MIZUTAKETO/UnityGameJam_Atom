using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public enum GameScene
    {
        Title,
        Gameplay,
        Result
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    public void ChangeScene(GameScene sceneNum)
    {

        switch(sceneNum)
        {
            case GameScene.Title:
                StartCoroutine(LoadTitle());
                break;
            case GameScene.Gameplay:
                StartCoroutine(LoadGameplay());
                break;
            case GameScene.Result:
                StartCoroutine(LoadResult());
                break;
        }
    }

    IEnumerator LoadTitle()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(0);

        while(!op.isDone)
        {
            yield return null;
        }

        Destroy(gameObject);
    }

    IEnumerator LoadGameplay()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(1);

        while (!op.isDone)
        {
            yield return null;
        }
    }

    IEnumerator LoadResult()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(2);

        while (!op.isDone)
        {
            yield return null;
        }
    }

}
