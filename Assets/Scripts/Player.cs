using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public InputActionAsset InputActions;

    private InputAction aButtonAction;

    void OnEnable()
    {
        // Hole die AButton Action aus dem Input Actions Asset
        aButtonAction = InputActions.FindActionMap("XRI RightHand").FindAction("AButton");

        // Registriere das Event für den A-Knopf
        aButtonAction.performed += OnAButtonPressed;
        aButtonAction.Enable();
    }

    void OnDisable()
    {
        // Deregistriere das Event für den A-Knopf
        aButtonAction.performed -= OnAButtonPressed;
        aButtonAction.Disable();
    }

    private void OnAButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log("A button pressed!");

        // Führe hier deine gewünschte Aktion aus
        People NPC = GetNearestNPC();
        // Aktion ausführen, wenn der A-Knopf gehalten wird
        if (NPC != null && !DialogueManager.Instance.DialogueIsPlaying)
        {
            NPC.StartDialogue();
        }
        else
        {
            Debug.LogWarning("NPC Not added or Dialog Window still open");
        }
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
