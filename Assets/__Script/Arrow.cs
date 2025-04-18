using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Arrow : MonoBehaviour
{
    public int damageOnHit = 1;
    public static float deleteWhen = -20f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < deleteWhen){  //If arrow misses goblin, will dissapear into floor and delete itself.
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        GameObject otherGo = other.gameObject;
        Goblin g = otherGo.GetComponent<Goblin>();
        Orc O = otherGo.GetComponent<Orc>();
        if (g != null) {
            g.health -= damageOnHit;
            if (g.health <= 0) {
                g.Die();
            }
            Destroy(this.gameObject);
        }
        if (O != null) {
            O.health -= damageOnHit;
            if (O.health <= 0) {
                O.Die();
            }
            Destroy(this.gameObject);
        }
    }
}
