using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("수집 UI 텍스트")]
    public TMP_Text itemText;

    [Header("클리어 패널")]
    public GameObject clearPanel;

    [Header("버튼")]
    public Button restartButton;
    public Button quitButton;

    private void Start()
    {
        //시작 시 클리어 패널은 꺼두기
        if (clearPanel != null)
            clearPanel.SetActive(false);

        // 버튼 이벤트 연결
        if (restartButton != null)
            restartButton.onClick.AddListener(OnRestartClicked);

        if (quitButton != null)
            quitButton.onClick.AddListener(OnQuitClicked);
    }

    //수집/목표 UI 갱신
    public void UpdateItemUI(int current, int target)
    {
        if (itemText != null)
        {
            itemText.text = $"{current} / {target}";
        }
    }

    //목표 달성 시 클리어 패널 표시
    public void ShowClearPanel()
    {
        if (clearPanel != null)
            clearPanel.SetActive(true);
    }

    private void OnRestartClicked()
    {
        DataManager.Instance.RestartGame();
    }

    private void OnQuitClicked()
    {
        DataManager.Instance.QuitGame();
    }
}
