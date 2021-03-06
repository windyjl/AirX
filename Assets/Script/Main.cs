﻿using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
    private static Main instance;
    private Main() {}
    public GameObject uiLevelEnd;
    public Sprite[] ZeroSprites;
    public static Main Instance {
        get {
            return instance;
        }
    }
    public static string sPath="";

    public void Awake() {
        sPath = Application.dataPath;
        if (instance == null) {
            instance = this;
        }
    }

	// Use this for initialization
    void Start() {
        Data.Instance.CheckData();
        Test();
        InitPlane();
	}
	
	// Update is called once per frame
	void Update () {
        if (Avatar.Instance.isLaunched && !Avatar.Instance.isLanded) {
            PopControllor.Instance.CreatePopList(Time.time);
            ReadPopList();
            DealKeyBoardControl();
        }

        CheckAirXLand();
	}

    void InitPlane() {
        SpriteRenderer sprite = Avatar.Instance.GetComponent<SpriteRenderer>();
        switch (Data.Instance.lvInafune) {
            case 0:
                sprite.sprite = ZeroSprites[0];//(Sprite)Resources.Load("Image/情书号");
                break;
            case 1:
                sprite.sprite = ZeroSprites[1];//(Sprite)Resources.Load("Image/搞死号");
                break;
            case 2:
                sprite.sprite = ZeroSprites[2];//(Sprite)Resources.Load("Image/惟楚号");
                break;
            default:
                Debug.Log("草泥马，飞船等级错误");
                break;
        }
    }

    //确认飞行结束
    void CheckAirXLand() {
        if (Avatar.Instance.isLanded&&Avatar.Instance.Speed.magnitude<Mathf.Epsilon&&!uiLevelEnd.activeInHierarchy){
            uiLevelEnd.SetActive(true);
        }
    }

    //读取列表信息并创建琐事单位
    void ReadPopList() {

        int[] list = PopControllor.Instance.getCurruntPop(Time.time);
        if (list!=null){
            string log = "";
            for (int i = 0; i < list.Length ; i++){
                log += list[i] + ",";
                //Debug.Log("时间/ID:" + Time.time + "/" + log);
                Instantiate(Resources.Load("Prefabs/"+Trifle.triflePrefabNames[list[i]]));
            }

        }
        //PopControllor.Instance.popList
    }

    void DealKeyBoardControl() {
        if (Input.GetKeyDown(KeyCode.Space)){
            Avatar.Instance.UseWriting();
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            Avatar.Instance.PullUp();
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            Avatar.Instance.PushDown();
        }
    }


    void Test() {
        //Debug.Log((-232.669-360) % 360); //取余对负数一样有效，但会保留符号
        //Debug.Log(Vector2.Angle(new Vector2(1, 0), new Vector2(1, 1)));
        //Data data = Data.Instance;
//         Debug.Log("帽子等级：" + data.lvBowlerHat);
//         GameObject obj = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/T_Glasses"));
//         obj.transform.position = new Vector3(3, 3, 0);
        //         Debug.Log((int)TrifleType.Headband);
        //         Debug.Log((int)TrifleType.BowlerHat);
        //         Debug.Log((int)TrifleType.Glasses);
        //         Debug.Log((int)TrifleType.Writing);
        //         Debug.Log((int)TrifleType.Max);
        // 
        //         Debug.Log("测试随机数：");
        //         System.Random rand = new System.Random();
        //         string str = "";
        //         for (int i = 0; i < 100;++i ) {
        //             str += rand.Next(3, 7) + ",";
        //         }
        //         Debug.Log(str);
        //         for (int d = 0; d < 10 ; d++) {
        //             PopControllor.Instance.CreatePopList(Time.time);
        //             Debug.Log("测试输出-随机生成的琐事列表");
        //             string str = "";
        //             for (int i = 0; i < PopControllor.Instance.popList.Length; ++i) {
        //                 for (int j = 0; j < PopControllor.Instance.popList[i].Length; ++j) {
        //                     str += PopControllor.Instance.popList[i][j].ToString("f2") + " ";
        //                 }
        //                 str += "\n";
        //             }
        //             Debug.Log(str);
        //       }
    }
}
