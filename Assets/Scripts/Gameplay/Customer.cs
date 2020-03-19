using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Customer : MonoBehaviour
{
    public List<Attribute> attributes;
    public SpriteRenderer head;
    public SpriteRenderer weapon;
    public SpriteRenderer face;
    public SpriteRenderer body;

    public GameObject particleParent;
    
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator walkUp()
    {
        yield return new WaitForSeconds(.4f);
    }

    public IEnumerator walkAway()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("walkAway");
        yield return new WaitForSeconds(.4f);
    }

    public float karmaValue()
    {
        float value = 0;
        foreach(var attribute in attributes)
        {
            value += attribute.karmaValue;
        }
        return value;
    }

    public float goldValue()
    {
        float value = 0;
        foreach (var attribute in attributes)
        {
            value += attribute.goldValue;
        }
        return value;
    }

    public float staminaValue()
    {
        float value = 0;
        foreach (var attribute in attributes)
        {
            value += attribute.staminaValue;
        }
        return value;
    }

    internal string customerName()
    {
        return "Name";
    }

    internal string customerDesc()
    {
        //Reads like: A (adjective) (occupation) (personality) walks in. They place a (aura) (item) on the table
        return attributes[0].text + " " + attributes[1].text + " " + attributes[2].text + " They place a " + attributes[4].text + ", " + attributes[3].text + " on the table.";
    }
}

