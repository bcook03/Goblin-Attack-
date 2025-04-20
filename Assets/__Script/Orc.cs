using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Orc : MonoBehaviour
{
    [Header("Inscribed")]
    public float speed = 0.25f;
    public GameObject healthBar;
    public float health = 10f;
    private float maxhealth = 10f;
    public float swingRate = 2.25f;
    public int damageOnHit = 5;
    public int score = 10;
    public int coin = 2;
    private Animator animator;
    private Scrollbar hb;

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
        hb = healthBar.GetComponent<Scrollbar>();
    }

    void Update()
    {
        Move();    
    }

    public void Move() {
        Vector3 tempPos = pos;
        tempPos.x += speed * Time.deltaTime;
        pos = tempPos;
        Vector3 healthParPos = this.transform.position + new Vector3(0f, 5f, 0f);
        healthBar.transform.position = Camera.main.WorldToScreenPoint(healthParPos);
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject otherGo = other.gameObject;
        Gate g = otherGo.GetComponent<Gate>();
        if (g != null && g.health > 0) {
            animator.SetBool("IsAttacking", true);
            speed = 0f;
            StartCoroutine(SwingRoutine(swingRate, g));
        }
    }

    IEnumerator SwingRoutine(float seconds, Gate g) {
        while (g != null && g.health > 0) {
            yield return StartCoroutine(Swing(g));
            yield return new WaitForSeconds(seconds);
            
        }
    }

    IEnumerator Swing(Gate G) {
        yield return new WaitForSeconds(1.40f);
        G.TakeDamage(damageOnHit);
    }

    public void TakeDamage(int damageOnHit) {
        
        health -= damageOnHit;
        hb.size = health / maxhealth;

        if (health <= 0) {
            healthBar.SetActive(false);
            Die();
        }
    }

    public void Die() {
        GoblinAttack ga = FindFirstObjectByType<GoblinAttack>();
        ga.AddCoins(coin);
        speed = 0f;
        animator.SetBool("isDead", true);
        StopCoroutine("SwingRoutine");
        Destroy(this.gameObject, 6.25f);
    }
}
