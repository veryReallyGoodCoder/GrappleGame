using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterStatData", menuName = "Character/ CharacterStatData")]

public class CharacterStatData : ScriptableObject
{

    public string characterName = "John";
    public int maxHealth = 100;
    public int damage = -15;
    public int defense = 15;
    public int healRate = 10;

}
