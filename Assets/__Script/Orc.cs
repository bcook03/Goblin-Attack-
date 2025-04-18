using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour
{
    [Header("Inscribed")]
    public float speed = 0.25f;
    public float health = 10f;
    public float swingRate = 2.25f;
    public int damageOnHit = 5;
    public int score = 10;
    public int coin = 2;
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

    public void TakeDamage(int damageOnHit) {
        health -= damageOnHit;
        if (health <= 0) {
            Destroy(gameObject);
            GoblinAttack ga = FindFirstObjectByType<GoblinAttack>();
            ga.AddCoins(coin);
        }
    }

    public void Die() {
        speed = 0f;
        animator.SetBool("isDead", true);
        Destroy(this.gameObject, 6f);
    }
}
