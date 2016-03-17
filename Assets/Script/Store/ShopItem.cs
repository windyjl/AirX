using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopItem : MonoBehaviour {
    //public Renderer rend;
    public int itemID;

    private GameObject imgBorder;
    private GameObject imgBorderAvailable;
    private GameObject imgBorderUnvailable;

    private RectTransform rectTrans;
    // Use this for initialization
    void Start() {
        rectTrans = transform as RectTransform;
        //rend = GetComponent<Renderer>();
        imgBorderUnvailable = (GameObject)Instantiate(Resources.Load("Prefabs/UI/BorderUnvailable"));
        imgBorderAvailable = (GameObject)Instantiate(Resources.Load("Prefabs/UI/BorderAvailable"));
        imgBorder = (GameObject)Instantiate(Resources.Load("Prefabs/UI/Border"));

        imgBorderUnvailable.transform.parent = transform;
        ((RectTransform)imgBorderUnvailable.transform).anchoredPosition = Vector2.zero;
        imgBorderAvailable.transform.parent = transform;
        ((RectTransform)imgBorderAvailable.transform).anchoredPosition = Vector2.zero;
        imgBorder.transform.parent = transform;
        ((RectTransform)imgBorder.transform).anchoredPosition = Vector2.zero;

        setUnvailable();
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

    public void setUnvailable() {
        imgBorderUnvailable.SetActive(true);
        imgBorderAvailable.SetActive(false);
    }

    public void setAvailable() {
        imgBorderUnvailable.SetActive(false);
        imgBorderAvailable.SetActive(true);
    }

}
