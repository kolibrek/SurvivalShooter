using UnityEngine;
using System.Collections;

public class PlayerLayMine : MonoBehaviour {
	
	public float regainTime = 10f;
	public int maxMines = 2;
	public GameObject mine;

	int minesAvailable;
	float timer;

	// Use this for initialization
	void Start () {
		timer = 0;
		minesAvailable = maxMines;
	}
	
	// Update is called once per frame
	void Update () {
		if (minesAvailable < maxMines) {
			timer += Time.deltaTime;
			if (timer >= regainTime) {
				minesAvailable++;
				timer = 0;
			}
		}

		if (Input.GetButtonDown("Jump")  && minesAvailable > 0) {
			Instantiate(mine, this.transform.position + Vector3.up * 2, Quaternion.identity);
			minesAvailable--;
		}
	}
}
