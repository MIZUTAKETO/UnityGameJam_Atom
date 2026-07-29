using UnityEngine;

public class SlashWave : MonoBehaviour
{
    Vector3 velocity = Vector3.zero;
    Vector3 initialVelocity = Vector3.zero;
    const float INITIALMOVESPEED = 0.3f;

    float alpha = 1.0f;

    Renderer slashWaveRenderer;
    Material slashWaveMaterial;

    float t = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitSlashWave();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        velocity = Vector3.Lerp(initialVelocity, Vector3.zero, 1 - (1 - t) * (1 - t));

        transform.position += velocity;

        alpha = Mathf.Lerp(1.0f, 0.0f, t * t);

        Color color = slashWaveMaterial.color;
        color.a = alpha;
        slashWaveMaterial.color = color;

        if(t > 1.0f)
        {
            Destroy(gameObject);
        }
    }

    void InitSlashWave()
    {
        initialVelocity = transform.forward * INITIALMOVESPEED;

        slashWaveRenderer = gameObject.GetComponent<Renderer>();
        slashWaveMaterial = slashWaveRenderer.material;
    }
}
