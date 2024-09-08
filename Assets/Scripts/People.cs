using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class GameState
{
    public GameStateEnum CurrentGameState;
    public OfficeSpot OnSpot;      
    public OfficeSpot OffSpot;     
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

    [Header("Navigation")]
    [SerializeField] Transform currentDestination;
    [SerializeField] float tolerance = 0.1f;            // Toleranzwert für das erreichen des Destination Points
    [SerializeField] float offsetAngle = 5.0f;
    [SerializeField] float rotationSpeed = 2.0f;
    [SerializeField] float timerDuration = 5f;  // Dauer des Timers in Sekunden : für Reset der NPC-Rotation
    private float timeRemaining;                // Verbleibende Zeit

    private bool timerRunning = false; // Timer läuft
    [SerializeField] List<GameState> gameStates;        // Hält die On- & Off-Spots sowie -Dialoge für die jeweiligen GameState

    [Header("Interaction")]
    public CurrentDialogueState NPCDialogueState;
    [SerializeField] Transform DialoguePosition;
    [SerializeField] float interactionDistance = 2.75f;    // Distanz, ab der Interagiert werden kann
    [SerializeField] GameObject visualCue;
    [SerializeField] TextAsset backupInkJSON;
    [SerializeField] Sprite npcImage;
    public string NPCName;
    [HideInInspector] public int Rep = 10;                    // How the players reputation is on this NPC

    [HideInInspector] public bool Interactable;
    [Tooltip("Check to keep Dialogue State in every Game State")]
    public bool KeepDialogueState;

    private NavMeshAgent agent;
    private Animator animator;
    private Transform player;
    private Transform spotDefaultTarget;
    private bool resetNPCRotation;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.Player;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = true;
        animator = GetComponent<Animator>();

        // GameState <=> DialogueState Check up | First dialogue is always OninkJSON
        if (gameStates != null && gameStates.Count > 0) NPCDialogueState = CurrentDialogueState.On;
        else NPCDialogueState = CurrentDialogueState.None;
    }

    // Update is called once per frame
    void Update()
    {
        // NON INTERACTION - RESET NPC ROTATION
        RotateNPCOnReset(); // <- only works if ´ResetNPCRotation(timerDuration)´ is called!

        // INTERACTION
        // NPC: Check if player is near by - Show him that Interaction is now possible
        if (CheckedPlayerDistance())
        {
            if (timerRunning) StopTimer();

            Interactable = true;            
            visualCue.SetActive(true);
            FaceSlerpPlayer();
        }
        else if (Interactable && !CheckedPlayerDistance())      // <------------- PLAYER NOT IN RANGE: BREAK OUT OF DIALOGUE
        {
            //DialogueManager.Instance.ExitDialogueMode();
            Interactable = false;
            ResetNPCRotation(timerDuration);
            return;
        }
        else
        {
            Interactable = false;
            visualCue.SetActive(false);
            
        }

        // WALKING
        // OfficeSpot: Check if there is a Destination to walk to, if so move to that OfficeSpot Position
        if (currentDestination == null) return;
        if (!CheckIfCurrentDestinationIsReached())
        {
            agent.destination = currentDestination.position;
            return;
        }
        else
        {
            animator.SetBool("Walk?", false);   // Walk done
            ResetNPCRotation(0.5f);          // wenn angegeben drehe NPC nachdem er auf seinem Spot angekommen ist, in den richtigen Angle, damit er in keine komische Richtung guckt
            
            currentDestination = null;
        }
                    
    }

    /// <summary>
    /// Returns out currentGameState if it exists otherwhise null. 
    /// </summary>
    /// <param name="currentGameState"></param>
    /// <returns>True - if currentGameState != null. False - if currentGameState == null.</returns>
    private bool SelectCurrentGameState(out GameState currentGameState)
    {
        // Select the current Game state to pick up the right dialogue JSON file below
        currentGameState = null;

        // Error Warning (Just a Dev reminder)
        if (gameStates.Count > 0 && gameStates.Count <= (int)GetLastEnumValue<GameStateEnum>()) Debug.LogError("The NPC [" + NPCName + "] has faulty gameStates list. The list should possess out of as many elements as there are Game States.");
        
        if (gameStates != null && gameStates.Count > 0 && !(gameStates.Count <= (int)GetLastEnumValue<GameStateEnum>()))  
        currentGameState = gameStates[(int)GameManager.Instance.CurrentGameState];
        else return false;

        return true;
    }

    public static T GetLastEnumValue<T>() where T : Enum  // Used to prevent Error's caused by gameState list having the wrong amount of elements
    {
        Array values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(values.Length - 1);
    }

    // Movement ---------------------------------------
    public void SetCurrentDestination(Transform officeSpot)
    {
        animator.SetBool("Walk?", true);
        currentDestination = officeSpot;        
    }

    public bool CheckIfCurrentDestinationIsReached()
    {
        if (Vector3.Distance(new Vector3(transform.position.x,0,transform.position.z), currentDestination.position) < tolerance)
            return true;            
        else return false;
    }

    // Player Interaction & NPC Reaction ---------------
    // Distance Check for Interaction
    public bool CheckedPlayerDistance()
    {        
        if (player == null) return false;

        // Constrains:
        if(animator != null)
            if (animator.GetBool("Walk?") == true) return false;

        if (Vector3.Distance(player.position, transform.position) > interactionDistance) return false;
        else return true;
    }

    // Face Player during Interactable = true
    private void FaceSlerpPlayer()
    {
        //Debug.Log("[" + npcName + "] " + "OffsetAngle: " + Vector3.Angle(transform.forward, player.transform.forward));

        // If the offset angle is large enough, rotate the NPC towards the player
        if (Vector3.Angle(transform.forward, player.transform.forward) > offsetAngle)
        {
            // Calculate the rotation direction
            Vector3 targetDirection = player.transform.position - transform.position;
            targetDirection.y = 0.0f; // Ensure the NPC rotates on the horizontal plane

            // Rotate the NPC with the calculated rotation direction and rotation speed
            Quaternion newRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        }
    }

    // RESET NPC ROTATION METHODS vvv ------------------------------------

    public void StopTimer()
    {
        resetNPCRotation = false;
        timerRunning = false;
        Debug.Log("Timer abgebrochen!");
    }    

    // Start's the timer => Triggers the reset of the rotation
    private void ResetNPCRotation(float timerDuration)
    {
        // Start Timer so NPC will face its normal direction soon (timerDuration)
        if (GetOfficeSpotTarget() != null)
        {
            spotDefaultTarget = GetOfficeSpotTarget();
            // START TIMER
            if (!timerRunning)
            {
                timeRemaining = timerDuration;
                timerRunning = true;
            }
        }
    }

    // Return's the target where the NPC looks at normally when not interacting with the player
    private Transform GetOfficeSpotTarget()
    {
        Transform facingTarget;

        if (!SelectCurrentGameState(out GameState currentGameState)) return null;

        switch (NPCDialogueState)
        {
            case CurrentDialogueState.On:
                if (currentGameState.OnSpot == null) return null;
                facingTarget = currentGameState.OnSpot.FacingTarget;
                break;
            case CurrentDialogueState.Off:
                if (currentGameState.OffSpot == null) return null;
                facingTarget = currentGameState.OffSpot.FacingTarget;
                break;
            case CurrentDialogueState.None:
                return null;
            default:
                return null;
        }

        return facingTarget;
    }

    // Run's the timer in the Update-Methode and calls SlerpResetNPCRotation when time is up so the NPC actually gets rotated back were it started
    private void RotateNPCOnReset()
    {
        // Check if Timer has started
        if (timerRunning)
        {
            if (timeRemaining > 0) // If so count down
            {
                timeRemaining -= Time.deltaTime;
            }
            else // Timer finished
            {
                timeRemaining = 0;
                timerRunning = false;
                resetNPCRotation = true; // -> ResetNPCRotation
            }
        }

        // Reset NPC Rotation
        if (resetNPCRotation == true && !CheckedPlayerDistance())
        {
            SlerpResetNPCRotation(spotDefaultTarget);
            return;
        }
    }

    // Rotates the NPC (with Slerp)
    public void SlerpResetNPCRotation(Transform target)    
    {       

        Vector3 directionToTarget = target.position - transform.position;
        directionToTarget.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // Dialogue System --------------
    public void StartDialogue()
    {
        if (!SelectCurrentGameState(out GameState currentGameState) && NPCDialogueState != CurrentDialogueState.None)
            Debug.LogWarning("[Developer, People.cs] Select Dialogue Mode NONE or insert GameStates to the list.");

        // Update NPCimage & name to match the right NPC 
        DialogueManager.Instance.ReplaceNPCImage(npcImage);
        DialogueManager.Instance.ReplaceNameText(NPCName);
        
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

    public void MoveToNextOfficeSpot()
    {
        // Checks: Only move when a Game State in the List exists otherwise return
        if (!SelectCurrentGameState(out GameState currentGameState)) return;   

        // Set next Destination when it is set so NPC moves to the location, see Update() Method
        switch (NPCDialogueState)
        {
            case CurrentDialogueState.On:
                if (currentGameState.OnSpot == null) return;
                
                // If the OffSpot from the last GameState is equal to the OnSpot this GameState return, meaning don't set any destination in the first place
                if(currentGameState.CurrentGameState != GameStateEnum.State0)
                    if (gameStates[(int)currentGameState.CurrentGameState - 1].OffSpot == gameStates[(int)currentGameState.CurrentGameState].OnSpot) return;

                StopTimer();
                /*animator.SetBool("Walk?", true);*/
                SetCurrentDestination(currentGameState.OnSpot.transform); // Walking
                break;
            case CurrentDialogueState.Off:
                if (currentGameState.OffSpot == null) return;
                if (currentGameState.OffSpot == currentGameState.OnSpot) return;    // Don't set current destination in the first place if its the same as before

                StopTimer();
                /*animator.SetBool("Walk?", true);*/
                SetCurrentDestination(currentGameState.OffSpot.transform); // Walking
                break;
        }
    }
}
