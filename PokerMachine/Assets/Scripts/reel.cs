using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class reel : MonoBehaviour {
	public gameManager gameManager;
	public List<Sprite> images;
 	public GameObject cardPref;

	public Transform origin;
	public float time=1;
	public int wheelIndex;
	public int imageIndexer = 0;
	public List <GameObject> createdPeices;

	void Start () {
		images = new List<Sprite> ();
 		createdPeices = new List<GameObject> ();
  		StartCoroutine (makeNewPeice());
  	}
	public bool isSpinning;
	void Update()
	{
 		if (!isSpinning) {
			if ((int)createdPeices[createdPeices.Count-1].transform.localPosition.y %127 == 0)
			{
				foreach(GameObject u in createdPeices)
					u.GetComponent<spinner>().spinTime = 0;
 				time = 0;
				StopCoroutine ("makeNewPeice");
				StopAllCoroutines();
 				sendMyValues();

 			}
 		}   
 	}

	public void Spin()
	{
		isSpinning = true;
		iSentMyValues = false;
		time = 1;
		foreach(GameObject u in createdPeices)
			u.GetComponent<spinner>().spinTime = 1;
		StartCoroutine (makeNewPeice());

	}
	public void removeMe(GameObject iss)
	{
		createdPeices.Remove(iss);
		Destroy(iss);
	}
	bool iSentMyValues;
	void sendMyValues()
	{
		if (!iSentMyValues) {
 			gameManager.wheelsResutlsBuilder (createdPeices [3].GetComponent<Image> ().sprite.name, createdPeices [2].GetComponent<Image> ().sprite.name, createdPeices [1].GetComponent<Image> ().sprite.name);
			iSentMyValues = true;
		}

	}
	IEnumerator makeNewPeice()
	{
  		yield return new WaitForSeconds (.3f);
		GameObject newPeice = Instantiate (cardPref, origin.position, Quaternion.identity) as GameObject;
 
		switch (wheelIndex) {
		case 0: 
			newPeice.GetComponent<Image> ().sprite = Resources.Load("Icons/"+GameSetting.WHEELSPRITE1[imageIndexer], typeof(Sprite)) as Sprite;;
			imageIndexer++;
			imageIndexer = imageIndexer % GameSetting.WHEELSPRITE1.Length;
			break;
		case 1:
			newPeice.GetComponent<Image> ().sprite = Resources.Load("Icons/"+GameSetting.WHEELSPRITE2[imageIndexer], typeof(Sprite)) as Sprite;;
			imageIndexer++;
			imageIndexer = imageIndexer % GameSetting.WHEELSPRITE2.Length;
			break;
		case 2:
			newPeice.GetComponent<Image> ().sprite = Resources.Load("Icons/"+GameSetting.WHEELSPRITE3[imageIndexer], typeof(Sprite)) as Sprite;;
			imageIndexer++;
			imageIndexer = imageIndexer % GameSetting.WHEELSPRITE3.Length;
			break;
		case 3:
			newPeice.GetComponent<Image> ().sprite = Resources.Load("Icons/"+GameSetting.WHEELSPRITE4[imageIndexer], typeof(Sprite)) as Sprite;;
			imageIndexer++;
			imageIndexer = imageIndexer % GameSetting.WHEELSPRITE4.Length;
			break;
		case 4:
			newPeice.GetComponent<Image> ().sprite = Resources.Load("Icons/"+GameSetting.WHEELSPRITE5[imageIndexer], typeof(Sprite)) as Sprite;;
			imageIndexer++;
			imageIndexer = imageIndexer % GameSetting.WHEELSPRITE5.Length;
			break;	
		
		}



		newPeice.transform.parent = transform;
 		newPeice.transform.localScale = (origin.transform.localScale);
		createdPeices.Add (newPeice);
		newPeice.AddComponent<spinner> ();
 
		if (time == 0)
			StopCoroutine (makeNewPeice ());
		else
			StartCoroutine (makeNewPeice ());

	}
 
    
	 

}
