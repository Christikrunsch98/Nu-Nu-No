using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeSpot : MonoBehaviour
{
    public bool Seat;           // sitzen ode nicht sitzen
    public Transform FacingTarget;     // NPC Rotation while on that spot

    // Wenn Player out of interactable range -> drehe dich zurück zu facing target
}
