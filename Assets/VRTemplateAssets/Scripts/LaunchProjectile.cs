using System.Collections;
using UnityEngine;

namespace Unity.VRTemplate
{
    /// <summary>
    /// Apply forward force to instantiated prefab
    /// </summary>
    public class LaunchProjectile : MonoBehaviour
    {
        [Header("Self Build")]
        public GameObject BulletPrefab;
        public float Cooldown = 0.2f;


        [Header("Unity Build")]
        [SerializeField]
        [Tooltip("The projectile that's created")]
        GameObject m_ParticlePrefab = null;

        [SerializeField]
        [Tooltip("The point that the project is created")]
        Transform m_StartPoint = null;

        [SerializeField]
        [Tooltip("The speed at which the projectile is launched")]
        float m_LaunchSpeed = 1.0f;

        bool fired = false;

        public void Fire()
        {
            // Cooldown
            if (fired) return;
            else
            {
                fired = true;
                StartCoroutine(CooldownCoroutine());
            }                       

            // Confetti
            GameObject newObject = Instantiate(m_ParticlePrefab, m_StartPoint.position, m_StartPoint.rotation, null);

            if (newObject.TryGetComponent(out Rigidbody rigidBody))
                ApplyForce(rigidBody);

            // Bullet
            Instantiate(BulletPrefab, m_StartPoint.position, m_StartPoint.rotation, null);
        }

        void ApplyForce(Rigidbody rigidBody)
        {
            Vector3 force = m_StartPoint.forward * m_LaunchSpeed;
            rigidBody.AddForce(force);
        }

        IEnumerator CooldownCoroutine() 
        {
            yield return new WaitForSeconds(Cooldown);
            fired = false;
        }
    }
}
