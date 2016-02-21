using UnityEngine;
using System.Collections;

public class gameManage : MonoBehaviour {

	void Start() {
		//初期設定
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		//Photon Realtimeのサーバーへ接続(ロビーへ入室)
		PhotonNetwork.ConnectUsingSettings(null);
	}

	void Update() {

	}

	//ロビーへ入室完了
	void OnJoinedLobby() {
		//どこかのルームへ入室
		PhotonNetwork.JoinRandomRoom();
	}

	//ロビーへの入室が失敗
	void OnFailedToConnectToPhoton() {
		Application.LoadLevel("MainMenu");
	}

	//ルームへ入室失敗
	void OnPhotonRandomJoinFailed() {
		//自分でルームを作成
		PhotonNetwork.CreateRoom(null);
	}

	//無事にルームへ入室
	void OnJoinedRoom() {
		//オブジェクトを読み込み
		GameObject myPlayer = PhotonNetwork.Instantiate("character/t01", new Vector3(440f, 30f, -560f), Quaternion.identity, 0);
	}

	//Photon Realtimeとの接続が切断された場合
	void OnConnectionFail() {
		Application.LoadLevel("MainMenu");
	}

	void OnAppricationPause(bool pauseStatus) {
		if (pauseStatus) {
			PhotonNetwork.Disconnect();
		} else {
			Application.LoadLevel("MainMenu");
		}
	}
}
