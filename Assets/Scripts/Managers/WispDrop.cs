using UnityEngine;
using System.Collections;

public class WispDrop : DropItem {

	void Update() {
		transform.Rotate(new Vector3(0,rotationSpeed * Time.deltaTime,0));
	}
	
	public override void UseItem(GameObject player) {
		GameObject[] wisps = GameObject.FindGameObjectsWithTag("Wisp");
		foreach (GameObject wisp in wisps) {
			if (!wisp.GetComponent<Wisp>().enabled) {
				wisp.GetComponent<Wisp>().enabled = true;
				wisp.GetComponent<Light>().enabled = true;
				wisp.GetComponent<ParticleSystem>().Play();
				return;
			}
		}
	}
}
