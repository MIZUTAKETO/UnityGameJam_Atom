using UnityEngine;

public class Blink : MonoBehaviour
{
    CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void  Update()
    {
        canvasGroup.alpha = Mathf.PingPong(Time.time * 1f, 1f); ;
    }
}
