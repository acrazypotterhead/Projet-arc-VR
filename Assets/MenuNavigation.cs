using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
    public Button startButton;

    public Button difficultyBackButton;
    public Button weaponsBackButton;
    public Button scoreboardBackButton;
    public Button settingsBackButton;

    public Button trainingButton;
    public Button easyButton;
    public Button normalButton;
    public Button hardButton;
    public Button[] bowButtons;
    public Button[] arrowButtons;

    private Button selectedDifficultyButton;
    private Button selectedBowButton;
    private Button selectedArrowButton;

    public TMP_InputField pseudoInputField;

    public enum PanelType { Difficulty, Weapons, Scoreboard, Settings }

    void Start()
    {
        gameModeButton.onClick.AddListener(() => OpenMenu(PanelType.Difficulty));
        weaponsButton.onClick.AddListener(() => OpenMenu(PanelType.Weapons));
        scoreboardButton.onClick.AddListener(() => OpenMenu(PanelType.Scoreboard));
        settingsButton.onClick.AddListener(() => OpenMenu(PanelType.Settings));
        startButton.onClick.AddListener(StartGame);

        difficultyBackButton.onClick.AddListener(() => BackToMainMenu(PanelType.Difficulty));
        weaponsBackButton.onClick.AddListener(() => BackToMainMenu(PanelType.Weapons));
        scoreboardBackButton.onClick.AddListener(() => BackToMainMenu(PanelType.Scoreboard));
        settingsBackButton.onClick.AddListener(() => BackToMainMenu(PanelType.Settings));

        trainingButton.onClick.AddListener(() => SelectDifficulty(trainingButton, "Training"));
        easyButton.onClick.AddListener(() => SelectDifficulty(easyButton, "Easy"));
        normalButton.onClick.AddListener(() => SelectDifficulty(normalButton, "Normal"));
        hardButton.onClick.AddListener(() => SelectDifficulty(hardButton, "Hard"));

        foreach (Button bowButton in bowButtons)
            bowButton.onClick.AddListener(() => SelectBow(bowButton));

        foreach (Button arrowButton in arrowButtons)
            arrowButton.onClick.AddListener(() => SelectArrow(arrowButton));



        pseudoInputField.onValueChanged.AddListener(UpdatePseudo);


        // default values : 

        SelectDifficulty(trainingButton, "Training");
        SelectBow(bowButtons[0]);
        SelectArrow(arrowButtons[0]);
    }

    void OpenMenu(PanelType panel)
    {
        mainPanel.SetActive(false);
        switch (panel)
        {
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
        mainPanel.SetActive(true);
    }

    void SelectDifficulty(Button difficultyButton, string difficultyName)
    {
        if (selectedDifficultyButton != null)
            selectedDifficultyButton.GetComponent<Image>().color = Color.white;

        selectedDifficultyButton = difficultyButton;
        selectedDifficultyButton.GetComponent<Image>().color = Color.green;
        GameSessionManager.SelectedDifficulty = difficultyName;
    }

    void SelectBow(Button bowButton)
    {
        if (selectedBowButton != null)
            selectedBowButton.GetComponent<Image>().color = Color.white;

        selectedBowButton = bowButton;
        selectedBowButton.GetComponent<Image>().color = Color.green;
        GameSessionManager.SelectedBow = bowButton.name;
    }

    void SelectArrow(Button arrowButton)
    {
        if (selectedArrowButton != null)
            selectedArrowButton.GetComponent<Image>().color = Color.white;

        selectedArrowButton = arrowButton;
        selectedArrowButton.GetComponent<Image>().color = Color.green;
        GameSessionManager.SelectedArrow = arrowButton.name;
    }

    void StartGame()
    {
        GameSessionManager.LoadScene();
    }


    void UpdatePseudo(string pseudo)
    {
        GameSessionManager.Pseudo = pseudo;
    }

}