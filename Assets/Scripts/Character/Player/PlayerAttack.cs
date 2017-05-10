using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    public float attackTimer;
    public float coolDown;
    public bool attack = false;
    private Collider m_sphere;
    // Use this for initialization
    void Start () {
        m_sphere = GetComponent<Collider>();
        m_sphere.enabled = false;
        attackTimer = 0;
        coolDown = 2.0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
        if (attackTimer < 0)
            attackTimer = 0;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (attackTimer == 0)
            {
                attack = true;
                attackTimer = coolDown;
            }
        }
        checkAttack();

    }
    void checkAttack()
    {
        if (attack)
        {
            attack = false;
            m_sphere.enabled = true;
            Debug.Log("attacking");
        }
        if(attackTimer<1.5f)
        {
            m_sphere.enabled = false;
        }
    }
    
    void OnTriggerEnter(Collider enemy)
    {
        Debug.Log(enemy.name);
        m_sphere.enabled = false;
    }
}
