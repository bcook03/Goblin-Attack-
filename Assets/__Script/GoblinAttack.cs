using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GoblinAttack : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject goblinPrefab;
    public GameObject goblinCampPrefab;
    public GameObject orcPrefab;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI waveText;
    public float goblinSpawnRate = 4f;  
    public float minGoblinSpawnRate = 2f;
    public float maxGoblinCount = 5;
    public float maxGoblinSpawn = 10;
    public float orcSpawnRate = 6f;
    public float minOrcSpawnRate = 4f;
    public float maxOrcCount = 1;
    public float maxOrcSpawn = 5;
    
    [Header("Dynamic")]
    public int waveCount = 1;
    public int maxWaveCount = 10;
    public int goblinSpawned = 0;
    public int orcSpawned = 0;
    public int coins = 0;
    private int goblinCount = 0;
    private int orcCount = 0;
    void Start()
    {
        InvokeRepeating(nameof(SpawnGoblin), 0f, goblinSpawnRate);
        
    }

    void Update()
    {
        coinText.text = "Coins: " + coins.ToString(); // Updates the coin text
        
        waveText.text = "Wave: " + waveCount.ToString() + " of " + maxWaveCount.ToString(); // Updates the wave text
        
    }

    void SpawnGoblin()
    {
        if (goblinCount >= maxGoblinCount) {
           CancelInvoke(nameof(SpawnGoblin));
           InvokeRepeating(nameof(AreGoblinsDefeated), 1f, 3f);
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

    void SpawnOrc()
    {
        if (orcCount >= maxOrcCount) {
            CancelInvoke(nameof(SpawnOrc));
            return;
        }
        if (orcSpawned < maxOrcSpawn) {
            Transform spawnPoint = goblinCampPrefab.transform.Find("OrcSpawn");
            Vector3 newPosition = spawnPoint.position;
            newPosition.y = orcPrefab.transform.position.y;
            spawnPoint.position = newPosition;

            GameObject orc = Instantiate(orcPrefab, spawnPoint.position, Quaternion.Euler(0, 90, 0));
            orcCount++;
            orcSpawned++;
        }
    }
    
    public void AreGoblinsDefeated()
    {
        Goblin[] goblins = FindObjectsByType<Goblin>(FindObjectsSortMode.None); // Checks if there are any goblins left
        Orc[] orcs = FindObjectsByType<Orc>(FindObjectsSortMode.None);       // Checks if there are any orcs left
        if (goblins.Length == 0 && orcs.Length == 0)  {                     // If there are none left, then it moves to the next wave
            NextWave();
        }
        else {
            goblinSpawned = goblins.Length;
            orcSpawned = orcs.Length;
        }

    }

    public void ResetGoblinCount()
    {
        goblinCount = 0;
        orcCount = 0;
    }

    public void NextWave() {
        goblinSpawnRate -= 0.5f;        // Decreases the spawn rate for goblins
        maxGoblinCount *= 1.5f;         // Increases the max goblin count
        if (goblinSpawnRate < minGoblinSpawnRate) {     // Checks if the spawn rate is less than the minimum
            goblinSpawnRate = minGoblinSpawnRate;
        }
        
        ResetGoblinCount();         // Resets the goblin count
        waveCount++;
        if (waveCount > maxWaveCount) {     // Checks if the wave count is greater than the maximum to win
            SceneManager.LoadScene("Victory");
        }
        else {                              // Else, invokes the spawn methods
            InvokeRepeating(nameof(SpawnGoblin), 0f, goblinSpawnRate);
            if (waveCount > 3){
                InvokeRepeating(nameof(SpawnOrc), 0f, orcSpawnRate);
            }
        }
    }

    public void GameOver() { // If the gate is destroyed, then it stops all goblin and orc attacks and start walking and later on loads the game over scene
        Goblin[] goblins = FindObjectsByType<Goblin>(FindObjectsSortMode.None);
        Orc[] orcs = FindObjectsByType<Orc>(FindObjectsSortMode.None);
        foreach (Goblin g in goblins) {
            g.GetComponent<Animator>().SetBool("isAttacking", false);
            g.speed = 0.5f;
        }
        foreach (Orc o in orcs) {
            o.GetComponent<Animator>().SetBool("IsAttacking", false);
            o.speed = 0.25f;
        }
        StartCoroutine(GameOverDelay(8f));        
    }

    IEnumerator GameOverDelay(float wait) { // Delays the game over scene for 5 seconds
        yield return new WaitForSeconds(wait);
        SceneManager.LoadScene("GameOver");
    }

    public void AddCoins(int amount) { // Adds or removes coins from the player
        coins += amount;
        if (coins < 0) {
            coins = 0;
        }
    }
}
