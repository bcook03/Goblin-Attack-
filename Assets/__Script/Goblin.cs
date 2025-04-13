using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    [Header("Dynamic")]
    public float speed = 0.5f;
    public float health = 1f;
    public float swingRate = 1.2f;
    public int damageOnHit = 1;
    public int score = 5;
    public int coin = 1;
    private Animator animator;

    public Vector3 pos {
        get {
             return this.transform.position;
        }
        set {
            this.transform.position = value;
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
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

    void OnCollisionEnter(Collision collision)
    {
        GameObject otherGo = collision.gameObject;
        Debug.Log("Goblin Collision: " + otherGo.name);
        Gate g = otherGo.GetComponent<Gate>();
        if (g != null) {
            animator.SetBool("isAttacking", true);
            speed = 0f;
            StartCoroutine(SwingRoutine(swingRate, g));
        }

    }

    void OnTriggerEnter(Collider other)
    {
        GameObject otherGo = other.gameObject;
        Gate g = otherGo.GetComponent<Gate>();
        if (g != null && g.health > 0) {
            animator.SetBool("isAttacking", true);
            speed = 0f;
            StartCoroutine(SwingRoutine(swingRate, g));
        }
    }

    IEnumerator SwingRoutine(float seconds, Gate g) {
        while (g != null && g.health > 0) {
            Swing(g);
            yield return new WaitForSeconds(seconds);
        }
    }

    void Swing(Gate G) {
        G.TakeDamage(damageOnHit);
        return;
    }

    public void Die() {
        speed = 0f;
        animator.SetBool("isDead", true);
        Destroy(this.gameObject, 4.2f);
    }

}
