using UnityEngine;

public class SpinSlashWaveCollision : MonoBehaviour
{
    const float SPINSLASHWAVE_DAMAGE = 50.0f;
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

        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.Damage(SPINSLASHWAVE_DAMAGE,true);
            }

            //Debug.Log("当たったよ！");
        }
    }
}
