using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public string curScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onExit(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(curScene));
    }
}
