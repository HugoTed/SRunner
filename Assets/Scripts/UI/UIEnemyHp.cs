using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyHp : MonoBehaviour {

    public AIEnemy player;
    public Image imageHp;
    public Text textHp;
    private float cur_hp_value, temp_hp_value;
	// Use this for initialization
	void Start () {
        cur_hp_value = 1.0f;
        temp_hp_value = 1.0f;
    }
	
	// Update is called once per frame
	void Update () {
        //血量缓慢减少
        cur_hp_value = (float)player.cur_health / (float)player.max_health;
        if (cur_hp_value<temp_hp_value) { temp_hp_value -= 0.005f; }
        imageHp.fillAmount = temp_hp_value;
        if (imageHp.fillAmount == cur_hp_value) {temp_hp_value = cur_hp_value;}
        textHp.text = player.max_health.ToString() + " | " + (int)(imageHp.fillAmount * 100) + "%";
	}
}
