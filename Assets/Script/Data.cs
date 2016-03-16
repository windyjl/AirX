using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;


class Data :MonoBehaviour {
    private static readonly Data instance = new Data();
    public static Data Instance {  get  {  return instance;   } }
    //显示参数
    public static float SCREEN_WIDTH = 12.8f;
    public static float SCREEN_HEIGHT = 7.2f;
    public Dictionary<string,int> setting = new Dictionary<string,int>();
    //数值们
    private int money = 0;
    public int lvStarMult   = 0;//星星倍率
    public int lvHeadband   = 0;//发带升级数据（出现频率，提升数值）
    public int lvBowlerHat  = 4;//礼帽升级数据（出现频率，提升无敌时间）
    public int lvGlasses    = 0;//眼镜升级数据（出现频率，提升数值）
    public int lvWriting = 0;//道符升级数据（出现频率，提升数值）
    public int lvInafune = 0;//飞船等级
    public int lvResistance = 0;
    //道具等级对应的数据
    private int[] arrValueStar = { 5,10,15,20,25 };  //星星的出现数量
    private float[] arrHeadband = { 10, 20, 30, 40, 50 };  //发带升级数据
    private float[] arrBowlerHat = { 2, 2.5f, 3, 3.5f, 4 };  //无敌时间长度
    private float[] arrGlasses = { 10, 20, 30, 40, 50 };  //眼镜升级数据
    private Vector3[] arrInafune = { new Vector3(10,5), new Vector3(20,10), new Vector3(30,15)};  //飞船等级
    private int[] arrWriting = { 1, 2, 3, 4, 5 }; //初始道符数量
    //场景中琐事道具出现的概率
    private int[] popRateStar = { 45, 55 };  //星星的出现数量
    private int[] popRateHeadband = { 3,7 };  //发带的出现数量
    private int[] popRateBowlerHat = {0,1};  //圆顶帽的出现数量
    private int[] popRateGlasses = { 3,7 };  //眼镜的出现数量
    private int[] arrRateWriting = { 3, 7 }; //道符出现数量
    //飞行环境因素
    private ArrayList arrFlightArgu;
	public float fGravity;
	public float[] aResistance;
	public float groundResistance = 10;
    // public int lvGlasses    = 0;
    // public int lvGlasses    = 0;
    // public int lvGlasses    = 0;
    // public int lvGlasses    = 0;

    //不同平台下StreamingAssets的路径是不同的，这里需要注意一下。  
//     public static readonly string PathURL =
// #if UNITY_ANDROID   //安卓  
//     "jar:file://" + Application.dataPath + "!/assets/";  
// #elif UNITY_IPHONE  //iPhone  
//     Application.dataPath + "/Raw/";  
// #elif UNITY_STANDALONE_WIN || UNITY_EDITOR  //windows平台和web平台
//  "file://" + Application.dataPath + "/StreamingAssets/";
// #else  
//         string.Empty;  
// #endif  

    
    private Data() {
    }
    public void CheckData() {
        arrFlightArgu = LoadFile(Main.sPath + "/Resources/Config", "FlightArgument.txt");
        for (int i = arrFlightArgu.Count - 1; i >= 0; --i) {
            string[] astr = arrFlightArgu[i].ToString().Split(':');
            if (astr[0].Equals("重力")){
                fGravity = float.Parse( astr[1]);
            } else if (astr[0].Equals("阻力")) {
                aResistance = SplitStringToFloat(astr[1],',');
            } else {
                Debug.Log("该行数据为找到配对的存储对象:" + arrFlightArgu[i]);
            }
            arrFlightArgu.RemoveAt(i);
        }
    }
    public float[] SplitStringToFloat(string str,char c){
        string[] arr = str.Split(c);
        float[] outArr = new float[arr.Length];
        for (int i=0;i<arr.Length;++i){
            outArr[i] = float.Parse(arr[i]);
        }
        return outArr;
    }
    /** 
     * 读取文本文件 
     * path：读取文件的路径 
     * name：读取文件的名称 
     */
    ArrayList LoadFile(string path, string name) {
        //使用流的形式读取  
        StreamReader sr = null;
        try {
            sr = File.OpenText(path + "//" + name);
        } catch (Exception e) {
            //路径与名称未找到文件则直接返回空  
            return null;
        }
        string line;
        ArrayList arrlist = new ArrayList();
        while ((line = sr.ReadLine()) != null) {
            //一行一行的读取  
            //将每一行的内容存入数组链表容器中  
            arrlist.Add(line);
        }
        //关闭流  
        sr.Close();
        //销毁流  
        sr.Dispose();
        //将数组链表容器返回  
        return arrlist;
    }

    //获得飞船初速度
    public Vector3 getLaunchSpeed() {
        return arrInafune[lvInafune];
    }

    //获得梦中模式时间
    public float getDreamModeTime() {
        return arrBowlerHat[lvBowlerHat];
    }
    //获得圆顶帽出现频率
    public int[] getBowlerHatPopNum() {
        return popRateBowlerHat;
    }

    //获得星星价值
    public float getStarValue() {
        return arrValueStar[lvStarMult];
    }

    //获得星星出现频率
    public int[] getStarPopNum() {
        return popRateStar;
    }

    //获得发带模式时间
    public float getHeadBandValue() {
        return arrHeadband[lvHeadband];
    }
    //获得发带出现频率
    public int[] getHeadBandPopNum() {
        return popRateHeadband;
    }

    //获得眼镜模式时间
    public float getGlassesValue() {
        return arrGlasses[lvGlasses];
    }
    //获得眼镜出现频率
    public int[] getGlassesPopNum() {
        return popRateGlasses;
    }

    //获得道符出现频率
    public int[] getWritingPopNum() {
        return arrRateWriting;
    }


    //获得道符初始数量
    public int getWritingValue() {
        return arrWriting[lvWriting];
    }

    //获得阻力
    public float getResistance() {
        return aResistance[lvResistance];
    }

    //金币翻倍
    public void setMoneyMultUp() {
        lvStarMult++;
        if (lvStarMult>=arrValueStar.Length){
            lvStarMult = arrValueStar.Length - 1;
        }
    }
    public void resetMoneyMult() {
        lvStarMult = 0;
    }

    //碰到星星，赚钱
    public void touchStarGetMoney() {
        money += arrValueStar[lvStarMult];
    }

    //getMoney
    public int getMoney() {
        return money;
    }
}
