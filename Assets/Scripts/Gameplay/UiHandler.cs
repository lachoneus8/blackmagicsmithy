using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour
{
    public Text scoreText;
    public Text customerNameText;
    public Text customerDescText;

    public MeterBehaviour staminaMeter;
    public MeterBehaviour goldMeter;
    public MeterBehaviour karmaMeter;

    public GameController gameController;

    public Button repairButton;
    public Button rejectButton;

    // Start is called before the first frame update
    void Start()
    {
        var canvas = GetComponent<Canvas>();
        staminaMeter.Init(canvas.scaleFactor);
        goldMeter.Init(canvas.scaleFactor);
        karmaMeter.Init(canvas.scaleFactor);

        staminaMeter.SetValue(50);
        goldMeter.SetValue(50);
        karmaMeter.SetValue(50);
    }

    // Update is called once per frame
    void Update()
    {
        staminaMeter.SetValue(gameController.curStamina);
        goldMeter.SetValue(gameController.curGold);
        karmaMeter.SetValue(gameController.curKarma);

        if (gameController.curCustomer != null)
        {
            customerNameText.text = gameController.curCustomer.customerName();
            customerDescText.text = gameController.curCustomer.customerDesc();
        }
    }

    public void onExit()
    {
        SceneManager.LoadScene("Startup",LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Gameplay");
    }

    public void setButtonsEnabled(bool enabled)
    {
        repairButton.interactable = enabled;
        rejectButton.interactable = enabled;
    }

    public void onRepair()
    {
        gameController.customerAccepted();
    }

    public void onReject()
    {
        gameController.customerRejected();
    }
}
