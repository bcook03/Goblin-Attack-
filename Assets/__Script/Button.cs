using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Button : MonoBehaviour
{
    GoblinAttack goblinAttack;
    void Start()
    {
        goblinAttack = FindObjectsByType<GoblinAttack>(FindObjectsSortMode.None)[0];
    }
    public void GameStart() {
        SceneManager.LoadScene(0);
    }

    public void ReturnToMenu(){
        SceneManager.LoadScene(3);
    }

    public void ClearGoblins(){
        Goblin[] goblins = FindObjectsByType<Goblin>(FindObjectsSortMode.None);
        foreach (Goblin goblin in goblins) {
            Destroy(goblin.gameObject);    
        }
        goblinAttack.AreGoblinsDefeated();
    }

    public void Quit()
    {
        
    }
}
