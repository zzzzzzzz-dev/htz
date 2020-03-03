using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {

	//private
	//cash
	Transform cashTransform;

	//local
	int hp;
	int state;

	// Use this for initialization
	void Start () {

		//cash
		cashTransform = transform;

		//local
		hp = Random.Range( 40, 80 );
		state = 0;
		
	}
	
	// Update is called once per frame
	void Update () {


		//always process

	}


	//public
	//collision
	public void OnTriggerEnter(Collider coll) {
		if (hp <= 0) {
			return;
		}
		//bullet hit
		if (coll.tag == "bl") {
			this.hitProcess ();
		}
	}


	//private
	private void hitProcess(){
		hp = hp - 1;
		if (hp <= 0) {
			state = 1;
//			//tag
//			this.tag = "unavailable";
		}
	}


	//public
	//get status
	public int getState(){
		return state;
	}

}
#hello-world

Hi

my name is zainab
my old is 16
i love everyone
i'm study
