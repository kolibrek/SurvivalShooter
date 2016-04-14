using UnityEngine;

public class HealthPack : DropItem {
	
	public int healthAmount;

	void Update() {
		transform.Rotate(new Vector3(0,0,rotationSpeed * Time.deltaTime));
	}

	public override void UseItem(GameObject player) {
		PlayerHealth health = player.GetComponent<PlayerHealth>();
		health.currentHealth += healthAmount;
		if (health.currentHealth > health.startingHealth) {
			health.currentHealth = health.startingHealth;
		}
		health.healthSlider.value = health.currentHealth;
	}
}
