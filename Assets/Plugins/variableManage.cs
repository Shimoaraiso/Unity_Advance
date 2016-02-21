using UnityEngine;
using System.Collections;

public class variableManage : MonoBehaviour {


	static public int movingYaxis;
	static public int movingXaxis;

	static public bool fireWeapon;
	static public GameObject lockonTarget;
	static public bool lockoned;

	void Start() {

		initializeVariable();
	}

	static public void initializeVariable() {
		movingXaxis = 0;
		movingYaxis = 0;
		fireWeapon = false;
		lockoned = false;
	}

	void Update() {

	}
}
