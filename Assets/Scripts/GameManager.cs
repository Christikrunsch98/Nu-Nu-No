using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OfficeSpots
{
    public string Name;
    public Transform Position;
}

public class GameManager : MonoBehaviour
{
    public List<OfficeSpots> OfficeSpots;

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
    public OfficeSpots GetOfficeSpotByName(string name)
    {
        foreach (OfficeSpots spot in OfficeSpots)
        {
            if (spot.Name == name)
            {
                return spot;
            }
        }
        return null; // Rückgabe von null, falls kein passender Spot gefunden wurde
    }

}
