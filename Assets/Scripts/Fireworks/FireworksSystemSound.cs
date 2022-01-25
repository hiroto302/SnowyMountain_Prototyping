using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

// 花火の音の制御
// 花火が点火した時の音
// 花火が打ち上がる音
// 花火が破裂する音

[RequireComponent(typeof(ParticleSystem))]
public class FireworksSystemSound : MonoBehaviour
{

    [SerializeField] AudioClip[] shootSound = null;
    [SerializeField] AudioClip[] explosionSound = null;
    [SerializeField] AudioClip[] crackleSound = null;
    [SerializeField] AudioSource source = null;

    // 打ち上げられた花火の Trail が消滅した時に呼ばれる event
    [Serializable] public class OnDeathEvent : UnityEvent<Vector3>{}
    public OnDeathEvent onDeath;

    void LateUpdate()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[GetComponent<ParticleSystem>().particleCount];
        int length = GetComponent<ParticleSystem>().GetParticles(particles);
        int i = 0;
        while(i < length)
        {
            if(explosionSound.Length > 0 && particles[i].remainingLifetime < Time.deltaTime)
            {
                // 爆発音 particles[i].position の位置で
                // PlaySound(explosionSound[0], 1.0f, particles[i].position);
                PlaySound(explosionSound[0], 1.0f);
            }
            if(shootSound.Length > 0 && particles[i].remainingLifetime >= particles[i].startLifetime - Time.deltaTime)
            {
                // 打ち上げ音
                // PlaySound(shootSound[0], 0.5f, particles[i].position);
                PlaySound(shootSound[0], 0.5f);
            }
            i++;
        }
    }

    void PlaySound(AudioClip sound, float volume, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(sound, position, volume);
    }

    void PlaySound(AudioClip sound, float volume)
    {
        source.PlayOneShot(sound, 1.0f);
    }

}
