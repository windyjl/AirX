using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	// Use this for initialization
	void Start () {
        Test();
	}
	
	// Update is called once per frame
	void Update () {

        if (Avatar.Instance.isLaunched) {
            PopControllor.Instance.CreatePopList(Time.time);
            ReadPopList();
            DealKeyBoardControl();
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
                Instantiate(Resources.Load("prefabs/"+Trifle.triflePrefabNames[list[i]]));
            }

        }
        //PopControllor.Instance.popList
    }

    void DealKeyBoardControl() {
        if (Input.GetKeyDown(KeyCode.Space)){
            Avatar.Instance.UseWriting();
        }
    }

    void Test() {
        Data data = Data.Instance;
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
