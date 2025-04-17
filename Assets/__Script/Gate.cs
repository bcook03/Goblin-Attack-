using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{
    GoblinAttack ga;
    [Header("Instance")]
    public float health;
    public int maxHealth = 100;
    public GameObject healthBar;
    private Scrollbar hb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar.active = false;
        health = maxHealth;
        Rigidbody rb = GetComponent<Rigidbody>();
        ga = FindFirstObjectByType<GoblinAttack>();
        hb = healthBar.GetComponent<Scrollbar>();
        hb.size = health / maxHealth;
    }
    
    internal void TakeDamage(int damageOnHit)
    {
        healthBar.active = true;
        health -= damageOnHit;
        hb.size = (health) / maxHealth;
        
        if (health <= 0) {
            healthBar.active = false;
            Transform t = gameObject.transform;
            Destroy(t.GetChild(0).gameObject);
            ga.GameOver();
        }
    }

    public void RepairGate() {
        if (health < maxHealth) {
            health += 5;
            if (health > maxHealth) health = maxHealth;
            hb.size = health / maxHealth;
        }
        GoblinAttack ga = FindFirstObjectByType<GoblinAttack>();
        ga.AddCoins(-10);
    }
}
