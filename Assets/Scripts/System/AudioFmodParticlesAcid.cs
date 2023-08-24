using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioFmodParticlesAcid : MonoBehaviour
{
    private ParticleSystem _parentParticleSystem;

private int _currentNumberOfParticles = 0;

private void Start()

{

_parentParticleSystem = this.GetComponent<ParticleSystem>();

}

void Update()

{

if (_parentParticleSystem.particleCount < _currentNumberOfParticles)

{

}

if (_parentParticleSystem.particleCount > _currentNumberOfParticles)

{

FMODUnity.RuntimeManager.PlayOneShotAttached("event:/acid damage", gameObject);

}

_currentNumberOfParticles = _parentParticleSystem.particleCount;

}
}