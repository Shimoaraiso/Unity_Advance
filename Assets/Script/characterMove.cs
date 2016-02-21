using UnityEngine;
using System.Collections;

public class characterMove : MonoBehaviour {

	//機体のパラメータ設定
	public float maxSpd;
	public float cornering;
	public float basePower;
	//オブジェクト格納
	public Rigidbody myRigid;
	public PhotonView myPV;
	public Camera myCam;

	void Start () {
		//
		if (!myPV.isMine) {
			myRigid.isKinematic = true;
			myCam.transform.gameObject.SetActive(false);
			Destroy(this);
		}
	}
	
	void Update () {
		//PC動作確認
		if (!Application.isMobilePlatform) {
			if (Input.GetKey(KeyCode.W)) {
				variableManage.movingYaxis = 1;
			} else if (Input.GetKey(KeyCode.S)) {
				variableManage.movingYaxis = -1;
			} else {
				variableManage.movingYaxis = 0;
			}
			if (Input.GetKey(KeyCode.A)) {
				variableManage.movingXaxis = -1;
			} else if (Input.GetKey(KeyCode.D)) {
				variableManage.movingXaxis = 1;
			} else {
				variableManage.movingXaxis = 0;
			}
		}
	}

	void FixedUpdate() {
		//移動処理
		if (variableManage.movingYaxis != 0) {
			if (myRigid.velocity.magnitude < maxSpd) {
				myRigid.AddForce(transform.TransformDirection(Vector3.forward) * basePower * 11f * variableManage.movingYaxis);
			} else {
				//myRigid.velocity = myRigid.velocity.normalized * maxSpd; 
			}
			//旋回処理
			if (myRigid.angularVelocity.magnitude < (myRigid.velocity.magnitude * 0.3f)) {
				myRigid.AddTorque(transform.TransformDirection(Vector3.up) * cornering * variableManage.movingXaxis * 90f);
			} else {
				myRigid.angularVelocity = (myRigid.velocity.magnitude * 0.3f) * myRigid.angularVelocity.normalized;
				//myRigid.angularVelocity = myRigid.angularVelocity;
			}
		}
		//Debug.Log("velo" + myRigid.velocity.magnitude);
		//Debug.Log("ang" + myRigid.angularVelocity.magnitude);

	}
}
