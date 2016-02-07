using UnityEngine;
using System.Collections;

public class manage : MonoBehaviour {

	private bool keyLock;
	public GameObject a;

	void Start() {
		keyLock = false;
		PhotonNetwork.ConnectUsingSettings(null);

		PhotonNetwork.sendRate = 30;
	}

	void OnJoinedLobby() {
		PhotonNetwork.JoinRandomRoom();
	}

	void OnJoinedRoom() {
		Debug.Log("ルームへ入室しました。");
		keyLock = true;
	}

	void OnPhotonRandomJoinFailed() {
		Debug.Log("ルームの入室に失敗しました。");
		PhotonNetwork.CreateRoom(null);
	}

	void FixedUpdate() {
		Debug.Log(keyLock);
		if (Input.GetMouseButtonDown(0) && keyLock) {
			//GameObject mySyncObj = Instantiate(a);
			GameObject mySyncObj = PhotonNetwork.Instantiate("Cube", new Vector3(9.0f, 0f, 0f), Quaternion.identity, 0);
			Rigidbody mySyncObjRB = mySyncObj.GetComponent<Rigidbody>();
			mySyncObjRB.isKinematic = false;
			float rndPow = Random.Range(1.0f, 5.0f);
			mySyncObjRB.AddForce(Vector3.left * rndPow, ForceMode.Impulse);
		}
	}
}
