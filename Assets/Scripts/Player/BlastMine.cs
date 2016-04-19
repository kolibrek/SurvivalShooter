using UnityEngine;
using System.Collections.Generic;

public class BlastMine : MonoBehaviour {

	public float timer = 5f;
	public int damage = 150;
	public GameObject explosion;

	List<Collider> victims;
	Light bombLight;

	void Awake() {
		victims = new List<Collider>();
		bombLight = GetComponentInChildren<Light>();
	}

	void Update () {
		timer -= Time.deltaTime;
		
		if (timer <= 0) {
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
		Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
