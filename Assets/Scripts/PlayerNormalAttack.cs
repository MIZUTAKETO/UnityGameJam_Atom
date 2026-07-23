using UnityEngine;

public class PlayerNormalAttack : MonoBehaviour
{
    [SerializeField] Player player;
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
        Debug.Log("攻撃したよ！");

        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if(enemy != null)
            {
                enemy.Damage(player.attackDamage);
            }

            Debug.Log("当たったよ！");
        }

        enabled = false;
    }
}
