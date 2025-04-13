using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Gate : MonoBehaviour
{
    GoblinAttack ga;
    [Header("Instance")]
    public int health = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        ga = FindFirstObjectByType<GoblinAttack>();
    }
    
    internal void TakeDamage(int damageOnHit)
    {
        health -= damageOnHit;
        if (health <= 0) {
            Transform t = gameObject.transform;
            Destroy(t.GetChild(0).gameObject);
            ga.GameOver();
        }
    }
}
