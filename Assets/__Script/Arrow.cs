using Unity.VisualScripting;
using UnityEngine;

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
        if (g != null) {
            g.health -= damageOnHit;
            if (g.health <= 0) {
                g.Die();
            }
            Destroy(this.gameObject);
        }
    }
}
