using UnityEngine;
using System.Collections;

public class GamesceneUI : MonoBehaviour
{
    private Avatar avatar;
	// Use this for initialization
    void Start()
    {
        avatar = GameObject.Find("avatar").GetComponent<Avatar>();
	}
	
	// Update is called once per frame
    void Update()
    {
	}
    void OnGUI()
    {
        //GUILayout.BeginHorizontal("box");
        //GUILayout.Label("にまび");
        //test = GUILayout.TextArea(test);
        //GUILayout.TextField("终于好用了");
        //if (GUILayout.Button("欧耶"))
        //{
        //    print("you input " + test);
        //}
        //GUILayout.EndHorizontal();
        //GUI.Label(new Rect(0,0,160,90),"にまび");
        GUILayout.BeginHorizontal("box");
        if (GUILayout.Button("回到开始界面"))
        {
            Application.LoadLevel("Start");
            //print("you input " + test);
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginVertical("box");
        if (GUILayout.Button("重试"))
        {
            //DontDestroyOnLoad(refMain);
            Application.LoadLevel("Game");
        }
//         GUILayout.BeginHorizontal();
//         GUILayout.Label("SpeedX");
//         //SpeedX = int.Parse(GUILayout.TextArea("" + SpeedX));
//         GUILayout.EndHorizontal();
//         GUILayout.BeginHorizontal();
//         GUILayout.Label("SpeedY");
//         //SpeedY = int.Parse(GUILayout.TextArea("" + SpeedY));
//         GUILayout.EndHorizontal();
        if (GUILayout.Button("发射"))
        {
            avatar.Launch();
            //refMain.Launch(SpeedX, SpeedY);
        }
        if (GUILayout.Button("清除速度"))
        {
            //PlayerPrefs.DeleteAll();
            Avatar.Instance.Speed = Vector3.zero;
        }
		/* 协线程测试
        if (GUILayout.Button("测试Coroutine"))
        {
			StartCoroutine(refMain.loadItem());
		}
		if (GUILayout.Button("测试直接生成"))
		{
			refMain.loadItemBad();
		}
		*/
        //Vector3 pos = refMain.player.transform.position;
        GUILayout.Label("道符：" + avatar.nWriting);
        GUILayout.Label("空格使用道符");
        GUILayout.Label("←→ 调整角度");
        //GUILayout.Label("角色逻辑坐标 (" + pos.x.ToString("f2") + "," + pos.y.ToString("f2") + ")");
        //GUILayout.Label("击杀数(空格挥剑):" + KillEnemyCount + "");
        //pos = GameObject.Find("di").transform.position;
        //GUILayout.Label("参照物坐标 (" + pos.x.ToString("f2") + "," + pos.y.ToString("f2") + ")");
        GUILayout.EndVertical();
//         GameObject obj = GameObject.Find("background"); 获取sprite对象长宽，测试代码。测试失败
//         SpriteRenderer spr = obj.GetComponent<SpriteRenderer>();
//         if (spr!=null)
//         {
//             print(spr.sprite.rect.width);
//         }
    }
    void OnKillEnemy()
    {
        //++KillEnemyCount;
    }
}
