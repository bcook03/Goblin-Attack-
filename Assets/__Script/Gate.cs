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
        healthBar.SetActive(false);
        health = maxHealth;
        Rigidbody rb = GetComponent<Rigidbody>();
        ga = FindFirstObjectByType<GoblinAttack>();
        hb = healthBar.GetComponent<Scrollbar>();
        hb.size = health / maxHealth;
    }
    void Update()
    {
        if (ga.coins < 10 || health >= maxHealth) {
            UnityEngine.UI.Button RE = GameObject.Find("Repair").GetComponent<UnityEngine.UI.Button>();
            RE.interactable = false;
        }
        else {
            UnityEngine.UI.Button RE = GameObject.Find("Repair").GetComponent<UnityEngine.UI.Button>();
            RE.interactable = true;
        }    
    }

    internal void TakeDamage(int damageOnHit)
    {
        healthBar.SetActive(true);
        health -= damageOnHit;
        hb.size = (health) / maxHealth;
        
        if (health <= 0) {
            healthBar.SetActive(false);
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
