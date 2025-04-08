using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoblinAttack : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject goblinPrefab;
    public GameObject goblinCampPrefab;
    

    [Header("Dynamic")]
    public float spawnRate = 4f;
 

    private int goblinCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        InvokeRepeating(nameof(SpawnGoblin), 0f, spawnRate);

    }

    

    void SpawnGoblin()
    {
        if (goblinCount >= 5) return;
        Transform spawnPoint = goblinCampPrefab.transform.Find("SpawnPoint");
        Vector3 newPosition = spawnPoint.position;
        newPosition.y = goblinPrefab.transform.position.y;
        spawnPoint.position = newPosition;

        GameObject goblin = Instantiate(goblinPrefab, spawnPoint.position, Quaternion.Euler(0,0,0));
        goblinCount++;
    }
}
