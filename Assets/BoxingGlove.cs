using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingGlove : MonoBehaviour
{
    public float PunchForce = 1000f;

    public void Punch(Rigidbody rb)
    {
        Debug.Log("Boxed Vrouw");

        // Berechne die Richtung der Kraft basierend auf der Richtung des Schlags
        Vector3 punchDirection = transform.right; // Annahme: Der Handschuh bewegt sich nach vorne

        // Wende die Kraft auf die kollidierte Person an
        if (rb != null)
            rb.AddForce(punchDirection * PunchForce, ForceMode.Impulse);
    }

}
