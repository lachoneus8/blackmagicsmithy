using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{
    public float curKarma = 50f;
    public float curGold = 50f;
    public float curStamina = 50f;
    public int score;

    public Customer curCustomer;
    public GameObject customerPrefab;
    
    public UiHandler ui;
    public Text deathText;
    public Text WinText;
    public Text scoreText;

    public List<Attribute> adjectives;
    public List<Attribute> jobs;
    public List<Attribute> personalities;

    public List<ImageAttribute> faces;
    public List<ImageAttribute> heads;
    public List<ImageAttribute> bodies; 

    public List<ImageAttribute> weapons;
    public List<Attribute> auras;
    public List<GameObject> renderers;

    public List<string> tooRich;
    public List<string> tooPoor;
    public List<string> tooEvil;
    public List<string> tooGood;
    public List<string> tooStressed;
    public List<string> tooRelaxed;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(nextCustomer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void randomizeCustomer()
    {

        var customerObject = Instantiate(customerPrefab, transform);

        curCustomer = customerObject.GetComponent<Customer>();
        //randomize description
        curCustomer.attributes[0] = adjectives[UnityEngine.Random.Range(0, adjectives.Count)];
        curCustomer.attributes[1] = jobs[UnityEngine.Random.Range(0, jobs.Count)];
        curCustomer.attributes[2] = personalities[UnityEngine.Random.Range(0, personalities.Count)];
        //randomize weapon/weapon description
        int i = UnityEngine.Random.Range(0, auras.Count);
        curCustomer.attributes[3] = auras[i];
        Instantiate(renderers[i], curCustomer.particleParent.transform);

        i = UnityEngine.Random.Range(0, weapons.Count);
        curCustomer.attributes[4] = weapons[i];
        curCustomer.weapon.sprite = weapons[i].Image;

        //appearance
        i = UnityEngine.Random.Range(0, faces.Count);
        curCustomer.attributes[5] = faces[i];
        curCustomer.face.sprite = faces[i].Image;

        i = UnityEngine.Random.Range(0, heads.Count);
        curCustomer.attributes[6] = heads[i];
        curCustomer.head.sprite = heads[i].Image;

        i = UnityEngine.Random.Range(0, bodies.Count);
        curCustomer.attributes[7] = bodies[i];
        curCustomer.body.sprite = bodies[i].Image;
    }

    public void customerRejected()
    {
        curKarma -= curCustomer.karmaValue(false);
        curGold -= curCustomer.goldValue(false);
        curStamina -= curCustomer.staminaValue(false);

        StartCoroutine(nextCustomer());
    }

    public void customerAccepted()
    {
        curKarma += curCustomer.karmaValue(true);
        curGold += curCustomer.goldValue(true);
        curStamina += curCustomer.staminaValue(true);

        StartCoroutine(nextCustomer());
    }

    public IEnumerator nextCustomer()
    {
        //gold deaths
        if (curGold >= 100)
        {
            death(tooRich);
        }
        else if (curGold <= 0)
        {
            death(tooPoor);
        }
        //karma deaths
        else if (curKarma >= 100)
        {
            death(tooGood);
        }
        else if (curKarma <= 0)
        {
            death(tooEvil);
        }
        //stamina deaths
        else if (curStamina >= 100)
        {
            death(tooRelaxed);
        }
        else if (curStamina <= 0)
        {
            death(tooStressed);
        }
        else
        {
            score++;
            scoreText.text = "" + score;
            //winning
            if (score >= 30)
            {
                ui.setButtonsEnabled(false);
                WinText.transform.parent.gameObject.SetActive(true);
                WinText.text = "You won!";
            }
            // what to do if you survived but didn't win
            else
            {
                ui.setButtonsEnabled(false);


                if (curCustomer != null)
                {
                    yield return curCustomer.walkAway();
                    Destroy(curCustomer.gameObject);
                }

                yield return new WaitForSeconds(.2f);

                randomizeCustomer();

                yield return curCustomer.walkUp();

                ui.setButtonsEnabled(true);
            }

        }
    }
    private void death(List<string> messages)
    {
        ui.setButtonsEnabled(false);
        deathText.transform.parent.gameObject.SetActive(true);
        deathText.text = messages[UnityEngine.Random.Range(0, messages.Count)];
    }
}
