using UnityEngine;
using System.Collections;

public class GravitySystemEmulator : MonoBehaviour {
    public Vector3 Speed;
    public Vector3 logicPosition;
    private float SpeedRate=3;
    private float Radius = 3;
    private float GravityDonotEdit = 3;
	// Use this for initialization
	void Start () {
        Speed = Vector3.up * SpeedRate;
        Radius = GravityDonotEdit / Mathf.Pow(SpeedRate, 2);
        logicPosition = new Vector3(Radius, 0, 0);
        //GravityDonotEdit = Radius * SpeedRate * SpeedRate;
	}
	
	// Update is called once per frame
    void Update() {
        DealMove();
        DealSpeed();
        transform.position = logicPosition;

        if (Input.GetKeyDown(KeyCode.Space)){
            Vector3 acc = new Vector3();
            Vector3 dir = (Vector3.zero - logicPosition);
            float theta = -Mathf.PI/2;
            acc.x = Speed.normalized.x * Mathf.Cos(theta) + Speed.normalized.y*Mathf.Sin(theta);
            acc.y = Speed.normalized.y * Mathf.Cos(theta) - Speed.normalized.x*Mathf.Sin(theta);
            Speed += acc*SpeedRate;
        }
	}

    void DealSpeed() {
        Vector3 dir = (Vector3.zero - logicPosition);
        dir.Normalize();
        Speed += GravityDonotEdit/Mathf.Pow(Radius,2) * dir * Time.deltaTime;
    }

    void DealMove() {
        logicPosition += Speed * Time.deltaTime;
    }
}
