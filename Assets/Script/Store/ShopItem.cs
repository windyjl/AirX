using UnityEngine;
using System.Collections;

public class ShopItem : MonoBehaviour {
    //public Renderer rend;
    public int itemID;
    // Use this for initialization
    void Start() {
        //rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update() {

    }

    //     void OnMouseEnter() {
    //         rend.material.color = Color.red;
    //     }
    //     void OnMouseOver() {
    //         rend.material.color -= new Color(0.1F, 0, 0) * Time.deltaTime;
    //     }
    //     void OnMouseExit() {
    //         rend.material.color = Color.white;
    //    }

    void OnPointerEnter(){
        Debug.Log("啊！~进来了");
    }
    void OnPointerExit() {
        Debug.Log("不要拔出去");
    }

}
