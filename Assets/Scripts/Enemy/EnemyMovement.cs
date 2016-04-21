using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    NavMeshAgent nav;
	float baseSpeed;

	float timeToSpeed = 20f;
	public float maxSpeed;
	float timer = 0;


    void Awake () {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <NavMeshAgent> ();
		baseSpeed = nav.speed;
    }


    void Update () {
		timer += Time.deltaTime;
		if (timer >= timeToSpeed && nav.speed < maxSpeed) {
			nav.speed += baseSpeed;
			timer = 0;
		}

        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
            nav.SetDestination (player.position);
        } else {
            nav.enabled = false;
        }
    }
}
