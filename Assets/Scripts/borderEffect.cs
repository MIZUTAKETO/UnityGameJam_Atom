using UnityEngine;

public class borderEffect : MonoBehaviour
{
    Vector3 endPos = new Vector3(0.0f,10.0f,0.0f);
    float endScale = 0.0f;

    LineRenderer line;

    float t = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime / 2.0f;

        transform.position = Vector3.Lerp(Vector3.zero, endPos, 1 - (1 - t) * (1 - t));
        line.widthMultiplier = Mathf.Lerp(1.0f, endScale, t * t);

        if(t > 1.0f)
        {
            Destroy(gameObject);
        }
    }
}
