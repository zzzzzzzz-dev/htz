using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class htController : MonoBehaviour {


	//private

	//cash
	Transform cashTransform;
	GameObject zombie;
	Animator zombie_anm;
	Transform zombieTransform;
	ZombieController ZombieCtr;

	//local
	int seqcnt;
	int actcnt;

	float sx,sz;


	// Use this for initialization
	void Start () {

		//private

		//cash
		cashTransform = transform;

		zombie = cashTransform.FindChild ("Zombie").gameObject;
		ZombieCtr = zombie.GetComponent<ZombieController> (); 
		zombie_anm = zombie.GetComponent<Animator> ();
		zombieTransform = zombie.transform;

		//local
		seqcnt = 0;
		actcnt = 0;

		//locate
		cashTransform.position = new Vector3( sx, -1.8f, sz );
	}


	// Update is called once per frame
	void Update () {

		switch (seqcnt) {
		case 0:
			//wake up
			actcnt++;
			if (actcnt >= 3) {
				actcnt = 0;
				Vector3 pos = cashTransform.position;
				pos.y = pos.y + 0.03f;
				if (pos.y >= 0) {
					pos.y = 0;
					seqcnt = 1;
					actcnt = 0;
					//start walk
					zombie_anm.SetTrigger("startWalk");
				}
				cashTransform.position = pos;
			}
			break;
		case 1:
			//walk
			if (actcnt == 0) {
				zombie_anm.speed = Random.Range (0.8f, 3.0f);
			}
			actcnt++;
//			int tmp = ZombieCtr.getState ();
			int tmp = 0;
			if (zombieTransform.position.z <= -6.0f) {
				tmp = 1;
			}
			if (tmp == 1) {	//near player?
				seqcnt = 2;
				actcnt = 0;
				//start walk
				zombie_anm.SetTrigger("startAtack");
			}
			break;
		case 2:
			//atack
			if (actcnt == 0) {
				zombie_anm.speed = 1.0f;
			}
			actcnt++;
			break;
		case 3:
			//down
			if (actcnt == 0) {
				zombie_anm.speed = 1.0f;
			}
			actcnt++;
			if (actcnt == 500) {
				GameObject.Destroy (gameObject);
			}
			break;
		default:
			break;
		}

		//move
		if ( (seqcnt != 0) && (seqcnt != 3) ) {
			if (cashTransform.position.x > 0) {
				cashTransform.Translate (new Vector3 (0.0025f, 0, 0));
			}
			if (cashTransform.position.x < 0) {
				cashTransform.Translate (new Vector3 (-0.0025f, 0, 0));
			}
		}

		//down check
		if (ZombieCtr.getState () == 1) {
			if ( (seqcnt >= 1) && (seqcnt != 3) ) {
				seqcnt = 3;
				actcnt = 0;
				//down
				zombie_anm.SetTrigger("down");
				this.tag = "unavailable";
				zombie.tag = "unavailable";
			}
		}

	}

	//public
	//set init status
	public void setInitState( float x, float z ){
		sx = x;
		sz = z;
	}

}
