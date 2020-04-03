using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    public Button game;
    public Button menu;
    PlayerStats stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = (PlayerStats)FindObjectOfType(typeof(PlayerStats));
        if (stats.introSeen)
        {
            menu.gameObject.SetActive(true);
            game.gameObject.SetActive(false);
        }
        stats.introSeen = true;
    }

}
