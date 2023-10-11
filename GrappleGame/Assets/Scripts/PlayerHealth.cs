using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    
    public int health = 100;

    public UnityEvent OnDeath;

    private void Update()
    {
        TakeDamage(5);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            health = 0;

            OnDeath.Invoke();
        }
    }

}
