using UnityEngine;

public class Wisp : MonoBehaviour {
	
	public float timeBetweenShots = 0.15f;
	public int damagePerShot = 5;

	public GameObject target;
	LineRenderer laserLine;
	Ray shootRay;
	RaycastHit shootHit;
	float effectsDisplayTime = 0.05f;
	float timer;
	
	// Use this for initialization
	void Start () {
		laserLine = GetComponentInChildren<LineRenderer>();
		Invoke("Disable", 30f);
	}

	void OnTriggerStay(Collider other) {
		if (target == null && other.GetComponent<EnemyHealth>() && !other.GetComponent<EnemyHealth>().isDead) {
			target = other.gameObject;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject == target) {
			target = null;
		}
	}

	void Update () {
		timer += Time.deltaTime;

		if(target != null && timer >= timeBetweenShots && Time.timeScale != 0) {
			Shoot ();
		}

		if(timer >= timeBetweenShots * effectsDisplayTime) {
			DisableEffects ();
		}

		Vector3 translation = transform.position;
		translation.y = Mathf.Sin(Time.time) * 0.5f + 1;
		transform.position = translation;

		transform.RotateAround(transform.parent.position, Vector3.up, Time.deltaTime * 10f);
	}
	
	public void DisableEffects () {
		laserLine.enabled = false;
	}

	public void Disable() {
		DisableEffects();
		GetComponent<ParticleSystem>().Stop();
		GetComponent<Light>().enabled = false;
		GetComponent<Wisp>().enabled = false;
	}

	void Shoot() {
		timer = 0f;
		laserLine.enabled = true;
		laserLine.SetPosition (0, transform.position);
		laserLine.SetPosition(1, target.transform.position + Vector3.up);

		target.GetComponent<EnemyHealth>().TakeDamage(damagePerShot, target.transform.position + Vector3.up);
	}
}
