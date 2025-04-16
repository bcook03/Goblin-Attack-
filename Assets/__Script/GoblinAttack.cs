using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoblinAttack : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject goblinPrefab;
    public GameObject goblinCampPrefab;
    public float spawnRate = 4f;  
    public float maxGoblinCount = 5;
    public float maxGoblinSpawn = 10;
    
    [Header("Dynamic")]
    public int waveCount = 1;
    public int maxWaveCount = 10;
    public int goblinSpawned = 0;

    private int goblinCount = 0;
    void Start()
    {
        InvokeRepeating(nameof(SpawnGoblin), 0f, spawnRate);
    }

    void SpawnGoblin()
    {
        if (goblinCount >= maxGoblinCount) {
           CancelInvoke(nameof(SpawnGoblin));
           InvokeRepeating(nameof(AreGoblinsDefeated), 1f, 5f);
           return;
        }
        if (goblinSpawned < maxGoblinSpawn){
            Transform spawnPoint = goblinCampPrefab.transform.Find("SpawnPoint");
            Vector3 newPosition = spawnPoint.position;
            newPosition.y = goblinPrefab.transform.position.y;
            spawnPoint.position = newPosition;

            GameObject goblin = Instantiate(goblinPrefab, spawnPoint.position, Quaternion.Euler(0,90,0));
            goblinCount++;
            goblinSpawned++;
        }
    }
    
    public void AreGoblinsDefeated()
    {
        Goblin[] goblins = FindObjectsByType<Goblin>(FindObjectsSortMode.None);
        if (goblins.Length == 0) {
            NextWave();
        }
        else goblinSpawned = goblins.Length;
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
        else {
            InvokeRepeating(nameof(SpawnGoblin), 0f, spawnRate);
        }
    }

    public void GameOver() {
        Goblin[] goblins = FindObjectsByType<Goblin>(FindObjectsSortMode.None);
        foreach (Goblin g in goblins) {
            g.GetComponent<Animator>().SetBool("isAttacking", false);
            g.speed = 0.5f;
        }
        StartCoroutine(GameOverDelay(20f));        
    }

    IEnumerator GameOverDelay(float wait) {
        yield return new WaitForSeconds(wait);
        SceneManager.LoadScene("GameOver");
    }
}
