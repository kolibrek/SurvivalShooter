using UnityEngine;

public class PauseManager : MonoBehaviour {
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			TogglePause();
		}
	}

	public void TogglePause() {
		Time.timeScale = (Time.timeScale == 0)? 1 : 0;
	}
}
