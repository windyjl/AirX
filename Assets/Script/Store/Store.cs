using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class Store : MonoBehaviour {
    public Text ItemTips;
    public Text ItemPrice;
    public GameObject[] arrShopItem;
    private RectTransform tranItemTips;
    private int IDItemSelecting = -1;
	// Use this for initialization
	void Start () {
        //tranItemTips = ItemTips.transform as RectTransform;
        Data.Instance.CheckData();
        //注册事件
        regEvent();
	}
	
	// Update is called once per frame
	void Update () {
	}

    //初始化商品
//     void InitShopItem() {
//         ShopItem[0]
//     }

    //注册事件
    void regEvent() {
        for (int i = 0; i < arrShopItem.Length; i++) {
            EventTriggerListener.Get(arrShopItem[i]/*.GetComponent<Button>()*/).onClick = OnButtonClick;
        }
    }

    public void GotoNextRun() {
        Application.LoadLevel("Game");
    }

    private void OnButtonClick(GameObject go) {
        //Debug.Log("各位能写句完整的吗?");
        int id = go.GetComponent<ShopItem>().itemID;
        ItemData data = Data.Instance.arrItemData[id - 1];
        IDItemSelecting = id;
        ItemTips.text = data.itemDescription;
        ItemPrice.text = "$"+data.arrPrice[Data.Instance.lvBowlerHat];
        clearOutLine();
        go.GetComponent<ShopItem>().setAvailable();
    }

    void clearOutLine() {
        for (int i = 0; i < arrShopItem.Length; i++) {
            arrShopItem[i].GetComponent<ShopItem>().setUnvailable();
        }
    }

    public void buyStar() {
    	Data.Instance.lvStarMult ++;
    	if(Data.Instance.lvStarMult>=Data.Instance.arrValueStar.Length){
    		Data.Instance.lvStarMult=Data.Instance.arrValueStar.Length-1;
    	}
    }

    public void buyHeadband() {
    	Data.Instance.lvHeadband ++;
    	if(Data.Instance.lvHeadband>=Data.Instance.arrHeadband.Length){
    		Data.Instance.lvHeadband=Data.Instance.arrHeadband.Length-1;
    	}
    }

    public void buyBowlerHat() {
    	Data.Instance.lvBowlerHat ++;
    	if(Data.Instance.lvBowlerHat>=Data.Instance.arrBowlerHat.Length){
    		Data.Instance.lvBowlerHat=Data.Instance.arrBowlerHat.Length-1;
    	}
    }

    public void buyGlasses() {
    	Data.Instance.lvGlasses ++;
    	if(Data.Instance.lvGlasses>=Data.Instance.arrGlasses.Length){
    		Data.Instance.lvGlasses=Data.Instance.arrGlasses.Length-1;
    	}
    }

    public void buyWriting() {
    	Data.Instance.lvWriting ++;
    	if(Data.Instance.lvWriting>=Data.Instance.arrWriting.Length){
    		Data.Instance.lvWriting=Data.Instance.arrWriting.Length-1;
    	}
    }

}
