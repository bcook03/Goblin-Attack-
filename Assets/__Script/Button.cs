using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Button : MonoBehaviour
{
    void GameStart() {
        SceneManager.LoadScene(0);
    }

    void ReturnToMenu(){
        SceneManager.LoadScene(3);
    }

    void Quit()
    {
        
    }
}
