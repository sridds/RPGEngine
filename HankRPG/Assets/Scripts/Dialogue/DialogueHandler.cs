using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueHandler : MonoBehaviour
{
    [SerializeField]
    private float _typewriterSpeed = 0.1f;

    [SerializeField]
    private TextMeshProUGUI _textLabel;

    private readonly List<Punctuation> punctuations = new List<Punctuation>()
    {
        new Punctuation(new HashSet<char>() {'.', '!', '?'}, 0.6f),
        new Punctuation(new HashSet<char>() {',', ';', ':'}, 0.3f)
    };

    private Queue<DialogueObject> dialogueQueue = new Queue<DialogueObject>();
    private DialogueObject currentDialogueObject;
    private Coroutine activeDialogueCoroutine;

    private string currentDialogueLine;
    private int currentIndex;
    private bool continueFlag;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) Continue();
        if (Input.GetKeyDown(KeyCode.X)) Skip();
        
        HandleDialogueObject();
    }

    private void HandleDialogueObject()
    {
        if (!continueFlag) return;

        // Dequeue if currently null
        if (currentDialogueObject == null && dialogueQueue.Count > 0) currentDialogueObject = dialogueQueue.Dequeue();

        // Continue getting next lines
        if (currentIndex >= currentDialogueObject.Dialogue.Length - 1) {
            activeDialogueCoroutine = StartCoroutine(HandleDialogue(currentDialogueObject.Dialogue[currentIndex]));
        }
        // End the loop by setting to null reference
        else {
            currentIndex = 0;
            currentDialogueObject = null;
        }
    }

    /// <summary>
    /// Handles a single string of dialogue
    /// </summary>
    /// <param name="dialogue"></param>
    /// <returns></returns>
    private IEnumerator HandleDialogue(string dialogue)
    {
        currentDialogueLine = dialogue;
        float time = 0;
        int charIndex = 0;

        for(int i = 0; i < dialogue.Length; i++)
        {
            bool isLast = i > dialogue.Length - 1;

            _textLabel.text += dialogue[i];

            // Detect special character and wait if necessary. This will not execute if we are on the last character of the string
            if (IsPunctuation(dialogue[i], out float waitTime) && !isLast) {
                yield return new WaitForSeconds(waitTime);
            }
            else {
                yield return new WaitForSeconds(_typewriterSpeed);
            }
        }

        activeDialogueCoroutine = null;
    }

    /// <summary>
    /// Sets the continue flag for the next line to be queued
    /// </summary>
    private void Continue()
    {
        if (activeDialogueCoroutine != null) return;

        continueFlag = true;
    }

    /// <summary>
    /// Speeds up the current line to the end of the line
    /// </summary>
    private void Skip()
    {
        // Ensure the dialogue is currently running to avoid null reference errors
        if (activeDialogueCoroutine == null) return;
        StopCoroutine(activeDialogueCoroutine);

        _textLabel.text = currentDialogueLine;
    }

    public void QueueDialogue(DialogueObject dialogue)
    {
        Debug.Log("erm wat da sigma");
        dialogueQueue.Enqueue(dialogue);
        Continue();
    }

    private bool IsPunctuation(char character, out float waitTime)
    {
        foreach(Punctuation punctuationCategory in punctuations)
        {
            if (punctuationCategory.Punctuations.Contains(character)){
                waitTime = punctuationCategory.WaitTime;
                return true;
            }
        }

        waitTime = default;
        return false;
    }

    private readonly struct Punctuation
    {
        public readonly HashSet<char> Punctuations;
        public readonly float WaitTime;

        public Punctuation(HashSet<char> punctuations, float waitTime)
        {
            Punctuations = punctuations;
            WaitTime = waitTime;
        }
    }
}


