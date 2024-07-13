using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonNPC : MonoBehaviour
{
    public Material material;

    MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    string pokemonName = "";

    // Update is called once per frame
    void Update()
    {
        pokemonName = ((Ink.Runtime.StringValue) DialogueManager.Instance.GetInkVariableState("pokemonName")).value;

        switch (pokemonName)
        {
            case "":
                meshRenderer.material.color = Color.white;
                break;
            case "Charmander":
                meshRenderer.material.color = Color.red;
                break;
            case "Bulbasaur":
                meshRenderer.material.color = Color.green;
                break;
            case "Squirtle":
                meshRenderer.material.color = Color.blue;
                break;
            default:
                Debug.LogWarning("Pokemon Name in pokemonName [global ink variable] not handled by switch statement: " + pokemonName);
                break;
        }
    }
}
