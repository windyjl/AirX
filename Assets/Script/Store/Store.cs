using UnityEngine;
using System.Collections;

public class Store : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void buyStar() {
    	Data.Instance.lvStarMult ++;
    	if(Data.Instance.lvStarMult>=arrValueStar.length){
    		Data.Instance.lvStarMult=arrValueStar.length-1;
    	}
    }

    public void buyHeadband() {
    	Data.Instance.lvHeadband ++;
    	if(Data.Instance.lvHeadband>=arrHeadband.length){
    		Data.Instance.lvHeadband=arrHeadband.length-1;
    	}
    }

    public void buyBowlerHat() {
    	Data.Instance.lvBowlerHat ++;
    	if(Data.Instance.lvBowlerHat>=arrBowlerHat.length){
    		Data.Instance.lvBowlerHat=arrBowlerHat.length-1;
    	}
    }

    public void buyGlasses() {
    	Data.Instance.lvGlasses ++;
    	if(Data.Instance.lvGlasses>=arrGlasses.length){
    		Data.Instance.lvGlasses=arrGlasses.length-1;
    	}
    }

    public void buyWriting() {
    	Data.Instance.lvWriting ++;
    	if(Data.Instance.lvWriting>=arrWriting.length){
    		Data.Instance.lvWriting=arrWriting.length-1;
    	}
    }

}
