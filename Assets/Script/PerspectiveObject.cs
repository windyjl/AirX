using UnityEngine;
using System.Collections;
public enum BGIType {
    NONE=0,
    XAY,    //X和Y均有透视
    X,      //仅横向有透视效果
    Y,      //背景图片，X轴始终更随，Y方向有透视
    MAX
}
public class PerspectiveObject : MonoBehaviour {
    public static float PerspectiveDis = 1;   //视距100为基础，该类的参考系与实际游戏坐标100:1换算
    public BGIType typeBGI = BGIType.X;
    public Vector3 Position;
    public Vector3 CentreOff=new Vector3();   //最终显示时进行偏移，【注意】所有的图片都当做点来使用，透视只参考这个点
    public SpriteRenderer sprite;
    private Main refMain;               // 主逻辑对象引用
    public bool isInit = false;
    private bool isFirstTime = false;

    //检测用变量
    private bool isDidCheck = false;
    // Use this for initialization
    void Start() {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        Init();
    }
    //参考Y值0是地面，Z值100是1:1透视
    public void Init(float x, float y, float z) {
        Position = new Vector3(x, y, z);
        isInit = true;
    }
    public void Init() {
        if (Position.z==0){
            Position.z = 100;
        }
        isInit = true;
    }
    // Update is called once per frame
    void Update() {
        if (!isInit){
            return;
        }
        //如果参照物飞出视野左侧以外就删除
        float LeftBorderOfCamera = Camera.main.transform.position.x - Data.SCREEN_WIDTH/2;
        float RefObjWidth = sprite.sprite.rect.width * transform.localScale.x / sprite.sprite.pixelsPerUnit;
//         Vector3 viewPos = GetDisplayPosition();
//         float abc = RefObjWidth / 100;
        if (transform.position.x+RefObjWidth/2 < LeftBorderOfCamera) {
            Destroy(gameObject);
        }
    }
    void FixedUpdate() {
        if (isInit) {
            transform.position = GetDisplayPosition();
        }
        if (!isDidCheck){
            isDidCheck = true;
            if (Mathf.Abs(transform.position.x - Camera.main.transform.position.x)<(Data.SCREEN_WIDTH/2-sprite.sprite.rect.width/200)){
                Debug.Log("fuck you，there is bug");
            }
        }
    }
    Vector3 GetDisplayPosition() {
        Vector3 off = Position - Camera.main.transform.position;
        float tanThetaX = off.x / Position.z;
        float tanThetaY = off.y / Position.z;
        Vector3 viewPos = Camera.main.transform.position + new Vector3(PerspectiveDis * tanThetaX, PerspectiveDis * tanThetaY, 0);  //
        //建筑物额外处理
        if (typeBGI==BGIType.X) {
            viewPos.y = Position.y;
        }else if (typeBGI == BGIType.Y){
            viewPos.x = Camera.main.transform.position.x;
        }
        viewPos.z = Position.z;    //原本z值以像素为单位，需要换算。以前是/10的

        //         if (!isFirstTime)
        //         {
        //             print("MyX-cameraPosX:" + (viewPos.x-Camera.main.transform.position.x));
        //         }
        //         isFirstTime = true;
        return viewPos + CentreOff;
    }
}
