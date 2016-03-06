using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    private float camera_off_x = 3.88f;  //原定X黄金分割点，Y中心。X偏离极限388
    private static float camera_maxoff_x = 3.88f;   //X偏离极限388
    private static float camera_maxoff_y = 2.6f;    //Y偏离极限260
    public Vector2 nCSpeedBase = new Vector2(0,25); //处于该值和该值以内时，镜头不向边框拉伸
    public Vector2 nCSpeedOverflow = new Vector2(10, 5);  //基于该值，超过基础速度的值每达到该值一倍，边框距离取半
    public float followRateX = 35f;
    private static float followRateY = 14f;
	// Use this for initialization
    void Start() {
        camera_off_x = Data.SCREEN_WIDTH * (0.8f - 0.5f);//0.618太靠中心
        //         camera_off_y -= Data.SCREEN_HEIGHT * (0.5f - 0.618f	}
    }

	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate() {
        TrackAvatar();
    }
    /*
     * 使用星尘图采用的算法，与边框距离去0.5的n次方
     * 边框距离camera off
     * 
     */
    void TrackAvatar() {
        Vector3 targetPos = Avatar.Instance.transform.position;
        targetPos.z = Camera.main.transform.position.z;
        float rateX, rateY,offx=1,offy=1;
        rateX = Mathf.Max(0,Avatar.Instance.Speed.x - nCSpeedBase.x) / nCSpeedOverflow.x;
        rateY = Mathf.Max(0,Avatar.Instance.Speed.y - nCSpeedBase.y) / nCSpeedOverflow.y;
        if (Avatar.Instance.Speed.y<0){
            offy = -1;
        }
        offx *= (1 - Mathf.Pow(0.5f, rateX)) * camera_maxoff_x;
        offy *= (1 - Mathf.Pow(0.5f, rateY)) * camera_maxoff_y;
        targetPos += new Vector3(offx, offy, 0);
        //targetPos.x = Mathf.Lerp(Camera.main.transform.position.x, targetPos.x, followRateX * Time.deltaTime);
        targetPos.y = Mathf.Lerp(Camera.main.transform.position.y, targetPos.y, followRateY * Time.deltaTime);
        //固定屏幕1/5位置的锁定，不参与缓冲
        targetPos.x += camera_off_x;
        //定位镜头
        Camera.main.transform.position = targetPos;
    }
}
