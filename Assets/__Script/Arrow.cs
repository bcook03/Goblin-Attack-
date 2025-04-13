using UnityEngine;

public class Arrow : MonoBehaviour
{
    public static float deleteWhen = -20f;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < deleteWhen){  //If arrow misses goblin, will dissapear into floor and delete itself.
            Destroy(this.gameObject);
        }
    }
}
