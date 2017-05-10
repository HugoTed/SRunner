using UnityEngine;
using System.Collections;

public class playerAnimCtrl : MonoBehaviour {

    public PlayerAttack playerAttack;
    public Animator attack_test;
    float v;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            attack_test.SetBool("attack", true);
        }
        else
        {
            attack_test.SetBool("attack", false);
        }

    }
}
