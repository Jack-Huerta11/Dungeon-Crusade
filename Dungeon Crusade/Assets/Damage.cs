using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damageAmount = 25;

    // Called when something with a Collider enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object has a CharacterStats component
        CharacterStats targetStats = other.GetComponent<CharacterStats>();

        // If the object has CharacterStats, apply damage
        if (targetStats != null)
        {
            targetStats.TakeDamage(damageAmount);
        }
    }
}