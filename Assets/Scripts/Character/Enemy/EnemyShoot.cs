using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {

    public GameObject shell;
    public float shootPower=40;
    public Transform shootPoint;
    private AudioSource audioShoot;
    public float shootkTimer;
    public float coolDown;
    // Use this for initialization
    void Start () {
        audioShoot = GetComponent<AudioSource>();
        shootkTimer = 0;
        coolDown = 5.0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (shootkTimer > 0)
            shootkTimer -= Time.deltaTime;
        if (shootkTimer < 0)
            shootkTimer = 0;
        if (shootkTimer == 0)
        {
            shootkTimer = coolDown;
            Shoot();
        }
	}
    void Shoot() {
        GameObject newShell = Instantiate(shell,shootPoint.position,shootPoint.rotation);
        Rigidbody r = newShell.GetComponent<Rigidbody>();
        r.velocity = shootPoint.forward * shootPower;
        audioShoot.Play();
    }
}
