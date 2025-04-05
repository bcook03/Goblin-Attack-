using System;
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

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
    
    internal void TakeDamage(int damageOnHit)
    {
        health -= damageOnHit;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
