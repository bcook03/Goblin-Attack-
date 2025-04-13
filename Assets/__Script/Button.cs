using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Button : MonoBehaviour
{
    public void GameStart() {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMenu(){
        SceneManager.LoadScene(3);
    }

    public void Quit()
    {
        
    }
}
