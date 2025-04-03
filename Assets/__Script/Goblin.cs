using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    [Header("Dynamic")]
    public float speed = 0.5f;
    public float health = 10;
    public float swingRate = 0.3f;
    public int damageOnHit = 1;
    public int score = 5;

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
    }

    public void Move() {
        Vector3 tempPos = pos;
        tempPos.x += speed * Time.deltaTime;
        pos = tempPos;
    }

    // void OnCollisionEnter(Collision collision)
    // {
    //     GameObject otherGo = collision.gameObject;

    //     Gate g = otherGo.GetComponent<Gate>();
    //     Arrow a = otherGo.GetComponent<Arrow>();
    //     if (g != null) {
    //         speed = 0f;
    //         g.Swing();
    //     }
    //     else if (a != null) {
    //         health -= a.damageOnHit;
    //         if (health <= 0) {
    //             Destroy(gameObject);
    //         }
    //     }

    // } 

    void Swing() {
        
    }
}
