using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public abstract class DropItem : MonoBehaviour {

	public float rotationSpeed;
	public float timeRemaining;

	Light glow;

	void Start () {
		glow = GetComponent<Light>();
		Destroy(gameObject, timeRemaining);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			UseItem(other.gameObject);
			glow.intensity = 10f;
			Destroy(gameObject, 0.2f);
		}
	}

	public abstract void UseItem(GameObject player);
}
