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
    public Text changeText;

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
        curCustomer.attributes[0] = weightedRandom(adjectives);
        curCustomer.attributes[1] = weightedRandom(jobs);
        curCustomer.attributes[2] = weightedRandom(personalities);
        //randomize weapon/weapon description
        var selectedAtt = weightedRandom(auras);
        curCustomer.attributes[3] = selectedAtt;
        Instantiate(renderers[auras.IndexOf(selectedAtt)], curCustomer.particleParent.transform);

        var selectedImageAtt = weightedRandom(weapons);
        curCustomer.attributes[4] = selectedImageAtt;
        curCustomer.weapon.sprite = selectedImageAtt.Image;

        //appearance
        selectedImageAtt = weightedRandom(faces);
        curCustomer.attributes[5] = selectedImageAtt;
        curCustomer.face.sprite = selectedImageAtt.Image;

        selectedImageAtt = weightedRandom(heads);
        curCustomer.attributes[6] = selectedImageAtt;
        curCustomer.head.sprite = selectedImageAtt.Image;

        selectedImageAtt = weightedRandom(bodies);
        curCustomer.attributes[7] = selectedImageAtt;
        curCustomer.body.sprite = selectedImageAtt.Image;
    }

    private ImageAttribute weightedRandom(List<ImageAttribute> imageAttributes)
    {
        var attList = new List<Attribute>(imageAttributes.ToArray());
        var att = weightedRandom(attList);

        return att as ImageAttribute;
    }

    private Attribute weightedRandom(List<Attribute> attributes)
    {
        int totalWeight = 0;
        foreach(var att in attributes)
        {
            totalWeight += att.weight;
        }

        int selectedValue = UnityEngine.Random.Range(0, totalWeight);
        Attribute selectedAttribute = null;

        foreach(var att in attributes)
        {
            if (selectedAttribute == null && selectedValue < att.weight)
            {
                selectedAttribute = att;
            }
            selectedValue -= att.weight;
            att.weight++;
        }

        selectedAttribute.weight = 0;
        return selectedAttribute;
    }

    public void customerRejected()
    {
       var gold = curCustomer.goldValue();
        var karma= curCustomer.karmaValue();
        var stamina= curCustomer.staminaValue();
        var val = Math.Max(Math.Max(Math.Abs(gold), Math.Abs(stamina)), Math.Abs(karma));

        curKarma -= karma;
        curGold -= gold;
        curStamina -= stamina;

        if (val == gold)
        {
            changeText.text = "Biggest change: Gold";
        }
        else if (val == karma)
        {
            changeText.text = "Biggest change: Karma";
        }
        else if (val == stamina)
        {
            changeText.text = "Biggest change: Stamina";
        }
        else
        {
            changeText.text = "Nothing changed the most";
        }

        StartCoroutine(nextCustomer());
    }

    public void customerAccepted()
    {
        var gold = curCustomer.goldValue();
        var karma = curCustomer.karmaValue();
        var stamina = curCustomer.staminaValue();
        var val = Math.Max(Math.Max(Math.Abs(gold), Math.Abs(stamina)), Math.Abs(karma));

        curKarma += karma;
        curGold += gold;
        curStamina += stamina;

        if (val == gold)
        {
            changeText.text = "Biggest change: Gold";
        }
        else if (val == karma)
        {
            changeText.text = "Biggest change: Karma";
        }
        else if (val == stamina)
        {
            changeText.text = "Biggest change: Stamina";
        }
        else
        {
            changeText.text = "Nothing changed the most";
        }

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
