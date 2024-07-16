using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class GameState
{
    public GameStateEnum CurrentGameState;
    public OfficeSpot OnSpot;       // Name & Transform des Spots
    public OfficeSpot OffSpot;      // Name & Transform des Spots
    public OfficeSpot PatrolSpot;   // Spot benutzt im Off-State zum Laufen zwischen Off-Spot und diesem
    public TextAsset OninkJSON;     // Ink file ON : Dialogue
    public TextAsset OffinkJSON;    // Ink file OFF : Dialogue
}

public enum NPCState
{
    None = 0,
    On = 1,
    Off = 2,
    OffPatrol = 3,
    Talking = 4
}

public class People : MonoBehaviour
{
    // COMING SOON: People's animator state machine
    // Walking
    // Sitting
    // Working
    // Talking
    // Gesturing

    [Header("Office Spot")]
    [SerializeField] Transform currentDestination;
    [SerializeField] float tolerance = 0.1f;            // Toleranzwert für das erreichen des Destination Points
    [SerializeField] List<GameState> gameStates;        // Hält den On- & Off-Spot sowie Dialog für den aktuellen GameState

    [Header("Interaction")]
    public CurrentDialogueState NPCDialogueState;
    [SerializeField] Transform DialoguePosition;
    [SerializeField] float interactionDistance = 2.75f;    // Distanz, ab der Interagiert werden kann
    [SerializeField] GameObject visualCue;
    [SerializeField] TextAsset backupInkJSON;
    [SerializeField] Sprite npcImage;
    [SerializeField] string npcName;

    [HideInInspector] public bool Interactable;
    [HideInInspector] public bool? NPCOnState;  // nullable bool for backup json

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // GameState <=> DialogueState Check up | First dialogue is always OninkJSON
        if (gameStates != null && gameStates.Count > 0) NPCDialogueState = CurrentDialogueState.On;
        else NPCDialogueState = CurrentDialogueState.None;
    }

    // Update is called once per frame
    void Update()
    {
        // NPC: Check if player is near by - Show him that Interaction is now possible
        if (CheckedPlayerDistance())
        {
            Interactable = true;
            visualCue.SetActive(true);
        }
        else if (Interactable && !CheckedPlayerDistance())      // <------------- DIALOGUE CASE 1: BREAK OUT OF DIALOGUE
        {
            DialogueManager.Instance.ExitDialogueMode();
            Interactable = false;
            return;
        }
        else
        {
            Interactable = false;
            visualCue.SetActive(false);
            
        }

        // OfficeSpot: Check if there is a Destination to walk to, if so move to that OfficeSpot Position
        if (currentDestination == null) return;
        if (!CheckIfCurrentDestinationIsReached())
        {
            agent.destination = currentDestination.position;
            return;
        }
        else
        {
            currentDestination = null;
        }
                    
    }

    // Movement --------------
    public void SetCurrentDestination(string officeSpotName)
    {
        currentDestination = GameManager.Instance.GetOfficeSpotByName(officeSpotName).Position;        
    }

    public bool CheckIfCurrentDestinationIsReached()
    {
        if (Vector3.Distance(new Vector3(transform.position.x,0,transform.position.z), currentDestination.position) < tolerance)
            return true;            
        else return false;
    }

    // Distance Check for Interaction
    public bool CheckedPlayerDistance()
    {
        Transform player = GameManager.Instance.Player;
        if (player == null) return false;
        if (Vector3.Distance(player.position, transform.position) > interactionDistance) return false;
        else return true;
    }

    // Dialogue System --------------
    public void StartDialogue()
    {
        // Select the current Game state to pick up the right dialogue JSON file below
        GameState currentGameState = null;

        if (gameStates != null && gameStates.Count > 0)
        {
            Debug.Log("Current Game State as int: " + (int)GameManager.Instance.CurrentGameState);
            currentGameState = gameStates[(int)GameManager.Instance.CurrentGameState];
        }

        // Update NPCimage & name to match the right NPC 
        DialogueManager.Instance.ReplaceNPCImage(npcImage);
        DialogueManager.Instance.ReplaceNameText(npcName);
        
        // Choos the dialogue based on the current NPC State - ON meaning first contact and valuebale dialogue which effects the score values and game direction based on the player's decicions
        // OFF - meaning the dialogue after the ON dialogue was made that doesn't effect the game in any way further
        // DEFAULT - Fallback for special cases where a third dialogue might be needed, ON needs to be = null for this case
        switch (NPCDialogueState)
        {
            case CurrentDialogueState.On:  // ON
                DialogueManager.Instance.EnterDialogueMode(currentGameState.OninkJSON, DialoguePosition, this);
                break;
            case CurrentDialogueState.Off: // OFF
                DialogueManager.Instance.EnterDialogueMode(currentGameState.OffinkJSON, DialoguePosition, this);
                break;
            case CurrentDialogueState.None:
                if (backupInkJSON != null)
                    DialogueManager.Instance.EnterDialogueMode(backupInkJSON, DialoguePosition, this);
                break;
        }
    }

    public void SwitchDialogueState(CurrentDialogueState currentDialogueState)
    {
        NPCDialogueState = currentDialogueState;
        Debug.Log("NPC Dialogue State now is <" + currentDialogueState.ToString() + ">!");
    }
}
