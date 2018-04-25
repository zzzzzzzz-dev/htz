using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainController : MonoBehaviour {

	//public prefab
	public GameObject htControllerPrefab;

	//public


	//private
	//local
	int htcnt;



	// Use this for initialization
	void Start () {
	
		//local
		htcnt = 0;
		this.setHt ();

	}


	// Update is called once per frame
	void Update () {
		if (htcnt == 250) {
			htcnt = 0;
			this.setHt ();
		}
		htcnt++;

	}

	//private
	private void setHt(){
		//generate bullet
		GameObject go = Instantiate (htControllerPrefab) as GameObject;
		float sx = Random.Range (-2.0f, 2.0f);
		float sy = Random.Range (-1.0f, 1.0f);
		go.GetComponent<htController> ().setInitState (sx, sy);

	}

}
