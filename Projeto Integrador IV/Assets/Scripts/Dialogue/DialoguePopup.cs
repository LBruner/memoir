using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialoguePopup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speakerNameText;
    [SerializeField] TextMeshProUGUI dialogueText;

    public void SetUpPopup(string speakerText, string dialogue, Color color)
    {
        speakerNameText.text = speakerText;
        dialogueText.color = color;
        dialogueText.text = dialogue;
    }
}
