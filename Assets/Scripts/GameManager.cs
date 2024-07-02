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

    // Hier kannst du weitere Methoden und Eigenschaften des GameManagers hinzufügen
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
        return null; // Rückgabe von null, falls kein passender Spot gefunden wurde
    }


    /// Game State Vorgaben ///
    /// A - Der Anfang
    /// Nörgler.OfficeSpot -> Küche_Kaffee
    /// Nörgler.Reaktion -> Nörgler.OfficeSpot -> Küche_Snackautomat
    /// -
    /// B - Teil 1
    /// Ätzender Kollege.OfficeSpot -> Arbeitsplatz.ÄtzenderKollege
    /// Spieler Wegpunkt -> Arbeitsplatz.Spieler
    /// Andere auf ihren Plätzen oder irgendwo
    /// Meetingraum geschlossen und niemand zu sehen weiter
    /// -
    /// C - Teil 2
    /// Alle im Meetingraum.Sitz1,.Sitz2,.Sitz3,...
    /// Spieler Wegpunkt -> Meetingraum.Sitz6
    /// Nach dem Verlassen Teamleiter.NebenSpieler
    /// -
    /// D - Teil 3
    /// Küche - Fettsack -> Küche.Tischplatz2
    /// Spieler -> Freie Sitzwahl aber Dialog mit Fettsack nötig
    /// -
    /// E - Teil 4
    /// Spieler -> Büro des Chefs vor dem Schreibtisch des Ches
    /// Chef -> Chefbüro.Chefsessel
    /// Endboss Gespräch langer Dialog, Geistesraum besuche 2 oder so und dann Ende
    /// 
    public void ChangeCurrentGameState(GameStateEnum newGameState)
    {        
        switch (newGameState)
        {
            case GameStateEnum.State0:
                // Code ...
                break;
            case GameStateEnum.State1:
                // Code ...
                break;
            case GameStateEnum.State2:
                // Code ...
                break;
            case GameStateEnum.State3:
                // Code ...
                break;
        }

        CurrentGameState = newGameState;
    }
}
