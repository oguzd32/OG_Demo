using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public UnityEvent OnLevelStarted;

    [Header("GameObject References")] 
    [SerializeField] private GameObject startUI = default;
    [SerializeField] private GameObject inGameUI = default;
    [SerializeField] private GameObject winUI = default;
    [SerializeField] private GameObject failUI = default;

    [Header("Text References")] 
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI inGameLevelText;
    [SerializeField] private TextMeshProUGUI winGameLevelText;

    // private variables
    private int currentLevel = 0;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentLevel = PlayerPrefs.GetInt("Level", 0);
        levelText.text = inGameLevelText.text = $"LEVEL {currentLevel + 1}";
        winGameLevelText.text = $"LEVEL {currentLevel + 1} COMPLETED!";
    }

    #region Main Functions
    
    public void OnPlayerStartedLevel()
    {
        startUI.SetActive(false);
        inGameUI.SetActive(true);
        OnLevelStarted?.Invoke();
    }

    public void OnPlayerCompletedLevel()
    {
        inGameUI.SetActive(false);
        winUI.SetActive(true);
    }

    public void OnPlayerFailedLevel()
    {
        inGameUI.SetActive(false);
        failUI.SetActive(true);
    }
    
    #endregion
    
    #region Button Functions
    
    public void OnPlayButtonClicked()
    {
        OnPlayerStartedLevel();
    }

    public void OnRetryButtonClicked()
    {
        DOTween.KillAll();
        LevelLoader.Instance.RestartLevel();
    }

    public void OnNextButtonClicked()
    {
        DOTween.KillAll();
        LevelLoader.Instance.NextLevel();
    }
    
    #endregion
}
