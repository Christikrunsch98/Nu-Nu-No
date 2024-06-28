using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] GameObject dialogueWindow;
    [SerializeField] GameObject dialogueChoices;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] Image npcDialogueImage;            // NPC Image

    // Lerping Dialogue Height
    private Vector3 targetPositionA = new Vector3(0, 1.6f, 0);
    private Vector3 targetPositionB = new Vector3(0, 3.2f, 0);
    private float lerpTime = 0.3f;
    private float currentLerpTime = 0;
    private bool isLerping = false;
    private Vector3 startPosition;
    private Vector3 endPosition;

    private Story currentStory;
    
    public bool DialogueIsPlaying { get; private set; }     // TODO: Freeze Player Movement (Teleport & Left Controller Walk)

    [Header("Choices")]
    [SerializeField] GameObject[] choices;                  // List of UI choices: Choice0, Choice1, Choice2, ...
    private TextMeshProUGUI[] choicesText;                  // Text of button that is a child of each choice

    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in th scene");
        }
        Instance = this;
    }

    private void Start()
    {
        dialogueWindow.transform.localPosition = targetPositionA;
        DialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        // get all of the choices text
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    void Update()
    {
        if (isLerping)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
                isLerping = false;
            }

            float perc = currentLerpTime / lerpTime;
            dialogueWindow.transform.localPosition = Vector3.Lerp(startPosition, endPosition, perc);
        }
    }

    private void StartLerping(Vector3 targetPosition)
    {
        startPosition = dialogueWindow.transform.localPosition;
        endPosition = targetPosition;
        currentLerpTime = 0;
        isLerping = true;
    }

    public void EnterDialogueMode(TextAsset inkJSON, Transform dialoguePosition)
    {
        currentStory = new Story(inkJSON.text);
        DialogueIsPlaying = true;
        dialoguePanel.transform.position = dialoguePosition.position;
        dialoguePanel.SetActive(true);
        

        ContinueStory();
    }

    public void ExitDialogueMode()
    {
        DialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            // Set the text for the current dialogue line
            dialogueText.text = currentStory.Continue();
            // display choices, if any, for this dialogue line
            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices; // Choices given in the ink File

        if (currentChoices.Count <= 0)
        {
            if (dialogueWindow.transform.localPosition != Vector3.zero) StartLerping(targetPositionA);
            dialogueChoices.SetActive(false);
            return;
        }
        else
        {
            StartLerping(targetPositionB);
            dialogueChoices.SetActive(true);
        }

        // definsive check to make sure our UI can support the number of choices coming in
        if (currentChoices.Count > choices.Length) // Check ink file choices vs in Game Choices UI Elements
        {
            Debug.LogError("More choices were given then the UI can support. Number of choices given: "
                + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the coices up to the amount of choices for this line of dialogue
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.transform.parent.gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

    public void ReplaceNPCImage(Sprite npcImage)
    {
        npcDialogueImage.sprite = npcImage;
    }
}
