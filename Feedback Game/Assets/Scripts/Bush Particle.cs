using UnityEngine;

public class BushParticle : MonoBehaviour
{
    ParticleSystem particleSystem;
    AudioSource audio;
    [SerializeField] AudioClip sound;

    private void Start() { particleSystem = GetComponent<ParticleSystem>(); audio = GetComponent<AudioSource>(); }
    private void OnTriggerEnter2D() { particleSystem.Play(); audio.PlayOneShot(sound); }
}
