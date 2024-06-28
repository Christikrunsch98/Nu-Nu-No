using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class People : MonoBehaviour
{
    // People's state machine
    // Walking
    // Sitting
    // Working
    // Talking
    // Gesturing
    [Header("Office Spot")]
    [SerializeField] Transform currentDestination;
    [SerializeField] float tolerance = 0.1f;            // Toleranzwert für das erreichen des Destination Points

    [Header("Interaction")]
    [SerializeField] Transform DialoguePosition;
    [SerializeField] float interactionDistance = 2.75f;    // Distanz, ab der Interagiert werden kann
    [SerializeField] GameObject visualCue;
    [SerializeField] TextAsset inkJSON;
    [SerializeField] Sprite npcImage;

    [HideInInspector] public bool Interactable;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
        else
        {
            Interactable = false;
            visualCue.SetActive(false);
            //DialogueManager.Instance.ExitDialogueMode();
        }

        // OfficeSpot: Check if there is a Destination to walk to, if so move to that OfficeSpot Position
        if (currentDestination == null) return;
            agent.destination = currentDestination.position;        
    }

    public void SetCurrentDestination(string officeSpotName)
    {
        currentDestination = GameManager.Instance.GetOfficeSpotByName(officeSpotName).Position;
        CheckIfCurrentDestinationIsReached();
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

    public void StartDialogue()
    {
        DialogueManager.Instance.ReplaceNPCImage(npcImage);
        DialogueManager.Instance.EnterDialogueMode(inkJSON, DialoguePosition);
    }
}
