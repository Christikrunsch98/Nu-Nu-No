using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class People : MonoBehaviour
{
    // People's state machine
    // Walking
    // Sitting
    // Working
    // Talking
    // Gesturing

    [SerializeField] Transform currentDestination;
    [SerializeField] float tolerance = 0.1f; // Toleranzwert für den Positionsvergleich
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDestination == null) return;

        agent.destination = currentDestination.position;
    }

    public void SetCurrentDestination(string officeSpotName)
    {
        currentDestination = GameManager.Instance.GetOfficeSpotByName(officeSpotName).Position;
        CheckIfCurrentDestinationIsReached();
    }

    public bool CheckIfCurrentDestinationIsReached()
    {
        if (Vector3.Distance(new Vector3(transform.position.x,0,transform.position.z), currentDestination.position) < tolerance)
            return true;            
        else return false;
    }
}
