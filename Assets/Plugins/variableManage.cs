using UnityEngine;
using System.Collections;

public class variableManage : MonoBehaviour {


	static public int movingYaxis;
	static public int movingXaxis;

	static public bool fireWeapon;
	static public GameObject lockonTarget;
	static public bool lockoned;

	static public float currentHealth;

	// 勝敗用変数
	static public bool finishedGame;// 勝敗が確定されたか
	static public int team1Rest;    // チーム1の残り撃破数
	static public int team2Rest;    // チーム2の残り撃破数
	static public float base1Rest;  // チーム1の拠点の残りHP
	static public float base2Rest;  // チーム2の拠点の残りHP
	static public float timeRest;   // ゲームの残り時間
	static public int gameResult;   // 1-teamID=1の勝利、2-teamID=2の勝利

	static public int myTeamID;
	static public bool controlLock;


	void Start() {

		initializeVariable();
	}

	static public void initializeVariable() {
		movingXaxis = 0;
		movingYaxis = 0;
		fireWeapon = false;
		lockoned = false;
		controlLock = false;
		myTeamID = 0;
		currentHealth = 10f;
		// 勝敗用
		finishedGame = false;
		team1Rest = 20;
		team2Rest = 20;
		base1Rest = 9999f;
		base2Rest = 9999f;
		timeRest = 400f;
		gameResult = 0;
	}

	void Update() {

	}
}
