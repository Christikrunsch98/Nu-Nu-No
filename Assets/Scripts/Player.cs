using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public InputActionAsset InputActions;
    public Transform GhostRoomTransform;
    public Transform OfficeBaseTransform;

    private InputAction aButtonAction;
    private InputAction bButtonAction;

    void OnEnable()
    {
        // Get Button Action from Input Actions Asset
        aButtonAction = InputActions.FindActionMap("XRI RightHand").FindAction("AButton");
        bButtonAction = InputActions.FindActionMap("XRI RightHand").FindAction("BButton");

        // Register Event for Buttons
        aButtonAction.performed += OnAButtonPressed;
        aButtonAction.Enable();

        bButtonAction.performed += OnBButtonPressed;
        bButtonAction.Enable();
    }

    void OnDisable()
    {
        // De-Register Event for Buttons
        aButtonAction.performed -= OnAButtonPressed;
        aButtonAction.Disable();

        bButtonAction.performed -= OnBButtonPressed;
        bButtonAction.Disable();
    }

    private void OnAButtonPressed(InputAction.CallbackContext context)
    {
        // Get nearest NPC to start the correct conversation
        People NPC = GetNearestNPC();

        // Start Dialogue with nearest NPC
        if (NPC != null && !DialogueManager.Instance.DialogueIsPlaying)
        {
            NPC.StartDialogue();
        }
        else
        {
            Debug.LogWarning("NPC might not be added to the GameManager or Dialog Window might be still open!");
        }
    }

    private void OnBButtonPressed(InputAction.CallbackContext context)
    {
        // Teleport Player either to Ghost Room or Office
        if(Vector3.Distance(transform.position,GhostRoomTransform.position) > 2)  
            transform.position = GhostRoomTransform.position;
        else transform.position = OfficeBaseTransform.position;
    }

    public People GetNearestNPC()
    {
        People nearestNPC = null;
        float nearestDistance = 999f;
        // Check List
        foreach (var NPC in GameManager.Instance.NPCs)
        {
            if (!NPC.Interactable) continue;
            if (Vector3.Distance(transform.position,NPC.gameObject.transform.position) < nearestDistance)
            {
                nearestDistance = Vector3.Distance(transform.position, NPC.gameObject.transform.position);
                nearestNPC = NPC;
            }
                
        }
        return nearestNPC;
    }
}
