using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    //记录当前敌人状态 根据不同类型 播放不同动画
    // 普通状态 旋转状态 奔跑状态 追击状态 攻击状态
    public int state = 0;
    public const int NORMAL = 0;
    public const int ROTATION = 1;
    public const int RUN = 2;
    public const int JUMP = 3;
    public const int CHASE = 4;
    public const int ATTACK = 5;
    public const int DEAD = 6;

    //血量
    public int max_health;
    public int cur_health;

    //伤害
    public int damage;

    //死亡效果
    public GameObject deadEffect;

    // Use this for initialization
    public void Start () {
        cur_health = max_health;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void getDamage(int damage)
    {
        if (cur_health > damage){
            cur_health -= damage;
        }
        else {
            cur_health = 0;
            state = DEAD;
            dead();
        }
    }
    public void dead()
    {
        if (state == DEAD) {
            //Instantiate(deadEffect, transform.position,transform.rotation);
        }
        Destroy(gameObject);
    }
}
