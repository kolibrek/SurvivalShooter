using UnityEngine;
using UnityEngine.UI;

public class PlayerShotgunShooting : MonoBehaviour {
	
	public int damagePerShot = 12;
	public float timeBetweenShells = 1.6f;
	public float range = 10f;
	public int ammoRemaining = 0;
	
	float timer;
	
	Ray[] blastRay;
	RaycastHit blastHit;
	
	int shootableMask;
	ParticleSystem gunParticles;
	LineRenderer[] gunLines;
	AudioSource[] gunAudio;
	Light gunLight;
	Text ammoDisplay;
	float effectsDisplayTime = 0.2f;
	
	
	void Awake () {
		shootableMask = LayerMask.GetMask ("Shootable");
		gunParticles = GetComponent<ParticleSystem> ();
		gunLines = GetComponentsInChildren<LineRenderer>();
		gunAudio = GetComponents<AudioSource>();
		gunLight = GetComponent<Light> ();
		ammoDisplay = GameObject.Find("AmmoText").GetComponent<Text>();
	}
	
	void Update () {
		timer += Time.deltaTime;

		if (Input.GetButton("Fire1")) {
			timer = 0f;
		}
		
		if (Input.GetButton("Fire2") && timer >= timeBetweenShells && Time.timeScale != 0 && ammoRemaining > 0) {
			Blast();
			ammoRemaining--;
		}
		
		if(timer >= 0.15f * effectsDisplayTime) {
			DisableEffects ();
		}

		ammoDisplay.text = "Alternate Ammo: " + ammoRemaining.ToString("00");
	}
	
	
	public void DisableEffects () {
		for (int i = 0; i < gunLines.Length; i++) {
			gunLines[i].enabled = false;
		}
		gunLight.enabled = false;
	}
	
	void Blast() {
		timer = 0f;
		
		gunAudio[1].Play ();
		
		gunLight.enabled = true;
		
		gunParticles.Stop ();
		gunParticles.Play ();
		
		for(int i = 0; i < gunLines.Length; i++) {
			gunLines[i].enabled = true;
			gunLines[i].SetPosition(0, transform.position);
		}
		
		blastRay = new Ray[gunLines.Length];
		
		for (int i = 0; i < gunLines.Length; i++) {
			blastRay[i].origin = transform.position;
			blastRay[i].direction = transform.forward + transform.right * Random.Range(-0.3f, 0.3f);
			
			if (Physics.Raycast(blastRay[i], out blastHit, range, shootableMask)) {
				EnemyHealth enemyHealth = blastHit.collider.GetComponent<EnemyHealth>();
				if (enemyHealth != null) {
					enemyHealth.TakeDamage(damagePerShot, blastHit.point);
				}
				gunLines[i].SetPosition(1, blastHit.point);
			} else {
				gunLines[i].SetPosition(1, blastRay[i].origin + blastRay[i].direction * range);
			}
		}
	}
}

