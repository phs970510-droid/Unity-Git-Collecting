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
    public int targetItemCount;
    public int currentItemCount;

    [Header("참조")]
    public UIManager uiManager;

    public GameState gameState = GameState.Playing;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (uiManager == null)
            uiManager = FindObjectOfType<UIManager>();
    }

    public void AddItem(int amount)
    {
        currentItemCount += amount;
        Debug.Log("AddItem 실행됨: " + currentItemCount);

        if (uiManager != null)
            uiManager.UpdateItemUI(currentItemCount, targetItemCount);

        if (currentItemCount >= targetItemCount)
            ClearGame();
    }

    private void ClearGame()
    {
        gameState = GameState.Clear;

        if (uiManager != null)
            uiManager.ShowClearPanel();

        Debug.Log("CLEAR GAME CALLED");
    }

    public void RestartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
