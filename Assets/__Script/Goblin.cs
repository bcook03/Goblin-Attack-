using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    [Header("Dynamic")]
    public float speed = 0.5f;
    public float health = 10;
    public float swingRate = 0.3f;
    public int damageOnHit = 1;
    public int score = 5;
    public int coin = 1;

    public Vector3 pos {
        get {
             return this.transform.position;
        }
        set {
            this.transform.position = value;
        }
    }

    void Update()
    {
        Move();

        if (health <= 0) {
            
            Destroy(gameObject);
        }    
    }

    public void Move() {
        Vector3 tempPos = pos;
        tempPos.x += speed * Time.deltaTime;
        pos = tempPos;
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject otherGo = collision.gameObject;

        Gate g = otherGo.GetComponent<Gate>();
        //Arrow a = otherGo.GetComponent<Arrow>();
        if (g != null) {
            speed = 0f;
            StartCoroutine(WaitForSeconds(swingRate, g));
        } else
            Debug.Log("No gate found!");
        // else if (a != null) {
        //     health -= a.damageOnHit;
        //     if (health <= 0) {
        //         Destroy(gameObject);
        //     }
        // }

    } 

    IEnumerator WaitForSeconds(float seconds, Gate g) {
        while (g != null && g.health > 0) {
            Swing(g);
            yield return new WaitForSeconds(seconds);
        }
    }

    void Swing(Gate G) {
        G.TakeDamage(damageOnHit);
        return;
    }

}
