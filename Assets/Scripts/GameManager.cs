using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject endUI;
    public GameObject winUI;
    

    public static GameManager Instance;
    private MonsterSpawner enemySpawner;
    void Awake()
    {
        Instance = this;
        enemySpawner = GetComponent<MonsterSpawner>();
    }

    public void Win()
    {
        winUI.SetActive(true);
    }
    public void Failed()
    {
        enemySpawner.Stop();
        endUI.SetActive(true);
    }

    public void OnButtonRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnButtonMenu()
    {
        SceneManager.LoadScene(0);
    }
}
