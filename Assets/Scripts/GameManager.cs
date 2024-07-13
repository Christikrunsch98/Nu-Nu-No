using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OfficeSpot
{
    public string Name;
    public Transform Position;    
}

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
    public List<People> NPCs;

    public List<OfficeSpot> OfficeSpots;

    public GameStateEnum CurrentGameState;

    // Die statische Instanz des GameManagers
    private static GameManager _instance;

    // �ffentliche statische Eigenschaft, um auf die Instanz zuzugreifen
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Falls keine Instanz vorhanden ist, wird eine neue erstellt
                GameObject gameManager = new GameObject("GameManager");
                _instance = gameManager.AddComponent<GameManager>();
                DontDestroyOnLoad(gameManager);
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
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    /// Game State Vorgaben ///
    /// A - Der Anfang
    /// N�rgler.OfficeSpot -> K�che_Kaffee
    /// N�rgler.Reaktion -> N�rgler.OfficeSpot -> K�che_Snackautomat
    /// -
    /// B - Teil 1
    /// �tzender Kollege.OfficeSpot -> Arbeitsplatz.�tzenderKollege
    /// Spieler Wegpunkt -> Arbeitsplatz.Spieler
    /// Andere auf ihren Pl�tzen oder irgendwo
    /// Meetingraum geschlossen und niemand zu sehen weiter
    /// -
    /// C - Teil 2
    /// Alle im Meetingraum.Sitz1,.Sitz2,.Sitz3,...
    /// Spieler Wegpunkt -> Meetingraum.Sitz6
    /// Nach dem Verlassen Teamleiter.NebenSpieler
    /// -
    /// D - Teil 3
    /// K�che - Fettsack -> K�che.Tischplatz2
    /// Spieler -> Freie Sitzwahl aber Dialog mit Fettsack n�tig
    /// -
    /// E - Teil 4
    /// Spieler -> B�ro des Chefs vor dem Schreibtisch des Ches
    /// Chef -> Chefb�ro.Chefsessel
    /// Endboss Gespr�ch langer Dialog, Geistesraum besuche 2 oder so und dann Ende
    public void ContinueToNextGameState()
    {
        // Prepare NPC for next stage
        foreach (var NPC in NPCs)
        {
            NPC.NPCOnState = true;  // Set's On to true, meaning ONinkJSON will be used next            
        }

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
                CurrentGameState = GameStateEnum.State3;     // Switch from stage 3 to 4
                break;
            case GameStateEnum.State3:                      // Endgame
                break;
            default:
                break;
        }
    }

    // Methode, um ein OfficeSpot-Objekt anhand des Namens zu finden
    public OfficeSpot GetOfficeSpotByName(string name)
    {
        foreach (OfficeSpot spot in OfficeSpots)
        {
            if (spot.Name == name)
            {
                return spot;
            }
        }
        return null; // R�ckgabe von null, falls kein passender Spot gefunden wurde
    }
}
