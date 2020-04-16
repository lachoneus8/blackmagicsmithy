using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public PlayerStats stats;
    public GameObject panel;

    public Button howToPlayButton;
    public List<Button> buttons;
    public Text highScoreText;


    // Start is called before the first frame update
    void Start()
    {
        stats = (PlayerStats)FindObjectOfType(typeof(PlayerStats));
        updateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onPlay()
    {
        if (stats.introSeen)
        {
            SceneLoad("Gameplay");
        }
        else
        {
            SceneLoad("HowToPlay");
        }
    }
    public void showDelete(bool show)
    {
        panel.SetActive(show);
        foreach(var button in buttons)
        {
            button.enabled = !show;
        }
    }
    public void delete()
    {
        stats.ResetStats();
        stats.highScore = 0;
        stats.introSeen = false;
        showDelete(false);
        updateUI();
    }
    public void SceneLoad(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Startup");
    }
    public void OnExit()
    {
        Application.Quit();
    }
    private void updateUI()
    {
        highScoreText.text = "High Score: " + stats.highScore;
        if (stats.introSeen)
        {
            howToPlayButton.gameObject.SetActive(true);
        }
        else
        {
            howToPlayButton.gameObject.SetActive(false);
        }
    }
}
