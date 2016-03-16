using UnityEngine;
using System.Collections;

public class UI_LevelEnd : MonoBehaviour {
    private float aniDuration = 1f;
    private float aniCount = 0f;
    private bool isAniOver = false;
    private Vector2 posStart = new Vector2(Data.SCREEN_WIDTH*100, 0);
    private RectTransform rectTrans;
    private Canvas canvas;
	// Use this for initialization
	void Start () {
        rectTrans = transform as RectTransform;
        rectTrans.anchoredPosition = posStart;

        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        //GetComponent<Plane>()
	}
	
	// Update is called once per frame
    void Update() {
        if (!isAniOver) {
            aniCount += Time.deltaTime;
            rectTrans.anchoredPosition = Vector2.Lerp(posStart, Vector2.zero, aniCount / aniDuration);
        }


        // ↓从雨松处借鉴的代码，用于理解屏幕坐标
        //Vector2 pos;
        //         if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos)) {
        //         //if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, new Vector3(Data.SCREEN_WIDTH/0.02f,Data.SCREEN_HEIGHT/0.02f), canvas.worldCamera, out pos)) {
        //             rectTrans.anchoredPosition = pos;
        //        	}
    }
}
