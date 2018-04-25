using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour {

	//public
	//public prefab
	public GameObject bulletControllerPrefab;

	//private

	//cash
	//transform
	Transform cashTransform;

	//targetmark
	GameObject targetMark;
	Text targetMarkTxt;
	RectTransform cashRectTransform_targetMarkTxt;

	//local
	int stcnt;

	// Use this for initialization
	void Start () {

		//cash
		cashTransform = transform;

		//target mark
		targetMark = GameObject.Find("targetMark");
		targetMarkTxt = targetMark.GetComponent<Text> ();
		cashRectTransform_targetMarkTxt = targetMarkTxt.GetComponent<RectTransform> ();

		//local
		stcnt = 0;
		
	}
	
	// Update is called once per frame
	void Update () {

		//atack
		if (Input.GetMouseButton (0)) {
			Vector2 mpos = Input.mousePosition;

			//target mark disp
			cashRectTransform_targetMarkTxt.position = mpos;

			//shot
			stcnt++;
			if (stcnt % 6 == 0) {
				//bullet setting and generate
				setAtack( mpos, 0 );

			}
		} else {
			stcnt = 0;
		}

	}

	//private

	//set atack
	private void setAtack( Vector2 mpos, int bulletType ){
		float xdistance, ydistance, zdistance, xzdistance;
		float direction,direction2;
		Vector3 ppos;
		//tap pos to world pos
		Ray ray = Camera.main.ScreenPointToRay (mpos);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Mathf.Infinity) == true) {
//			//target mark disp
//			cashRectTransform_targetMarkTxt.position = mpos;
			//atack dir
			//direction to tap pos from player
			//get to tap pos dir
			ppos.x = 0.5f;
			ppos.y = 1.5f;
			ppos.z = -8.0f;
			xdistance = (hit.point.x) - (ppos.x);	//tap pos,player x distance
			ydistance = (hit.point.y) - (ppos.y);	//tap pos,player y distance
			zdistance = (hit.point.z) - (ppos.z);	//tap pos,player z distance
			xzdistance = Mathf.Sqrt( Mathf.Pow( xdistance,2 ) + Mathf.Pow( zdistance,2 ) );	//tap pos, player xz distance
			if (xdistance == 0) {	//for zero exception
				xdistance = 0.0001f;
			}
			if (ydistance == 0) {	//for zero exception
				ydistance = 0.0001f;
			}
			direction = Mathf.Atan2 (zdistance, xdistance) * Mathf.Rad2Deg;	//distance -> direction
			direction2 = Mathf.Atan2 (xzdistance, ydistance) * Mathf.Rad2Deg;	//distance -> direction
			float atcdir = direction;
			if (atcdir < 0) {
				atcdir = atcdir + 360.0f;
			}
			float atcdir2 = direction2;
			if (atcdir2 < 0) {
				atcdir2 = atcdir2 + 360.0f;
			}
			//get x,y,z speed to tap pos
			float pbspd = 0.35f;
			if (bulletType == 2) {	//large bullet tachio
				pbspd = 0.14f;
			}
			if (bulletType == 3) {	//normal bullet pants
				pbspd = 0.20f;
			}
			float xx = Mathf.Cos( atcdir*Mathf.Deg2Rad ) * pbspd;
			float yy = Mathf.Cos( atcdir2*Mathf.Deg2Rad ) * pbspd;
			float zz = Mathf.Sin( atcdir*Mathf.Deg2Rad ) * pbspd;
			//bullet pos init
			Vector3 bpos = ppos;
			float initadj = 2.0f;
			if (bulletType == 0) {	//normal bullet
				initadj = initadj + 0.0f;
			}
			if (bulletType == 1) {	//large bullet
				initadj = initadj + 0.0f;
			}
			if (bulletType == 2) {	//large bullet tachio
				//				initadj = initadj - 1.5f;
				initadj = initadj - 0.0f;
			}
			if (bulletType == 3) {	//normal bullet pants
				initadj = initadj + 0.0f;
			}
			bpos.x = bpos.x + (xx * initadj);
			bpos.y = bpos.y + (yy * initadj);
			bpos.z = bpos.z + (zz * initadj);
			//generate player bullet
			//generate bullet
			GameObject go = Instantiate (bulletControllerPrefab) as GameObject;
			go.GetComponent<bulletController> ().setInitState (xx, yy, zz);
		}
	}


}
