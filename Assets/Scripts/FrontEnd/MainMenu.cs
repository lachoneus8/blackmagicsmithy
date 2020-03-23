using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onPlay()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void onCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void onHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
