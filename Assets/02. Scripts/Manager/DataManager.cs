using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Playing,
    Clear
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    [Header("목표/현재 수집 개수")]
    public int targetItemCount = 0;
    public int currentItemCount = 0;

    [Header("참조")]
    public UIManager uiManager;

    public GameState gameState = GameState.Playing;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        if (uiManager != null)
            uiManager.UpdateItemUI(currentItemCount, targetItemCount);
    }

    public void AddItem(int amount)
    {
        if (gameState != GameState.Playing)
            return;

        currentItemCount += amount;

        if (uiManager != null)
            uiManager.UpdateItemUI(currentItemCount, targetItemCount);

        // 목표 달성 체크
        if (currentItemCount >= targetItemCount)
        {
            ClearGame();
        }

        Debug.Log("Collected: " + amount);
    }

    private void ClearGame()
    {
        gameState = GameState.Clear;

        if (uiManager != null)
            uiManager.ShowClearPanel();
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
