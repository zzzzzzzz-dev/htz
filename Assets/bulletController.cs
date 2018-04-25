using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour {


	//private

	//cash
	Transform cashTransform;

	//particles
	GameObject hitParticles;
	ParticleSystem cashParticleSystem_hitParticles;

	//local
	float xx;
	float yy;
	float zz;

	int mode;
	int pcnt;


	// Use this for initialization
	void Start () {

		//cash
		cashTransform = transform;

		//particles
		hitParticles = cashTransform.FindChild ("bulletHitParticle").gameObject;
		cashParticleSystem_hitParticles = hitParticles.GetComponent<ParticleSystem> ();
		cashParticleSystem_hitParticles.Stop ();

		//local

		//test
//		xx = -0.01f;
//		yy = 0.0f;
//		zz = 0.5f;

		mode = 0;
		pcnt = 0;

	}


	// Update is called once per frame
	void Update () {

		//always process


		//move
		if (mode == 0) {
			cashTransform.Translate (xx, yy, zz);

			//delete
			Vector3 pos = cashTransform.position;
			if ((pos.x >= 20) || (pos.x <= -20) ||
			    (pos.y >= 20) || (pos.y <= -20) ||
			    (pos.z >= 20) || (pos.z <= -20)) {
				GameObject.Destroy (gameObject);
			}
		} else if (mode == 1) {
			pcnt++;
			if (pcnt == 30) {
				cashParticleSystem_hitParticles.Stop ();
			}
			if (pcnt == 100) {
				GameObject.Destroy (gameObject);
			}
		}

	}


	//public
	//collision
	public void OnTriggerEnter(Collider coll) {
		//bullet hit
		if (coll.tag == "ht") {
//			GameObject.Destroy (gameObject);
			mode = 1;
			GameObject go = cashTransform.FindChild ("bullet").gameObject;
			go.GetComponent<MeshRenderer> ().enabled = false;
			cashParticleSystem_hitParticles.Play ();
		}
	}

	//set init status
	public void setInitState( float xx, float yy, float zz ){
		this.xx = xx;
		this.yy = yy;
		this.zz = zz;
	}

}
