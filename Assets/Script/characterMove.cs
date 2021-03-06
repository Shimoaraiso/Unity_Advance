﻿using UnityEngine;
using System.Collections;

public class characterMove : MonoBehaviour {

	//機体のパラメータ設定
	public float maxSpd;
	public float cornering;
	public float basePower;
	public float maxHealth;
	//オブジェクト格納
	public Rigidbody myRigid;
	public PhotonView myPV;
	public Camera myCam;
	private GameObject hitObject;
	//撃破時
	private float revivalTimer;

	void Start() {
		//自分が読み込んだオブジェクトではない場合
		if (!myPV.isMine) {
			myRigid.isKinematic = true;
			myCam.transform.gameObject.SetActive(false);
			Destroy(this);
		}
		//ほか
		revivalTimer = 0f;
		variableManage.currentHealth = maxHealth;
	}

	void Update() {
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
				variableManage.movingXaxis = 1;
			} else if (Input.GetKey(KeyCode.D)) {
				variableManage.movingXaxis = -1;
			} else {
				variableManage.movingXaxis = 0;
			}
		}
		//被弾処理
		if (hitObject != null) {
			//スクリプトを取得
			mainShell hitShell = hitObject.GetComponent<mainShell>();
			//ダメージ
			variableManage.currentHealth -= hitShell.pow;
			if (variableManage.currentHealth < 0) {
				variableManage.currentHealth = 0;
			}
			//オブジェクトを空にする
			hitObject = null;
		}
		//HPが0になったとき
		if (variableManage.currentHealth == 0f) {
			revivalTimer += Time.deltaTime;
			variableManage.controlLock = true;
			if (revivalTimer > 10.0f) {
				revivalTimer = 0f;
				variableManage.controlLock = false;
				variableManage.currentHealth = maxHealth;
				transform.position = myRespawnPos();
			}
		}
		//姿勢制御
		float xAngle = transform.rotation.eulerAngles.x;
		float zAngle = transform.rotation.eulerAngles.z;
		if (xAngle > 180f) {
			xAngle = xAngle - 360f;
		}
		if (zAngle > 180f) {
			zAngle = zAngle - 360f;
		}
		if (xAngle > 30f) {
			xAngle = 30f;
		} else if (xAngle < -30f) {
			xAngle = -30f;
		}
		if (zAngle > 30f) {
			zAngle = 30f;
		} else if (zAngle < -30f) {
			zAngle = -30f;
		}
		transform.rotation =
			Quaternion.Euler(
				new Vector3(xAngle, transform.rotation.eulerAngles.y, zAngle)
			);
		//
	}

	void FixedUpdate() {
		if (!variableManage.controlLock) {
			//移動処理
			if (variableManage.movingYaxis != 0) {
				if (myRigid.velocity.magnitude < maxSpd) {
					myRigid.AddForce(transform.TransformDirection(Vector3.forward) * basePower * 11f * variableManage.movingYaxis);
				}
				//旋回処理
				if (myRigid.angularVelocity.magnitude < (myRigid.velocity.magnitude * 0.3f)) {
					myRigid.AddTorque(transform.TransformDirection(Vector3.up) * cornering * variableManage.movingXaxis * -90f);
				} else {
					myRigid.angularVelocity = (myRigid.velocity.magnitude * 0.3f) * myRigid.angularVelocity.normalized;
				}
			}
		}
		//姿勢制御
		Vector3 raycastStartPos =
			new Vector3(transform.position.x, (transform.position.y + 1.0f), transform.position.z);
		RaycastHit rhit;
		if (!Physics.Raycast(raycastStartPos, transform.TransformDirection(-Vector3.up), out rhit, 3.0f)) {
			//地表に接していなければ下方向に力を加える
			myRigid.AddForce(Vector3.up * -50f);
		}
		//
	}

	//衝突時に呼ばれる
	void OnCollisionEnter(Collision col) {
		//bulletレイヤーに処理を限
		if (col.gameObject.layer == 9) {
			hitObject = col.gameObject;
		}
	}

	// 再出撃時の復帰位置を計算
	Vector3 myRespawnPos() {
		Vector2 rndPos = Vector2.zero;
		while (true) {
			rndPos = Random.insideUnitCircle * 150f;
			if (rndPos.x < -20f) {
				if (rndPos.y > 20f) {
					break;
				}
			}
		}
		Vector3 returnPos = new Vector3((592f + rndPos.x), 0f, (-592f + rndPos.y));
		if (variableManage.myTeamID == 2) {
			returnPos *= -1.0f;
		}
		returnPos = new Vector3(returnPos.x, 24f, returnPos.z);

		return returnPos;
	}
}
