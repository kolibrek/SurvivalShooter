using UnityEngine;

public class HealthPack : MonoBehaviour {

	public PlayerHealth playerHealth;
	public int healthAmount;
	public float rotationSpeed;
	public float timeRemaining;

	Light glow;

	void Start() {
		playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
		glow = GetComponent<Light>();
		Destroy(gameObject, timeRemaining);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			playerHealth.currentHealth += healthAmount;
			if (playerHealth.currentHealth > playerHealth.startingHealth) {
				playerHealth.currentHealth = playerHealth.startingHealth;
			}
			glow.intensity = 10f;
			playerHealth.healthSlider.value = playerHealth.currentHealth;

			Destroy(gameObject, 0.2f);
		}
	}

	void Update () {
		transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
	}
}
