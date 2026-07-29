using UnityEngine;

public class SlashWaveCollision : MonoBehaviour
{
    const float SLASHWAVE_DAMAGE = 30.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("攻撃したよ！");

        if (other.CompareTag("MeleeEnemy") || other.CompareTag("RangedEnemy") || other.CompareTag("BurlyEnemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.Damage(SLASHWAVE_DAMAGE,true);
            }

            //Debug.Log("当たったよ！");
        }
    }
}