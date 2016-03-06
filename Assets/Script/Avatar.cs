using UnityEngine;
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
    public Vector3 Accelerator = new Vector3(0, 0, 0);
    public Vector3 AcceleratorStep = new Vector3(20, 20, 0);
    protected Vector3 logicPosition;
    public bool isLaunched = false;
    //状态
    protected bool isDreamMode = false;
    protected float DreamModeDuration = 0f;
    //道具数量
    public int nWriting = 0;
    public void Awake() {
        if (instance == null) {
            instance = this;
        }
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
        //Speed.x
        Vector3 _spd = new Vector3(50, 50, 0);
        if (Data.Instance.lvInafune==0)
        {
        }
        isLaunched = true;
        Accelerator.x = _spd.x;
        Accelerator.y = _spd.y;
    }

    void DealSpeed()
    {
        if (Accelerator.x>0){
            Speed.x += AcceleratorStep.x * Time.deltaTime;
            Accelerator.x -= AcceleratorStep.x * Time.deltaTime;
        }
        if (Accelerator.y > 0) {
            Speed.y += AcceleratorStep.y * Time.deltaTime;
            Accelerator.y -= AcceleratorStep.y * Time.deltaTime;
        } else {
            Speed.y -= Define.GRAVITY * Time.deltaTime;
        }

        //最大下落速度
        if (Speed.y<-30){
            Speed.y = -30;
        }

        //着陆后减速
        if (logicPosition.y<=0){
            if (Speed.x > 0) {
                Speed.x -= AcceleratorStep.x * Time.deltaTime;
            } else {
                Speed.x = 0;
            }
        }
    }

    void DealMove()
    {
        logicPosition += Speed * Time.deltaTime;
        if (logicPosition.y<0)
        {
            logicPosition.y = 0;
            Speed.y = 0;
        }
    }

    //把逻辑位置转换成显示位置
    void ShowPosition()
    {
        transform.position = logicPosition;
    }

    //进入梦中（无敌）模式
    public void setDreamMode() {
        isDreamMode = true;
        DreamModeDuration = Data.Instance.getDreamModeTime();
        //根据不同等级给出反馈
        switch (Data.Instance.lvBowlerHat) {
            case 4:
                //圆神变身动画
            case 3:
                //与前进方向相同的飞行粒子
            case 2:
                //背景变色
            case 1:
                //满屏破碎特效，象征冲破时空束缚
            case 0:
                //普通条带
                break;
            default:
                Debug.Log("帽子等级超过上限");
                break;
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


    //纯测试用
//     void OnTriggerEnter2D(Collider2D target) {
//         Debug.Log(target.name);
//     }
}
