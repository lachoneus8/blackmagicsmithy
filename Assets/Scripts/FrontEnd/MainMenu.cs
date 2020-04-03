using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public PlayerStats stats;
    public Button howToPlayButton;
    // Start is called before the first frame update
    void Start()
    {
        stats = (PlayerStats)FindObjectOfType(typeof(PlayerStats));
        if (stats.introSeen)
        {
            howToPlayButton.gameObject.SetActive(true);
        }
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
    public void SceneLoad(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Startup");
    }
    public void OnExit()
    {
        Application.Quit();
    }
    
}
