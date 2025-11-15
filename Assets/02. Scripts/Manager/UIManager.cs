using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI 참조")]
    public TMP_Text itemText;
    public GameObject clearPanel;    //Clear 시 활성화되는 패널

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //씬 바뀌면 항상 UI 최신화
        if (DataManager.Instance != null)
        {
            UpdateItemUI(
                DataManager.Instance.currentItemCount,
                DataManager.Instance.targetItemCount
            );
        }

        // 씬 로드 시 클리어 패널은 기본 비활성화
        if (clearPanel != null)
            clearPanel.SetActive(false);
    }

    // 아이템 텍스트 업데이트
    public void UpdateItemUI(int current, int target)
    {
        if (itemText != null)
            itemText.text = $"Item {current}/{target}";
    }

    //클리어 패널 표시
    public void ShowClearPanel()
    {
        if (clearPanel != null)
            clearPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
