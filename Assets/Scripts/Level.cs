using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float loadDelay = 1.5f;

    public void LoadStartMenu() 
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
        
        GameSession gameSession = FindObjectOfType<GameSession>();
        if (gameSession)
        {
            gameSession.ResetGame();
        }
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoadGameOver());
    }

    public void QuiteGame()
    {
        Application.Quit();
    }

    private IEnumerator WaitAndLoadGameOver()
    {
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(2);
    }
}
