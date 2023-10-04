using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        Sword sword = new Sword();
        Bow bow = new Bow();

        List<Weapon> weapons = new List<Weapon> {sword, bow};

        foreach (Weapon weapon in weapons)
        {
            weapon.Attack();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
