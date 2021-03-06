﻿using UnityEngine;
using System.Collections;

public class Avatar : MonoBehaviour {
    private static Avatar instance;
    public static Avatar Instance {
        get {
            if (instance==null){
                instance = (Avatar)GameObject.Instantiate(Resources.Load("Prefabs/avatar"));
            }
            return instance;
        }
    }

    public Vector3 Speed;
    private float angleOfElevation = 0;//
    private Vector3 vectorOfElevationP90 = new Vector3(0,1); 
    public Vector3 Accelerator = new Vector3(0, 0, 0);
    protected Vector3 logicPosition;
    //迎角与受力相关测试
    public float kAerodynamics = 1f;
    protected float AngleStickRate = 40f;  //调整角度的力度 TODO 这个值可以做升级（最初设定10，很微妙。坏笑）
    protected float maxAeroAngle = 90f;
    //拖动相关
    private Vector3 touch0;
    //状态
    public bool isLaunched = false;
    public bool isDragging = false;
    public bool isControled = false;
    public bool isLanded = false;
    protected bool isDreamMode = false;
    protected float DreamModeDuration = 0f;
    //道具数量
    public int nWriting = 0;

    //测试数据
    public float timeLaunch;

    public void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

	// 初始化状态
	public void Init(){
		isLaunched = false;
		isDragging = false;
		isControled = false;
		isLanded = false;
		isDreamMode = false;
	}

	// Use this for initialization
	void Start () {
        logicPosition = new Vector3(0, 0, 0);
        nWriting = Data.Instance.getWritingValue();
	}
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate() {
        if (isLaunched) {
            DealSpeed();
            DealMove();
        }
        ShowPosition();
    }

    public void Launch()
    {
        timeLaunch = Time.time;
        //Speed.x
        Speed = Data.Instance.getLaunchSpeed();
        if (Data.Instance.lvInafune==0)
        {
        }
        isLaunched = true;

        angleOfElevation = Vector3.Angle(Vector3.right, Speed);
        CalcVectorOfSpeed();
//         Accelerator.x = _spd.x;
//         Accelerator.y = _spd.y;
    }

    void DealSpeed()
    {
//         if (Accelerator.x>0){
//             Speed.x += AcceleratorStep.x * Time.deltaTime;
//             Accelerator.x -= AcceleratorStep.x * Time.deltaTime;
//         }
//         if (Accelerator.y > 0) {
//             Speed.y += AcceleratorStep.y * Time.deltaTime;
//             Accelerator.y -= AcceleratorStep.y * Time.deltaTime;
//         } else {
//             Speed.y -= Define.GRAVITY * Time.deltaTime;
//         }
// 
//         //最大下落速度
//         if (Speed.y<-30){
//             Speed.y = -30;
//         }
        float aos = Vector3.Angle(new Vector3(1, 0), Speed);
        if (Speed.y<0) { aos *= -1;}
        float aoa = (angleOfElevation - aos)%360;
        if (aoa<-180){
            aoa += 360;
        }else if (aoa>180){
            aoa -= 360;
        }
        //angleOfElevation;
        
        //浮力和效果计算  方向 * 大小 * 常数 * 速度
        float spdmag = Speed.magnitude; //TODO 临时限制速度
        Vector3 res = vectorOfElevationP90 * FnAA(aoa) * kAerodynamics * Speed.magnitude;
//         Debug.Log("角度"+angleOfElevation+
//             "\t攻角"+aoa+
//             "\n方向" + vectorOfElevationP90 + "\n大小" + FnAA(aoa) + "\t常数" + kAerodynamics + "\t速度" +Speed.magnitude+"\n结果:"+res);
//		Debug.Log ("机翼垂直方向"+Vector3.Angle (vectorOfElevationP90, Speed)+"\n升力方向"+Vector3.Angle (res, Speed));
		Speed += res  * Time.deltaTime;
        //Speed = Speed.normalized * spdmag;//TODO 临时限制速度

        //重力计算
        Speed.y -= Data.Instance.fGravity*Time.deltaTime;
        //阻力计算
        //Speed = Speed.normalized * (Speed.magnitude - Data.Instance.getResistance()*Time.deltaTime);
        float resistance = Data.Instance.getResistance() * Time.deltaTime;
        if (Speed.x - resistance > 0) {
            Speed.x -= Data.Instance.getResistance() * Time.deltaTime;
        } else {
            Speed.x = 0;
        }

        //着陆后减速
        if (isLanded){
            //Debug.Log(timeLaunch-Time.time);
            if (Speed.x > 0) {
                Speed.x -= Data.Instance.groundResistance * Time.deltaTime;
            } else {
                Speed.x = 0;
            }
        }
    }

