using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public TextMeshProUGUI perdiste;
    public TextMeshProUGUI ganaste;
    public int TotalPoint { get; private set; }

    public HUD hud;

    public int Lives = 3;

    void Start()
    {
    }

    void Update()
    {

    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Cuidado! Mas de un GameManager en escena.");
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        perdiste.enabled = true;
        StartCoroutine(RestartGame());
    }
    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");

    }
    public void AddPoints(int puntosasumar)
    {
        TotalPoint += puntosasumar;
        hud.UpdatePoints(TotalPoint);
    }
    public void LoseLives()
    {
        Lives -= 1;
        if (Lives <= 0)
        {
            GameOver();
        }
        hud.LivesOff(Lives);
    }
    public bool AddLives()
    {
        if (Lives >= 3)
        {
            return false;
        }
        hud.Liveson(Lives);
        Lives += 1;
        return true;
    }

    public void Ganaste()
    {
        Debug.Log("Game Over");
        ganaste.enabled = true;
        StartCoroutine(RestartGame());
    }
}


