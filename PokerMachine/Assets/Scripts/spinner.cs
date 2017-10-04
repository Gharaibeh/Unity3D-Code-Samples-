using UnityEngine;
using System.Collections;

public class spinner : MonoBehaviour {

	public float spinTime;
	// Use this for initialization
	void Start () {
		spinTime = 1;
 	}
	void Update () {
		// Keep moving
		transform.position += new Vector3 (0, spinTime * Time.deltaTime*-400, 0);
 		if (transform.localPosition.y < -300) // DEstroy after..
			transform.parent.GetComponent<reel> ().removeMe (gameObject); 
	
	}
}
