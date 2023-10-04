using UnityEngine;

public class Character : MonoBehaviour
{

    public CharacterStatData characterData;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        if (characterData)
        {
            currentHealth = characterData.maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        int damageToTake = damage - characterData.defense;
        currentHealth -= damageToTake > 0 ? damageToTake : 0;

        if(currentHealth <= 0)
        {
            Die();
        }

    }

    public void Die()
    {
        Debug.Log($"{characterData.characterName} has died!");
    }

}
