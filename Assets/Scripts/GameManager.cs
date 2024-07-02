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

    // Hier kannst du weitere Methoden und Eigenschaften des GameManagers hinzuf�gen
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
