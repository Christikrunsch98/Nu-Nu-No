using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*[System.Serializable]
public class OfficeSpot
{
    public Transform Position;
    public bool Seat;           // sitzen ode nicht sitzen
    public float LookAngle;     // NPC Rotation while on that spot
}*/

public enum GameStateEnum
{
    State0,
    State1,
    State2,
    State3,
}

public class GameManager : MonoBehaviour
{
    public Transform Player;
    private Player player;
    public List<People> NPCs;
    public int Rage;

    public TextMeshProUGUI RagePopUpTMP;
    public TextMeshProUGUI InfoPopUpTMP;
    [SerializeField] Animator vrRageGlassesAnimator;
    [SerializeField] Animator vrInfoGlassesAnimator;

    public Transform OfficeStartSpot;

    public GameStateEnum CurrentGameState;

    private float activationTime = 10.0f;
    private float timer = 0.0f;


    // Die statische Instanz des GameManagers
    private static GameManager _instance;

    // Öffentliche statische Eigenschaft, um auf die Instanz zuzugreifen
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Falls keine Instanz vorhanden ist, wird eine neue erstellt
                GameObject gameManager = new GameObject("GameManager");
                _instance = gameManager.AddComponent<GameManager>();
                /*DontDestroyOnLoad(gameManager);*/
            }
            return _instance;
        }
    }

    // Die Awake-Methode stellt sicher, dass nur eine Instanz existiert
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            /*DontDestroyOnLoad(gameObject);*/
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        player = Player.gameObject.GetComponent<Player>();

        // Rage Score
        Rage = 0;

        foreach (var NPC in NPCs)
        {
            NPC.MoveToOnOfficeSpot(NPCMovementType.Teleport);
        }
    }


    public void AddRage(int add)
    {
        if ((Rage + add) > 10) Rage = 10;  // Clip to 10 max <-- Enter Ghost Room ???
        else if ((Rage + add) < 0) Rage = 0; // Clip to 0 min 
        else Rage += add;   // just add number

        // VR-Glasses PopUp's
        if(add > 0) RagePopUpTMP.text = "Wut um " + add + " erhöht.";
        else if (add < 0) RagePopUpTMP.text = "Wut um " + add + " gesenkt.";

        // Ghost Room PopUp
        if (Rage >= 5)  
        {
            InfoPopUpTMP.text = "Geisterraum jetzt verfügbar:\nDrücke B-Taste!";
            vrInfoGlassesAnimator.SetTrigger("PopUp");
            player.EnableBButton(true);
        }

        // Rage PopUp
        vrRageGlassesAnimator.SetTrigger("PopUp");

        // Display rage shader
        player.DisplayRageShader(Rage);   
    }

    public void ResetRage()
    {
        Rage = 0;
    }

    public void ContinueToNextGameState()
    {       
        // Change actual CurrentGameState
        switch (CurrentGameState)
        {
            case GameStateEnum.State0:
                CurrentGameState = GameStateEnum.State1;    // Switch from stage 1 to 2
                break;
            case GameStateEnum.State1:
                CurrentGameState = GameStateEnum.State2;    // Switch from stage 2 to 3
                break;
            case GameStateEnum.State2:
                CurrentGameState = GameStateEnum.State3;    // Switch from stage 3 to 4
                break;
            case GameStateEnum.State3:                      // Endgame
                break;
            default:
                break;
        }

        // Reset NPC Dialoge States
        foreach (var NPC in NPCs)
        {
            if (NPC.KeepDialogueState) continue;
            NPC.SwitchDialogueState(CurrentDialogueState.On);  // Set's On to true, meaning ONinkJSON will be used next
            NPC.MoveToOnOfficeSpot(NPCMovementType.Walking);                                                               
        }

        player.EnableBButton(true);
        InfoPopUpTMP.text = "Büro jetzt verfügbar:\nDrücke B-Taste!";
        vrInfoGlassesAnimator.SetTrigger("PopUp");
    }
}
