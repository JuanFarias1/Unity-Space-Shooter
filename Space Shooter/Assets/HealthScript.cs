using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour {

    public float Health;

    public void takeDamage(int currentHealth, int damageFactor) {

        currentHealth -= damageFactor;
    }


}
