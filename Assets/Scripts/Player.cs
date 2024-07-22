using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Input")]
    public InputActionAsset InputActions;
    private InputAction aButtonAction;
    private InputAction bButtonAction;

    [Header("Geisterraum")]
    public Transform GhostRoomTransform;
    public Transform OfficeBaseTransform;
    public Volume PlayerVolumeEffects;
    public float duration = 1.25f;

    private Bloom bloom;
    private float elapsedTime = 0f;
    private float initialThreshold = 0.9f;
    private float targetThreshold = 0f;
    private float initialIntensity = 0f;
    private float targetIntensity = 70f;

    private void Start()
    {
        // Initialize the Bloom values
        if (PlayerVolumeEffects.profile.TryGet<Bloom>(out bloom))
        {            
            bloom.threshold.value = initialThreshold;
            bloom.intensity.value = initialIntensity;
        }
        else Debug.LogError("Bloom effect not found in the Volume profile.");

    }

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
        ModifyVolumeProfile();
    }

    private void TeleportPlayer()
    {
        if (Vector3.Distance(transform.position, GhostRoomTransform.position) > 2)
        {
            transform.position = GhostRoomTransform.position;
            if (PlayerVolumeEffects.profile.TryGet<ChromaticAberration>(out ChromaticAberration chromaticAberration))
                chromaticAberration.intensity.Override(1f);
        }
        else 
        {
            transform.position = OfficeBaseTransform.position;
            if (PlayerVolumeEffects.profile.TryGet<ChromaticAberration>(out ChromaticAberration chromaticAberration))
                chromaticAberration.intensity.Override(0f);
        }            
    }

    public void ModifyVolumeProfile()
    {
        StartCoroutine(ChangeBloomEffect());
    }

    IEnumerator ChangeBloomEffect()
    {
        // Change from initial to target values
        yield return ChangeBloom(initialThreshold, targetThreshold, initialIntensity, targetIntensity);

        TeleportPlayer();        

        // Change from target values back to initial values
        yield return ChangeBloom(targetThreshold, initialThreshold, targetIntensity, initialIntensity);
    }

    IEnumerator ChangeBloom(float startThreshold, float endThreshold, float startIntensity, float endIntensity)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Lerp the threshold and intensity values
            bloom.threshold.value = Mathf.Lerp(startThreshold, endThreshold, t);
            bloom.intensity.value = Mathf.Lerp(startIntensity, endIntensity, t);

            yield return null;
        }

        // Ensure the final values are set
        bloom.threshold.value = endThreshold;
        bloom.intensity.value = endIntensity;
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
