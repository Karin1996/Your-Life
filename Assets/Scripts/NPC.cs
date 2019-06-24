using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC
{
    private string name;
    private string dialogueText;
    private Tuple<string, string, int> [] options;

    public NPC(string name, string dialogueText, Tuple<string, string, int> optionOne, Tuple<string, string, int> optionTwo, Tuple<string, string, int> optionThree)
    {
        this.name = name;
        this.dialogueText = dialogueText;
        this.options = new Tuple<string, string, int>[] { optionOne, optionTwo, optionThree };
    }

    public string getName()
    {
        return name;
    }
    
    public string getDialogueText()
    {
        return dialogueText;
    }

    private bool isValidIndex(int index)
    {
        return !(index < 0 || index > 2);
    }

    public string getOption(int index)
    {
        if (!isValidIndex(index))
        {
            throw new System.ArgumentOutOfRangeException("This option does not exist");
        }

        return options[index].Item1;
    }

    public Tuple<string, int> selectOption(int index)
    {
        if (!isValidIndex(index))
        {
            throw new System.ArgumentOutOfRangeException("This option does not exist");
        }

        return Tuple.Create(options[index].Item2, options[index].Item3);
    }
}
