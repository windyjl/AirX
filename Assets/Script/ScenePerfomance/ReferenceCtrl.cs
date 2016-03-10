using UnityEngine;
using System.Collections;

public class ReferenceCtrl : MonoBehaviour {
    public static int BuilMaxPerspective = 5; //  房子的最大距离。原本该值按像素计算。缩小到unity单位后，要小100倍
    public static float tanTheta = Data.SCREEN_WIDTH/2;
    public PerspectiveObject[] preBuilding;     // 房屋预置
    public GroundImage preGround;// 地面预置
    private float maxWidthOfBuilding = 0;
    ArrayList ReferenceObjsBGImage = new ArrayList();   // 景物们 
	// Use this for initialization
	void Start () {
        InitGround();
        float imgWidth;
        for (int i = 0; i < preBuilding.Length;++i ) {
            imgWidth = preBuilding[i].GetComponent<SpriteRenderer>().sprite.rect.width/200;
            if (maxWidthOfBuilding < imgWidth) {
                maxWidthOfBuilding = imgWidth;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        RefreshBuilding();
	}

    //初始化地板
    void InitGround() {
        // 创建地面
        for (int i = 0; i < GroundImage.BGSize; ++i) {
            GroundImage refercenobj = (GroundImage)Instantiate(preGround);
            refercenobj.transform.position = new Vector3((i - 1) * 1.89f - Data.SCREEN_WIDTH / 2, -0.6f, 1);
            refercenobj.Bangou = i;
            refercenobj.startPos = refercenobj.transform.position;
            ReferenceObjsBGImage.Add(refercenobj);
        }
    }

    // 刷新建筑
    void RefreshBuilding() {
        //if (offX < -Main.ScreenWidth * 100) offX = -Main.ScreenWidth * 100;
        int countCloud = GameObject.FindGameObjectsWithTag("Building").Length;
        if (countCloud < 3) {
            //在可视范围外刷新一个建筑。当角色速度灰常快时，刷新很可能超过半个屏幕，所以突然出现也不会突兀
            float z = Random.Range(1, BuilMaxPerspective);
            float offX;
            float VisibleRange = z * tanTheta * 2;
            float nextMove = Avatar.Instance.Speed.x*Time.deltaTime;
            if (nextMove < VisibleRange)
                offX = nextMove;
            else
                offX = nextMove - VisibleRange;
            float x = Random.Range(1f, 2f) * z * (tanTheta + maxWidthOfBuilding) + Camera.main.transform.position.x;
            SetBuilding(x + offX, z);
//             Debug.Log((x + offX - Camera.main.transform.position.x) / z);
//             if ((x + offX - Camera.main.transform.position.x) / z < tanTheta) {
//                 Debug.Log("tan不足");
//             }
        }
    }

    // 创建建筑
    void SetBuilding(float x, float z) {
        PerspectiveObject refercenobj = (PerspectiveObject)Instantiate(preBuilding[Random.Range(0, preBuilding.Length)]);
        refercenobj.transform.parent = gameObject.transform;
        refercenobj.Init(x, 2.6f, z);
    }
}
