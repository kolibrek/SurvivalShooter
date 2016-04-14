using UnityEngine;
using System.Collections;

public class ShotgunAmmo : DropItem {

	public int shellAmount;

	void Update() {
		transform.Rotate(new Vector3(0,rotationSpeed * Time.deltaTime,0));
	}
	
	public override void UseItem(GameObject player) {
		PlayerShotgunShooting shotgun = player.GetComponentInChildren<PlayerShotgunShooting>();
		shotgun.ammoRemaining += shellAmount;
	}
}
