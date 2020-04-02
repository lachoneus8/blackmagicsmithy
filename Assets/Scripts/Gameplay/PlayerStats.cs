using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public bool introSeen;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.sceneCount==1)
        {
            SceneManager.LoadScene("Startup", LoadSceneMode.Additive);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
