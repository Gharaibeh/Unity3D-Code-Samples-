using UnityEngine;
using System.Collections;

public class spinnerManager : MonoBehaviour {
	public reel[] _reel;
	public void startSpin(){
		for (int i=0; i<5; i++)
			_reel [i].Spin ();
		float rndTime = Random.Range (5.2f, 9.5f);
		for (int i=0; i<5; i++)
			StartCoroutine (StopWheel (i, i+rndTime));
	}
	IEnumerator StopWheel (int i , float tim) {
		yield return new WaitForSeconds (tim);
		_reel [i].isSpinning = false;
	}
}
