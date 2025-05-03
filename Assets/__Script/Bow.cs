using System.Collections;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [Header("Dynamic")]
    public GameObject launchPoint;
    public GameObject goblinCampPrefab;
    public Vector3 launchPos;
    public bool aimingMode;
    public GameObject projectile;
    private bool canShoot = true;

    [Header("Inscribed")]
    public GameObject projectilePrefab;
    public float velocityMult = 6f;
    public float shootCooldown = 1.5f;

    
    void Awake()    //Gets items and saves values for reference
    {
        Transform launchPointTrans = transform.Find("Launcher");
        launchPoint = launchPointTrans.gameObject;
        launchPos = launchPointTrans.position;
        
    }

    void OnMouseDown(){     //on mouse down, creates object
        if (!canShoot) return;
        aimingMode = true;
        //Transform launchPointTrans = transform.Find("Launcher");
        projectile = Instantiate(projectilePrefab) as GameObject;
        projectile.transform.position = this.transform.position;

        projectile.transform.parent = this.transform;
        projectile.transform.position += new Vector3(0, 1.5f, 0);
        projectile.GetComponent<Rigidbody>().isKinematic = true;
        projectile.GetComponentInChildren<TrailRenderer>().enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!aimingMode) return; //checks if aiming mode is on

        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 mouseDelta = mousePos3D - launchPos;    //checks mouse position in 2d

        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if(mouseDelta.magnitude > maxMagnitude){
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }   
        Vector3 arrowPos = launchPos + mouseDelta;
        projectile.transform.position = arrowPos; //makes it so the launch area is near the bow and arrow

        Transform spawnPoint = goblinCampPrefab.transform.Find("SpawnPoint");
        Vector3 pos = projectile.transform.position;
        pos.z = spawnPoint.position.z; 
        projectile.transform.position = pos;  //sets z position to what I want it to be. set in inspector

       Vector3 direction = mouseDelta.normalized;
       float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
       this.transform.rotation = Quaternion.Euler(angle, -110, 0);

        if(Input.GetMouseButtonUp(0)){  //if mouse goes up
            projectile.transform.parent = null;
            projectile.transform.rotation = this.transform.rotation;
            aimingMode = false;
            Rigidbody projRB = projectile.GetComponent<Rigidbody>();
            projRB.isKinematic = false;
            projRB.collisionDetectionMode = CollisionDetectionMode.Discrete;
            projRB.linearVelocity = -mouseDelta*velocityMult;

            projectile.GetComponentInChildren<TrailRenderer>().enabled = true;

            this.transform.rotation = Quaternion.Euler(0, -110, 0);

            projectile = null;
            StartCoroutine(BowDelay(shootCooldown));
        }
    }
    IEnumerator BowDelay(float wait) {
        canShoot = false;
        yield return new WaitForSeconds(wait);
        canShoot = true;
    }
}
