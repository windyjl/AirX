using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hirari : MonoBehaviour {
    private float time0;
    private Text txt;
    private bool isFadein = false;
	// Use this for initialization
	void Start () {
        time0 = Time.timeSinceLevelLoad;
        txt = gameObject.GetComponent<Text>();
	}

    // Update is called once per frame
    void Update() {
        if(Time.time - time0>1){
            time0 = Time.time;
            if (isFadein) {
                txt.CrossFadeAlpha(1, 1, false);
                isFadein = false;
            } else {
                txt.CrossFadeAlpha(0, 1, false);
                isFadein = true;
            }
        }

	}
}
