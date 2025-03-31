using UnityEngine;

public class ParticleDetector : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Detection");
        // Obtener el sistema de partículas
        ParticleSystem particleSystem = other.GetComponent<ParticleSystem>();
        if (particleSystem == null) return;
        Debug.Log("Particle system detected");
    }
}
