using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public bool introSeen;
    public int highScore;
    public List<string> keyNames;
    // Start is called before the first frame update
    void Start()
    {
        
        if (PlayerPrefs.GetInt(keyNames[(int)keys.gameOpened], 0) == 0)
        {
            ResetStats();
        }
        else
        {
            highScore= PlayerPrefs.GetInt(keyNames[(int)keys.highScore], 0);

            int test = PlayerPrefs.GetInt(keyNames[(int)keys.introSeen], 0);
            if (test == 0)
            {
                introSeen = false;
            }
            else
            {
                introSeen = true;
            }
        }
        if (SceneManager.sceneCount==1)
        {
            SceneManager.LoadScene("Startup", LoadSceneMode.Additive);
        }
    }

    public void ResetStats()
    {
        PlayerPrefs.SetInt(keyNames[(int)keys.gameOpened], 1);
        PlayerPrefs.SetInt(keyNames[(int)keys.introSeen], 0);
        PlayerPrefs.SetInt(keyNames[(int)keys.highScore], 0);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(keyNames[(int)keys.highScore], highScore);
        if (introSeen)
        {
            PlayerPrefs.SetInt(keyNames[(int)keys.introSeen], 1);
        }
        else
        {
            PlayerPrefs.SetInt(keyNames[(int)keys.introSeen], 0);
        }
        PlayerPrefs.Save();
    }

    //Use with the KeyNames list
    enum keys
    {
        gameOpened,
        highScore,
        introSeen
    }
}
