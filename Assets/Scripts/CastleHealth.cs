using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleHealth : MonoBehaviour
{

    [SerializeField] public float maxHealth = 100;
    public float currentHealth;
    public HealthBarUI healthBarUI;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
        healthBarUI.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("CastleDamaged");
        
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            

        }

    }

    public  void GameOver()
    {

    }
}
