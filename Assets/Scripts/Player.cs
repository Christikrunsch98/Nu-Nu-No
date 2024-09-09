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

    [Header("Ghost Room")]
    public Transform GhostRoomTransform;
    public Transform OfficeBaseTransform;

    [Header("Visual Effects")]
    public Volume PlayerVolumeEffects;

    private Bloom bloom;
    private float elapsedTime = 0f;
    private float initialThreshold = 0.9f;
    private float targetThreshold = 0f;
    private float initialIntensity = 0f;
    private float targetIntensity = 70f;
    private bool enableBButton;

    public ScriptableRendererFeature FullScreenHeartBeat;
    public Material HeartBeatMaterial;
    public GameObject HeartBeatSound;
    public float duration = 1.25f;

    private AudioSource heartBeatAudioSource;    
    private int voronoiIntensityID = Shader.PropertyToID("_VoronoiIntensity");
   

    private void Start()
    {
        // Initialize the Bloom values
        if (PlayerVolumeEffects.profile.TryGet<Bloom>(out bloom))
        {            
            bloom.threshold.value = initialThreshold;
            bloom.intensity.value = initialIntensity;
        }
        else Debug.LogError("Bloom effect not found in the Volume profile.");

        FullScreenHeartBeat.SetActive(false);
        heartBeatAudioSource = HeartBeatSound.GetComponent<AudioSource>();
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



    // ### INPUT HANDLING ### --- ### --- ### --- ### --- ###
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
        if(enableBButton)
            EnterGhostroom();
    }

    public void EnableBButton(bool on)
    {
        enableBButton = on;
    }

    void EnterGhostroom()
    {
        GameManager.Instance.ResetRage();

        // Disable this effect
        StartCoroutine(HeartBeatShader(0, 0));

        // Teleport to ghost room
        StartTeleportion(false);
    }



    // ### TELEPORTATION HANDLING ### --- ### --- ### --- ### --- ###
    public void StartTeleportion(bool gameStart) // Ausführen zum Game Start und zum Teleportieren von und zum Geisterraum
    {
        StartCoroutine(ChangeBloomEffect(gameStart));
    }

    private void TeleportPlayer(bool gameStart)
    {
        // On Main Menu -> Start Game teleport Player into the Office
        if (gameStart)   
        {
            DialogueManager.Instance.ExitDialogueMode();
            transform.position = GameManager.Instance.OfficeStartSpot.position;
            return;
        }

        // On Ingame Teleport -> To Ghostroom and back to office
        if (Vector3.Distance(transform.position, GhostRoomTransform.position) > 2)
        {
            DialogueManager.Instance.ExitDialogueMode();
            EnableBButton(false);
            transform.position = GhostRoomTransform.position;
            if (PlayerVolumeEffects.profile.TryGet<ChromaticAberration>(out ChromaticAberration chromaticAberration))
                chromaticAberration.intensity.Override(1f);
        }
        else 
        {
            DialogueManager.Instance.ExitDialogueMode();
            EnableBButton(false);
            transform.position = OfficeBaseTransform.position;            
            if (PlayerVolumeEffects.profile.TryGet<ChromaticAberration>(out ChromaticAberration chromaticAberration))
                chromaticAberration.intensity.Override(0f);
        }            
    }

    IEnumerator ChangeBloomEffect(bool gameStart)
    {
        // Change from initial to target values
        yield return ChangeBloom(initialThreshold, targetThreshold, initialIntensity, targetIntensity);

        TeleportPlayer(gameStart);        

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



    // ### Rage Display ### --- ### --- ### --- ### --- ###
    public void DisplayRageShader(int rage)
    { 
        // If rage is higher than that, activate visual and audio cue for player feedback to know rage
        if (rage > 4)
        {           
            // Map the rage from 0 - 10 to 0 - 2 to fit the Intensity Slider in the Shader
            float mappedRage = Mathf.Lerp(0f, 2f, (rage - 4f) / 6f);
            float mappedVolume = Mathf.Lerp(0f, 1f, (rage - 4f) / 6f);

            EnableOrDisableHeartBeat(true);
            StartCoroutine(HeartBeatShader(mappedRage, mappedVolume));
            return;
        }

        // If rage gets lower than that, switch off every effects etc.
        if (rage <= 4)
        {
            enableBButton = false;
            StartCoroutine(HeartBeatShader(0, 0));
        }
    }

    private void EnableOrDisableHeartBeat(bool on)
    {
        HeartBeatSound.SetActive(on);
        FullScreenHeartBeat.SetActive(on);
    }

    private IEnumerator HeartBeatShader(float targetIntensity, float targetVolume)
    {       
        // Der Startwert ist der aktuelle Wert
        float startIntensity = HeartBeatMaterial.GetFloat(voronoiIntensityID);
        float currentVoronoiIntensity;
        float startVolume = heartBeatAudioSource.volume;
        float currentVolume;

        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            currentVoronoiIntensity = Mathf.Lerp(startIntensity, targetIntensity, (elapsedTime / duration));
            HeartBeatMaterial.SetFloat(voronoiIntensityID,currentVoronoiIntensity);

            currentVolume = Mathf.Lerp(startVolume, targetVolume, (elapsedTime / duration));
            heartBeatAudioSource.volume = currentVolume;

            yield return null;
        }

        if (heartBeatAudioSource.volume <= 0f) EnableOrDisableHeartBeat(false);
    }



    // ### Functional NPC Methods ### --- ### --- ### --- ### --- ###

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
