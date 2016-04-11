using UnityEngine;
using System.Collections;

public class DropManager : MonoBehaviour {
	
	public GameObject[] dropItems;

	public void SpawnItem(Vector3 position) {
		float lootRoll = Random.value;
		if (lootRoll <= 1) {
			Instantiate(dropItems[0], position, Quaternion.Euler(new Vector3(90,0,0)));
		}
	}
}
