using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

public class DataReader : MonoBehaviour
{
    public float effectValue;

    private GameController gameController;

    // Use this for initialization
    void Start()
    {
        gameController = GetComponent<GameController>();

        readData();
    }

    private void readData()
    {
        updateAttributes("CharacterAdjectives", gameController.adjectives);
        updateAttributes("CharacterOccupation", gameController.jobs);
        updateAttributes("CharacterPersonality", gameController.personalities);
        updateAttributes("ObjectAura", gameController.auras);
        updateImageAttributes("ObjectType", gameController.weapons);
    }

    private void updateAttributes(string fileName, List<Attribute> attributeList)
    {
        var data = Resources.Load<TextAsset>(fileName);
        var lines = data.text.Split('\n');

        attributeList.Clear();

        foreach(var line in lines)
        {
            var items = line.Split(';');
            var attribute = new Attribute();

            if (items.Length >= 2)
            {
                attribute.text = items[0];

                parseEffect(attribute, items[1]);

                attributeList.Add(attribute);
            }
        }
    }

    private void updateImageAttributes(string fileName, List<ImageAttribute> attributeList)
    {
        var data = Resources.Load<TextAsset>(fileName);
        var lines = data.text.Split('\n');
        var idx = 0;

        foreach (var line in lines)
        {
            var items = line.Split(';');
            var attribute = attributeList[idx];

            if (items.Length >= 2)
            {
                attribute.text = items[0];

                parseEffect(attribute, items[1]);

                attributeList.Add(attribute);
            }

            ++idx;
        }
    }

    private void parseEffect(Attribute attribute, string effect)
    {
        effect.Trim();
        bool increase = effect.Contains("+");
        float adjEffectValue = effectValue * (increase ? 1f : -1f);

        if (effect.StartsWith("STAMINA"))
        {
            attribute.staminaValue = adjEffectValue;
        }
        else if (effect.StartsWith("KARMA"))
        {
            attribute.karmaValue = adjEffectValue;
        }
        else if (effect.StartsWith("MONEY"))
        {
            attribute.goldValue = adjEffectValue;
        }
    }
}
