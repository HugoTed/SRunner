using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
    public int temp_damage;

    //死亡效果
    public GameObject deadEffect;

    // Use this for initialization
    public void Start() {
        cur_health = max_health;
        damage = 0;
    }

    // Update is called once per frame
    void Update() {

    }

    public void ApplyDamage(int damage)
    {
        if (cur_health > damage) {
            cur_health -= damage;
            temp_damage = damage;
        }
        else {
            cur_health = 0;
            state = DEAD;
            dead();
        }
    }
    public void ApplyHP(int HP)
    {
        if (cur_health < max_health)
        {
            cur_health += HP;
        }
        if (cur_health >= max_health)
        {
            cur_health = max_health;
        }

    }
    public void dead()
    {
        if (state == DEAD) {
            //Instantiate(deadEffect, transform.position,transform.rotation);
        }
        //Destroy(gameObject);
        if (gameObject.tag == "Player") {
            SceneManager.LoadScene("level0");
        }
        else { Destroy(gameObject); }
        
    }
}
