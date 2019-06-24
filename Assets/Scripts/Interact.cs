using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityStandardAssets._2D;

public class Interact : MonoBehaviour
{
    public GameManager gm;
    private bool isTriggered = false;
    public Canvas dialogue;
    private NPC currentNPC = null;
    private Platformer2DUserControl movScript;

    // Update is called once per frame
    void Update()
    {
        if (isTriggered)
        {
            //Freeze all player animations and disable movement script until performAction() is done
            GetComponent<Animator>().enabled = false;
            movScript = GetComponent<Platformer2DUserControl>();
            movScript.enabled = false;

            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                performAction(0);
            } else if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                performAction(1);

            } else if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                performAction(2);
            }
        }
    }

    void performAction(int index)
    {
        if (currentNPC == null) { return; }

        //See what button is pressed, so which option is 
        Tuple<string, int> choice = currentNPC.selectOption(index);
        //Update a stat with a specific value
        gm.playerStats.updateStat(choice.Item1, choice.Item2);


        isTriggered = false;

        GetComponent<Animator>().enabled = true;

        //Change from sprites and animator when met a certain NPC
        if (currentNPC.getName().ToLower() == "teacher")
        {
            gm.ChangeCharacter(gm.child, gm.adult, "Animations/PlayerAnimController");
        }

        if (currentNPC.getName().ToLower() == "sira")
        {
            gm.ChangeCharacter(gm.adult, gm.old, "Animations/OldAnimController");
        }

        if (currentNPC.getName().ToLower() == "grim reaper")
        {
            //Check stats and show the end screen player got.
            string highest = highestStatValueKey();

            switch (highest)
            {
                case "knowledge":
                    gm.loadThisScene("Knowledge");
                    break;
                case "friendship":
                    gm.loadThisScene("Friendship");
                    break;
                case "ambition":
                    gm.loadThisScene("Ambition");
                    break;
                case "independence":
                    gm.loadThisScene("Independence");
                    break;
            }
        }

        gm.collect.Play();
        movScript.enabled = true;
        gm.DisableCanvas(gm.dialogue);
        gm.updateStatBar();
    }

    string highestStatValueKey()
    {
        int max_val = 0;
        string max_key = "";
        foreach(string key in gm.playerStats.stats.Keys)
        {
            int value = gm.playerStats.getStat(key);
            if (value > max_val)
            {
                max_val = value;
                max_key = key;
            }
        }
        return max_key;
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        isTriggered = true;
        // Fetch the NPC associated with the tag
        NPC theNPC = gm.getNPCByTag(trig.gameObject.tag);
        // Set the dialogue name
        dialogue.transform.Find("name").GetComponent<TextMeshProUGUI>().text = theNPC.getName();
        // Set the dialogue text
        dialogue.transform.Find("dialogue").GetComponent<TextMeshProUGUI>().text = theNPC.getDialogueText();
        // Set the options
        for (int i = 0; i < 3; i++)
        {
            dialogue.transform.Find("option" + (i+1).ToString()).GetComponent<TextMeshProUGUI>().text = theNPC.getOption(i);
        }

        currentNPC = theNPC;

        //show the canvas with the updated information
        gm.EnableCanvas(gm.dialogue);
        //delete the collision component. So that it can't be triggered more than once
        Destroy(trig);
    }
}
