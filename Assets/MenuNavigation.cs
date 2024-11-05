using UnityEngine;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject difficultyPanel;
    public GameObject weaponsPanel;
    public GameObject scoreboardPanel;
    public GameObject settingsPanel;

    public Button gameModeButton;
    public Button weaponsButton;
    public Button scoreboardButton;
    public Button settingsButton;

    public Button difficultyBackButton;
    public Button weaponsBackButton;
    public Button scoreboardBackButton;
    public Button settingsBackButton;

    // Enum for identifying submenus
    public enum PanelType { Difficulty, Weapons, Scoreboard, Settings }

    void Start()
    {
        gameModeButton.onClick.AddListener(() => OpenMenu(PanelType.Difficulty));
        weaponsButton.onClick.AddListener(() => OpenMenu(PanelType.Weapons));
        scoreboardButton.onClick.AddListener(() => OpenMenu(PanelType.Scoreboard));
        settingsButton.onClick.AddListener(() => OpenMenu(PanelType.Settings));
        // Assign back button actions for each submenu
        difficultyBackButton.onClick.AddListener(() => BackToMainMenu(PanelType.Difficulty));
        weaponsBackButton.onClick.AddListener(() => BackToMainMenu(PanelType.Weapons));
        scoreboardBackButton.onClick.AddListener(() => BackToMainMenu(PanelType.Scoreboard));
        settingsBackButton.onClick.AddListener(() => BackToMainMenu(PanelType.Settings));
    }

    void OpenMenu(PanelType panel)
    {
        mainPanel.SetActive(false);
        switch (panel) {
            case PanelType.Difficulty:
                difficultyPanel.SetActive(true);
                break;
            case PanelType.Weapons:
                weaponsPanel.SetActive(true);
                break;
            case PanelType.Scoreboard:
                scoreboardPanel.SetActive(true);
                break;
            case PanelType.Settings:
                settingsPanel.SetActive(true);
                break;
        }
    }

    void BackToMainMenu(PanelType panel)
    {
        switch (panel)
        {
            case PanelType.Difficulty:
                difficultyPanel.SetActive(false);
                break;
            case PanelType.Weapons:
                weaponsPanel.SetActive(false);
                break;
            case PanelType.Scoreboard:
                scoreboardPanel.SetActive(false);
                break;
            case PanelType.Settings:
                settingsPanel.SetActive(false);
                break;
        }
        mainPanel.SetActive(true); // Always show main panel after hiding a submenu
    }
}