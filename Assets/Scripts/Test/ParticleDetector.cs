using UnityEngine;

public class ParticleDetector : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Detection");
        // Obtener el sistema de part�culas
        ParticleSystem particleSystem = other.GetComponent<ParticleSystem>();
        if (particleSystem == null) return;
        Debug.Log("Particle system detected");
    }
}
