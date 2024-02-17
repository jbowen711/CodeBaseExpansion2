using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100f; // Karakterin can değeri.

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health < 1)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died."); // Hata ayıklama mesajı.
        Destroy(gameObject);
    }
}

public class Character : MonoBehaviour
{
    public float attackDamage = 10f; // Karakterin saldırı hasarı.
    public float attackRate = 2f; // Karakterin saldırı hızı.
    private float nextAttackTime = 0f;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && Time.time >= nextAttackTime)
        {
            Health enemyHealth = collision.gameObject.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
}

public class Enemy : MonoBehaviour
{
    public float health = 50f; // Düşmanın can değeri.
    public float attackDamage = 5f; // Düşmanın saldırı hasarı.
    public float attackRate = 2f; // Düşmanın saldırı hızı.
    private float nextAttackTime = 0f;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && Time.time >= nextAttackTime)
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
}
