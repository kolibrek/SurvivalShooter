using UnityEngine;
using System.Collections;

public class DropManager : MonoBehaviour {
	
	public GameObject[] dropItems;

	public void SpawnItem(Vector3 position) {
		float lootRoll = Random.value;
		if (lootRoll <= 0.33) {
			Instantiate(dropItems[0], position, Quaternion.Euler(new Vector3(90,0,0)));
		} else if (lootRoll <= 0.66) {
			Instantiate(dropItems[1], position, Quaternion.identity);
		} else {
			Instantiate(dropItems[2], position, Quaternion.identity);
		}

	}
}
