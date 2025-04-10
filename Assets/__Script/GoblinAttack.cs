using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoblinAttack : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject goblinPrefab;
    public GameObject goblinCampPrefab;
    public float spawnRate = 4f;  
    public float maxGoblinCount = 5;
    public int waveCount = 1;
    public int maxWaveCount = 10;

    
    private int goblinCount = 0;
    void Start()
    {
        InvokeRepeating(nameof(SpawnGoblin), 0f, spawnRate);
    }

    void SpawnGoblin()
    {
        if (goblinCount >= maxGoblinCount) return;
        Transform spawnPoint = goblinCampPrefab.transform.Find("SpawnPoint");
        Vector3 newPosition = spawnPoint.position;
        newPosition.y = goblinPrefab.transform.position.y;
        spawnPoint.position = newPosition;

        GameObject goblin = Instantiate(goblinPrefab, spawnPoint.position, Quaternion.Euler(0,0,0));
        goblinCount++;
    }
    
    public void GoblinDied()
    {
        goblinCount--;
    }

    public void ResetGoblinCount()
    {
        goblinCount = 0;
    }

    public void NextWave() {
        spawnRate -= 0.5f;
        maxGoblinCount *= 1.5f;
        if (spawnRate < 2f) {
            spawnRate = 2f;
        }
        ResetGoblinCount();
        waveCount++;
        if (waveCount > maxWaveCount) {
            SceneManager.LoadScene("Victory");
        }
    }

    public static void GameOver() {
        SceneManager.LoadScene("GameOver");
    }
}
