using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleShrinker : MonoBehaviour
{
    [SerializeField] private float defaultScale;
    [SerializeField] private float largestSize;
    [SerializeField] private float lerpDistance;

    private void Start()
    {
        defaultScale = ParticleSystem.Particle.startLifetime;
    }

    // Update is called once per frame
    private void Update()
    if (Input.GetKeyDown(KeyCode.Mouse0))
    {
        ParticleSystem.Particle.startLifetime = defaultScale
    }

    ParticleSystem.Particle.startLifetime = mathf.Lerp(ParticleSystem.Particle.startLifetime, largestSize, lerpDistance * Time.deltaTime);
    
}
