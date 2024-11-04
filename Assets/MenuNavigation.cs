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
    public Button difficultyBackButton;
    public Button weaponsBackButton;
    public Button scoreboardBackButton;
    public Button settingsBackButton;

    // Enum for identifying submenus
    public enum PanelType { Difficulty, Weapons, Scoreboard, Settings }

    void Start()
    {
        gameModeButton.onClick.AddListener(OpenDifficultyMenu);

        // Assign back button actions for each submenu
        difficultyBackButton.onClick.AddListener(() => BackToMainMenu(PanelType.Difficulty));
        weaponsBackButton.onClick.AddListener(() => BackToMainMenu(PanelType.Weapons));
        scoreboardBackButton.onClick.AddListener(() => BackToMainMenu(PanelType.Scoreboard));
        settingsBackButton.onClick.AddListener(() => BackToMainMenu(PanelType.Settings));
    }

    void OpenDifficultyMenu()
    {
        mainPanel.SetActive(false);
        difficultyPanel.SetActive(true);
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