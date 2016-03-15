using UnityEngine;
using System.Collections;

public class EffectDelay : MonoBehaviour {
	ParticleSystem particle;
	public float time = 0.1f;
    private bool isLaunched = false;
	// Use this for initialization
	void Start () {
		particle = gameObject.GetComponent <ParticleSystem>();
        particle.simulationSpace = ParticleSystemSimulationSpace.Local;
	}
	
	// Update is called once per frame
	void Update () {
		if (time < 0) {
            particle.Play();
			particle.simulationSpace = ParticleSystemSimulationSpace.World;
		} else {
			time -= Time.deltaTime;
		}
	}
}
