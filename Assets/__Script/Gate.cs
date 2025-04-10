using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [Header("Instance")]
    public int health = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
    }
    
    internal void TakeDamage(int damageOnHit)
    {
        health -= damageOnHit;
        if (health <= 0) {
            Destroy(gameObject);
            GoblinAttack.GameOver();
        }
    }
}
