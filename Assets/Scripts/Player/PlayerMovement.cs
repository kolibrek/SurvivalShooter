using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	public float speed = 6f;
	public float boostForce = 600f;
	public Slider boostSlider;

	int boostTimer = 100;
	float totalSpeed;
	Vector3 movement;
	Vector3 direction;
	Animator anim;
	Rigidbody playerRigidbody;
	ParticleSystem boost;
	int floorMask;
	float camRayLength = 100f;

	void Awake() {
		floorMask = LayerMask.GetMask("Floor");
		anim = GetComponent<Animator>();
		playerRigidbody = GetComponent<Rigidbody>();
		boost = transform.FindChild("BoostParticles").GetComponent<ParticleSystem>();
	}

	void FixedUpdate() {
		totalSpeed = speed;
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		Move(h,v);
		Turning();
		Animating(h,v);
	}

	void Update() {
		boostSlider.value = boostTimer;
		if (boostTimer < 100) {
			boostTimer++;
		}
		if (Input.GetButtonDown("Jump") && boostTimer == 100 && Time.timeScale != 0) {
			if (movement.magnitude > 0) {
				playerRigidbody.AddForce(movement.normalized * boostForce);
			} else {
				playerRigidbody.AddForce(direction.normalized * boostForce);
			}

			boostTimer = 0;
			boost.Play();
			Invoke("StopBoost", 1f);
		}
	}

	void Move(float h, float v) {
		movement.Set(h,0f,v);
		movement = movement.normalized * totalSpeed * Time.deltaTime;

		playerRigidbody.MovePosition(transform.position + movement);
	}

	void Turning() {
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit floorhit;

		if (Physics.Raycast(camRay, out floorhit, camRayLength, floorMask)) {
			Vector3 playerToMouse = floorhit.point - transform.position;
			playerToMouse.y = 0f;

			direction = playerToMouse;

			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(newRotation);
		}
	}

	void Animating(float h, float v) {
		bool walking = h != 0f || v!= 0f;
		anim.SetBool("IsWalking", walking);
	}

	void StopBoost() {
		boost.Stop();
	}
}
