using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

[System.Serializable]
public class DialogueVarbiables
{
    public Dictionary<string, Ink.Runtime.Object> InkVariables { get; private set;}    // string = variable name, Ink.Runtime.Object = variable value

    public DialogueVarbiables(TextAsset inkJSON)    // Constructor that stores all variables stored the globals ink file 
    {
        Story globalsVariablesStory = new Story(inkJSON.text); 
        InkVariables = new Dictionary<string, Ink.Runtime.Object>();

        foreach (var name in globalsVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalsVariablesStory.variablesState.GetVariableWithName(name);
            InkVariables.Add(name, value);
            Debug.Log("Initialized global dialogue variable: " + name + " = " + value);
        }
    }

    public void StartListening(Story story) // Listens to a change of an ink-Variable
    {
        LoadVariablesToStory(story);    // It's important that this method is called before the listener!
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story) // Stops listening to a changes of any ink-Variable
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        //Debug.Log("Varbiable changed: " + name + " = " + value);

        // Only maintain variables that were intialized from the globals ink file
        if (InkVariables.ContainsKey(name))
        {
            InkVariables.Remove(name);
            InkVariables.Add(name, value);
        }
    }

    private void LoadVariablesToStory(Story story)
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> inkVariable in InkVariables)
        {
            story.variablesState.SetGlobal(inkVariable.Key, inkVariable.Value);
        }
    }
}

public class InkExternalFunctions
{
    public void Bind(Story story, People NPC)
    {
        story.BindExternalFunction("SwitchDialogueState", (string currentDialogueState) =>
        {
            switch (currentDialogueState)
            {
                case "Off":
                    NPC.SwitchDialogueState(CurrentDialogueState.Off);
                    NPC.MoveToNextOfficeSpot();
                    break;
                case "On":
                    NPC.SwitchDialogueState(CurrentDialogueState.On);
                    break;
                default:
                    NPC.SwitchDialogueState(CurrentDialogueState.None);
                    break;
            }

        });

        story.BindExternalFunction("ContinueToNextGameState", (string gameState) =>
        {
            switch (gameState)
            {
                case "Next":
                    GameManager.Instance.ContinueToNextGameState();
                    Debug.Log("Current Game State = " + GameManager.Instance.CurrentGameState);
                    break;
                default:
                    Debug.LogWarning("GameState change was triggered, but not executed. See what parameter you gave the function in the ink file.");
                    break;
            }

        });
    }

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("SwitchDialogueState");
        story.UnbindExternalFunction("ContinueToNextGameState");
    }
}

public enum CurrentDialogueState
{
    On,
    Off,
    None
}

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue Variables")]
    DialogueVarbiables dialogueVarbiables;
    public TextAsset GlobalsInkJSON;

    [Header("Dialogue UI")]
    [SerializeField] Button continueButton;
    [SerializeField] GameObject dialogueWindow;
    [SerializeField] GameObject dialogueChoices;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Image npcDialogueImage;            // NPC Image
    ColorBlock referenceColors;     // saves the button colors
    ColorBlock grayColors;          // saves the grayed out colors

    // Lerping Dialogue Height
    private Vector3 targetPositionA = new Vector3(0, 1.6f, 0);  // Dialogue only
    private Vector3 targetPositionB = new Vector3(0, 3.2f, 0);  // Dialogue with given choices
    private float lerpTime = 0.3f;
    private float currentLerpTime = 0;
    private bool isLerping = false;
    private Vector3 startPosition;
    private Vector3 endPosition;

    private Story currentStory;
    private InkExternalFunctions inkExternalFunctions;
    
    public bool DialogueIsPlaying { get; private set; }     // TODO: Freeze Player Movement (Teleport & Left Controller Walk)

    [Header("Choices")]
    [SerializeField] GameObject[] choiceButtons;                  // List of UI-choice-GameObjects: Choice0, Choice1, Choice2
    private TextMeshProUGUI[] choicesText;                  // Text of button that is a child of each choice

    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in th scene");
        }
        Instance = this;

        dialogueVarbiables = new DialogueVarbiables(GlobalsInkJSON);    // Create Dialogue Variables Object
        inkExternalFunctions = new InkExternalFunctions();              // Exertnal Function for Ink used in Enter and Exit Dialogue Mode
    }

    private void Start()
    {
        dialogueWindow.transform.localPosition = targetPositionA;
        DialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        // get all of the choices text
        choicesText = new TextMeshProUGUI[choiceButtons.Length];
        int index = 0;
        foreach (GameObject choice in choiceButtons)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }

        // This is used to show continue button as enabled or diabled | see: DisableContinueButton(); and EnableContinueButton();
        referenceColors = continueButton.colors;
        grayColors.normalColor = Color.gray;
        grayColors.highlightedColor = Color.gray;
        grayColors.pressedColor = Color.gray;
        grayColors.selectedColor = Color.gray;
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

    public void EnterDialogueMode(TextAsset inkJSON, Transform dialoguePosition, People NPC)
    {
        currentStory = new Story(inkJSON.text);
        DialogueIsPlaying = true;
        dialoguePanel.transform.position = dialoguePosition.position;
        dialoguePanel.SetActive(true);

        dialogueVarbiables.StartListening(currentStory);
        inkExternalFunctions.Bind(currentStory, NPC);

        ContinueStory();
    }

    public void ExitDialogueMode()
    {
        dialogueVarbiables.StopListening(currentStory);
        inkExternalFunctions.Unbind(currentStory);

        DialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";        
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            string nextLine = currentStory.Continue();
            if(nextLine.Equals("") && !currentStory.canContinue)
                ExitDialogueMode();
            else
            {
                // Set the text for the current dialogue line
                dialogueText.text = nextLine;
                // display choices, if any, for this dialogue line
                DisplayChoices();
            }            
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices; // Choices given in the ink File

        // Adjust the height oft dialogue Window basted on choices being present or not
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

        DisableContinueButton();

        // Definsive check to make sure our UI can support the number of choices coming in
        if (currentChoices.Count > choiceButtons.Length) // Check ink file choices vs in Game Choices UI Elements
        {
            Debug.LogError("More choices were given then the UI can support. Number of choices given: "
                + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the coices up to the amount of choices for this line of dialogue
        foreach (Choice choice in currentChoices)
        {
            choiceButtons[index].gameObject.transform.parent.gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choiceButtons.Length; i++)
        {
            choiceButtons[i].gameObject.transform.parent.gameObject.SetActive(false);
        }        
    }

    public void DisableContinueButton()
    {
        // This coloring step isn't needed, because the button is grey anyways when interactable is set to false -> But I'm making the gray even darker with my grayColors variable
        continueButton.colors = grayColors; 
        continueButton.interactable = false;
    }

    public void EnableContinueButton()
    {
        continueButton.colors = referenceColors;    // Reset Button color
        continueButton.interactable = true;
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

    public void ReplaceNameText(string npcName)
    {
        if (npcName == null) return;
        if (npcName.Length == 0) Debug.LogWarning("NPC name not set.");
        nameText.text = npcName;
    }

    public Ink.Runtime.Object GetInkVariableState(string inkVariableName)
    {
        Ink.Runtime.Object inkVariableValue = null;
        dialogueVarbiables.InkVariables.TryGetValue(inkVariableName, out inkVariableValue);
        if(inkVariableValue == null)
        {
            Debug.LogWarning("Ink Variable was found to be null: " + inkVariableName);
        }
        return inkVariableValue;
    }
}
