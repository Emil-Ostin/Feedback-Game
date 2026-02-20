using UnityEngine;

public class BushParticle : MonoBehaviour
{
    ParticleSystem particleSystem;
    private void Start() { particleSystem = GetComponent<ParticleSystem>(); }
    private void OnTriggerEnter2D() { particleSystem.Play(); }
}
