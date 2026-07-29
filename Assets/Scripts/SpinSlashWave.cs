using UnityEngine;

public class SpinSlashWave : MonoBehaviour
{
    Vector3 endScale;
    Vector3 startScale;

    float alpha = 1.0f;

    Renderer spinSlashWaveRenderer;
    Material spinSlashWaveMaterial;

    float t = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitSpinSlashWave();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime * 2.0f;
        transform.localScale = Vector3.Lerp(startScale, endScale, 1 - (1 - t) * (1 - t) * (1 - t));

        alpha = Mathf.Lerp(1.0f, 0.0f, t * t);

        Color color = spinSlashWaveMaterial.color;
        color.a = alpha;
        spinSlashWaveMaterial.color = color;

        if (t > 1.0f)
        {
            Destroy(gameObject);
        }
    }

    void InitSpinSlashWave()
    {
        startScale = Vector3.zero;
        endScale = new Vector3(5.0f, 1.0f, 5.0f);

        spinSlashWaveRenderer = gameObject.GetComponent<Renderer>();
        spinSlashWaveMaterial = spinSlashWaveRenderer.material;
    }
}
