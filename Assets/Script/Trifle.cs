using UnityEngine;
using System.Collections;
public enum TrifleType
{
    None=0,
    Headband,   //发带
    BowlerHat,  //礼帽
    Glasses,    //眼镜
    Writing,    //道符
    Max
}
public class Trifle : MonoBehaviour {
    public TrifleType myType;
    protected float lastPop = 0;
    protected float popRate = 3;
    public static string[] triflePrefabNames = { "T_Headband", "T_Hat", "T_Glasses", "T_Writing"};
	// Use this for initialization
	void Start () {
        Init();
	}
	
	// Update is called once per frame
	void Update () {
        CheckOut();
	}
    
    //创建在Main处理

    //初始化
    /* 在任意位置出现
     */
    private void Init() {
        transform.position =
            new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y)
            + new Vector3(Data.SCREEN_WIDTH * Random.Range(0.5f,1), Random.Range(-Data.SCREEN_HEIGHT, Data.SCREEN_HEIGHT));
    }

    //不加RigidBody2D会在一次操作内出发多次
    void OnTriggerEnter2D(Collider2D target) {
        //Debug.Log(target.name);
        DealEffect(Avatar.Instance);
        Destroy(gameObject);
    }

    void DealEffect(Avatar target) {
        //测试
        Debug.Log("触发了效果，ID：" + (int)myType);

        //根据自身类型触发不同效果
        switch (myType) {
            case TrifleType.Headband:
                target.setHeightUp();
                break;
            case TrifleType.BowlerHat:
                target.setDreamMode();
                break;
            case TrifleType.Glasses:
                target.setSpeedUp();
                break;
            case TrifleType.Writing:
                target.setWritingUp();
                break;
            default:
                break;
        }
    }

    //自检删除
    void CheckOut() {
        if ((Camera.main.transform.position.x - transform.position.x )> Data.SCREEN_WIDTH) {
            Destroy(gameObject);
        }
    }
}