    // 攻角与升力的模糊计算，返回0~1的值，后续由常数调节
    public float FnAA(float angle) {
        int isPos = 1;
        if (angle<0){
            isPos = -1;
        }
        angle = Mathf.Abs(angle);
        float max = maxAeroAngle;
        float res = 0;
        if (angle<=max){
            return angle / max * isPos;
        }else if (angle>max){   //去掉了模拟失速的部分，就算角度很大仍然能保证力度。并测试能否完全模拟糊脸角度的减速
            res = 1;
        }
        return res*isPos;
    }

    //移动控制
    void DealMove()
    {
        logicPosition += Speed * Time.deltaTime;
        if (logicPosition.y<0)
        {
            isLanded = true;
            logicPosition.y = 0;
            Speed.y = 0;
        }

        //如果发射后没有控制过方向，那么机头朝速度方向自动调整
        if (!isControled && !isLanded) {
            angleOfElevation = Vector3.Angle(Speed,Vector3.right);
            if (Speed.y<0){
                angleOfElevation *= -1;
            }
            if (angleOfElevation<-30){
                angleOfElevation = -30;
            }
            CalcVectorOfSpeed();
        }else if (isLanded){
            angleOfElevation = 0;
        }
    }

    //把逻辑位置转换成显示位置
    void ShowPosition()
    {
        if (!isLaunched && isDragging) {
        }
        transform.position = logicPosition;

        transform.transform.rotation = Quaternion.Euler(0,0,angleOfElevation);
    }

    //拖拽起飞
    void OnDragStart() {
        if (Input.GetMouseButtonDown(0)&&!isLaunched) {
            touch0 = Input.mousePosition;
            isDragging = true;
        }
    }

    //进入梦中（无敌）模式
    public void setDreamMode() {
        isDreamMode = true;
        DreamModeDuration = Data.Instance.getDreamModeTime();
        //根据不同等级给出反馈
        for (int i = 0; i < Data.Instance.lvBowlerHat+1 ; i++){
            GameObject obj = (GameObject)Instantiate(Resources.Load("Prefabs/Effects/Avatar/DreamModeEffectLv"+(i+1)));
            switch (i) {
                case 4:
                    //圆神变身动画
                    obj.transform.parent = Camera.main.transform;
                    break;
                case 3:
                    //与前进方向相同的飞行粒子
                    obj.transform.parent = Camera.main.transform;
                    break;
                case 2:
                    //背景变色
                    obj.transform.parent = Camera.main.transform;
                    break;
                case 1:
                    //满屏破碎特效，象征冲破时空束缚
                    obj.transform.parent = Camera.main.transform;
                    break;
                case 0:
                    //普通条带
                    obj.transform.parent = transform;
                    break;
                default:
                    Debug.Log("帽子等级超过上限");
                    break;
            }
            obj.transform.localPosition = Vector3.zero;
            Destroy(obj, 5f);
        }
    }

    //提高垂直速度，发带效果
    public void setHeightUp() {
        Accelerator.y += Data.Instance.getHeadBandValue();
    }
    //提高水平速度，眼镜效果
    public void setSpeedUp() {
        Accelerator.x += Data.Instance.getGlassesValue();
        //根据不同等级给出反馈 TODO不做为道具效果，当速度超过一定值时自动出现
        switch (Data.Instance.lvBowlerHat) {
            case 4:
            case 3:
            //与前进方向相反的速度线粒子
            case 2:
            case 1:
            //拉远镜头
            case 0:
            //命中效果（一碗拉面）
                break;
            default:
                Debug.Log("眼镜等级超过上限");
                break;
        }
    }

    public void setWritingUp() {

    }

    //使用道符
    /*
     * 道符产生随机效果
     * 1、
     * 2、
     * 3、
     */
    public void UseWriting() {
        if (nWriting>0){
            nWriting--;
            // 道符的效果
            Debug.Log("使用了道符");
        }
    }

    //拉起
    public void PullUp() {
        angleOfElevation += AngleStickRate * Time.deltaTime;
        CalcVectorOfSpeed();
        isControled = true;
    }

    //推下
    public void PushDown() {
        angleOfElevation -= AngleStickRate * Time.deltaTime;
        CalcVectorOfSpeed();
        isControled = true;
    }

    protected void CalcVectorOfSpeed() {
        float theta = (angleOfElevation+90)/180*Mathf.PI;
        vectorOfElevationP90 = new Vector3(Mathf.Cos(theta), Mathf.Sin(theta));
    }

    //纯测试用
//     void OnTriggerEnter2D(Collider2D target) {
//         Debug.Log(target.name);
//     }
}
