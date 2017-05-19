using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这里是枚举选择敌人类型
public enum EnemyType
{
    Enemy,
    Boss
}

public class AIEnemy : Unit {

    //敌人类型枚举
    public EnemyType enemyType = EnemyType.Enemy;

    //主角游戏对象
    public GameObject player;

    //旋转状态，敌人自身旋转
    private int rotation_state;
    //记录敌人上一次思考时间
    private float aiThankLastTime;
    //玩家（无Y轴）位置
    public Vector3 newplayerpoint;
    //获取角色控制器
    private CharacterController enemyCtrl;
    //是否在地面
    private bool isground;

    // Use this for initialization
    void Start () {
        base.Start();
        //初始话标志敌人状态 以及动画为循环播放
        state = NORMAL;
        enemyCtrl = GetComponent<CharacterController>();
        isground = true;
        //------------------------------------------------------------
        this.GetComponent<Animation>().wrapMode = WrapMode.Loop;
       // ------------------------------------------------------------
    }
	// Update is called once per frame
	void Update () {
        //
        //CollisionFlags flags = enemyCtrl.Move(Vector3.forward *0.02f);
        //isground = (flags & CollisionFlags.CollidedBelow) != 0; //当controller处在空中间，grounded为false，即跳动和行走都无效

        newplayerpoint = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
        //根据策划选择的敌人类型 这里面会进行不同的敌人AI
        switch (enemyType)
        {
            case EnemyType.Enemy:
                updateEnemy();
                break;
            case EnemyType.Boss:
                updateBoss();
                break;
        }
    }

    //更新Boss敌人的AI
    void updateBoss()
    {

        //判断敌人是否开始思考
        if (isAIthank())
        {
            //敌人开始思考
            AIthankEnemyState(3);
        }
        else
        {
            //更新敌人状态
            UpdateEmenyState();
        }
    }
    //更新Enemy敌人的AI
    void updateEnemy()
    {

        //判断敌人是否开始思考
        if (isAIthank())
        {
            //敌人开始思考
            AIthankEnemyState(3);
        }
        else
        {
            //更新敌人状态
            UpdateEmenyState();
        }
    }
    int getRandom(int count)
    {

        return new System.Random().Next(count);

    }
    //敌人在这里进行思考
    bool isAIthank()
    {
        //这里表示敌人每3秒进行一次思考
        if (Time.time - aiThankLastTime >= 3.0f)
        {
            aiThankLastTime = Time.time;
            return true;

        }
        return false;
    }
    //在这里更新敌人状态
    void AIthankEnemyState(int count)
    {
        //开始随机数字。
        int d = getRandom(count);

        switch (d)
        {
            case 0:
                //设置敌人为站立状态
                setEmemyState(NORMAL);
                break;
            case 1:
                //设置敌人为旋转状态
                setEmemyState(ROTATION);
                break;
        }

    }
    void setEmemyState(int newState)
    {
        if (state == newState)
            return;
        state = newState;

        string animName = "idle";
        switch (state)
        {
            case NORMAL:
                animName = "idle";
                break;
            case RUN:
                animName = "runforward";
                break;
            case ROTATION:
                animName = "idle";
                //当敌人为旋转时， 开始随机旋转的角度系数
                rotation_state = getRandom(4);
                break;
            case CHASE:
                animName = "runforward";
                //当敌人进入追击状态时，将面朝主角方向奔跑
                this.transform.LookAt(newplayerpoint);
                //player.transform
                break;
            case ATTACK:
                animName = "Attack";
                //当敌人进入攻击状态时，继续朝向主角开始攻击砍人动画
                this.transform.LookAt(newplayerpoint);
                break;
        }
        //避免重复播放动画，这里进行判断
        if (!this.GetComponent<Animation>().IsPlaying(animName))
        {
            //播放动画
            this.GetComponent<Animation>().Play(animName);
        }

    }
    //在这里更新敌人状态
    void UpdateEmenyState()
    {
        //判断敌人与主角之间的距离
        float distance = Vector3.Distance(player.transform.position, this.transform.position);
        //当敌人与主角的距离小于10 敌人将开始面朝主角追击
        if (distance <= 20)
        {
            //当敌人与主角的距离小与3 敌人将开始面朝主角攻击
            if (distance <= 3)
            {
                setEmemyState(ATTACK);
            }
            else
            {
                //否则敌人将开始面朝主角追击
                setEmemyState(CHASE);
            }

        }
        else
        {
            //敌人攻击主角时 主角迅速奔跑 当它们之间的距离再次大于10的时候 敌人将再次进入正常状态 开始思考
            if (state == CHASE || state == ATTACK)
            {
                setEmemyState(NORMAL);
            }

        }

        switch (state)
        {
            case ROTATION:
                //旋转状态时 敌人开始旋转， 旋转时间为1秒 这样更加具有惯性
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, rotation_state * 90, 0), Time.deltaTime * 1);
                break;
            case RUN:
                //奔跑状态，敌人向前奔跑
                //if (isground)
                    transform.Translate(Vector3.forward * 0.2f); 
                break;
            case CHASE:
                //追击状态 敌人向前开始追击
                //if (isground)
                    transform.Translate(Vector3.forward * 0.2f);
                break;
            case ATTACK:

                break;
        }

    }
}
