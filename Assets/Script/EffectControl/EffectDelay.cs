using UnityEngine;
using System.Collections;

public class EffectDelay : MonoBehaviour {
	ParticleSystem particle;
	public float time = 0.1f;
	// Use this for initialization
	void Start () {
		particle = gameObject.GetComponent <ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if (time < 0) {
			particle.Play ();
		} else {
			time -= Time.deltaTime;
		}
	}
}
