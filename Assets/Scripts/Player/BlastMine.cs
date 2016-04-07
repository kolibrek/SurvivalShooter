using UnityEngine;
using System.Collections.Generic;

public class BlastMine : MonoBehaviour {

	public float timer = 5f;
	public int damage = 150;
	
	List<Collider> victims;
	Light bombLight;
	AudioSource bombAudio;

	void Awake() {
		victims = new List<Collider>();
		bombLight = GetComponent<Light>();
		bombAudio = GetComponent<AudioSource>();
	}

	void Update () {
		timer -= Time.deltaTime;
		
		if (timer <= 0) {
			bombAudio.Play();
			Blast ();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other != null && other.GetComponent<EnemyHealth>() != null && !victims.Contains(other)) {
			victims.Add(other);
		}
	}

	void OnTriggerExit(Collider other) {
		victims.Remove(other);
	}

	void Blast() {
		bombLight.range = Mathf.Lerp(bombLight.intensity, 10f, 0.1f);
		bombLight.intensity = Mathf.Lerp(bombLight.intensity, 10f, 0.1f);
		if (victims.Count > 0) {
			foreach(Collider victim in victims) {
				if (victim != null && victim.GetComponent<EnemyHealth>() != null) {
					victim.GetComponent<EnemyHealth>().TakeDamage(damage, victim.transform.position);
				}
			}
		}
		Destroy(gameObject, 0.5f);
	}
}
