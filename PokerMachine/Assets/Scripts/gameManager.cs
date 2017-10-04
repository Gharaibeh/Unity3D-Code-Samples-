using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
public class gameManager : MonoBehaviour {

	public spinnerManager gameSpinner;
 	int lineIndexer = 1;	
	string reelValues = "";
	public GameObject[]lanesofWinning;
	public AudioClip[] clickSounds;
	public AudioClip[] winningSounds;
	public int wheelIndexer = 0;

	// UI
	public GameObject spinBtn;
	public GameObject []betBtn;
	public Text creditTxt;
	public Text cashTxt;
	public Text betTxt;
	string[,] wheelResutls;



	void Start () {
 		wheelResutls = new string[5,3];
		loadUIText ();
 	}

	void playSound(AudioClip otherClip)
	{
		AudioSource myAudio = GetComponent<AudioSource>();
		myAudio.clip = otherClip;
		myAudio.Play();
	}
	
	void loadUIText()
	{
		betTxt.text = string.Format("${0}.00",GameSetting.BETVALUE );
		creditTxt.text = string.Format("${0}",GameSetting.CREDITVALUE );
		cashTxt.text = string.Format("${0}",GameSetting.CASHVALUE );

	}
 	public void spinPressed () {
		playSound (clickSounds [0]);
		spinBtn.SetActive (false); 	
		betBtn[0].SetActive (false); 	
		betBtn[1].SetActive (false); 
 
		gameSpinner.startSpin ();
		GameSetting.CREDITVALUE -= GameSetting.BETVALUE ;
		loadUIText ();
		wheelIndexer = 0;
	
	}

	public void BetPressed(int value)
	{
		//audio
		if (value > 0)
			playSound (clickSounds [1]);
				else 
			playSound(clickSounds[2]);

		if (GameSetting.BETVALUE + value <= 0 || GameSetting.CREDITVALUE <= 1) 
			return;
		else {
			GameSetting.BETVALUE += value;
			loadUIText();
  
		}

	}
	public void wheelsResutlsBuilder(string s1, string s2, string s3)
	{
 		wheelResutls [wheelIndexer, 0] = s1;
		wheelResutls [wheelIndexer, 1] = s2;
		wheelResutls [wheelIndexer, 2] = s3;
		wheelIndexer++;

		lineIndexer = 1;
		if (wheelIndexer == 4) // all 5 wheel completed
			StartCoroutine(checkWinningLine());

	}


	IEnumerator checkWinningLine()
	{
 		reelValues = "";

		switch (lineIndexer) {
		case 1:
    			for (int i=0; i<5; i++)				
				reelValues += gameSpinner._reel[i].createdPeices [3].GetComponent<Image>().sprite.name.ToString ();
 			break;
		case 2:
  			for (int i=0; i<5; i++)				
				reelValues += gameSpinner._reel[i].createdPeices[2].GetComponent<Image>().sprite.name.ToString();
			break;
		case 3:
  			for (int i=0; i<5; i++)				
				reelValues += gameSpinner._reel[i].createdPeices[1].GetComponent<Image>().sprite.name.ToString();
 			break;
		case 4:
			reelValues  = gameSpinner._reel[0].createdPeices[3].GetComponent<Image>().sprite.name.ToString() + gameSpinner._reel[1].createdPeices[2].GetComponent<Image>().sprite.name.ToString() + gameSpinner._reel[2].createdPeices[1].GetComponent<Image>().sprite.name.ToString() + gameSpinner._reel[3].createdPeices[2].GetComponent<Image>().sprite.name.ToString() + gameSpinner._reel[4].createdPeices[1].GetComponent<Image>().sprite.name.ToString();

			break;
		case 5:
			reelValues  = gameSpinner._reel[0].createdPeices[1].GetComponent<Image>().sprite.name.ToString() + gameSpinner._reel[1].createdPeices[2].GetComponent<Image>().sprite.name.ToString() + gameSpinner._reel[2].createdPeices[3].GetComponent<Image>().sprite.name.ToString() + gameSpinner._reel[3].createdPeices[2].GetComponent<Image>().sprite.name.ToString() + gameSpinner._reel[4].createdPeices[1].GetComponent<Image>().sprite.name.ToString();

			break;
		}
		yield return new WaitForSeconds (1);

		resultCheck ();
		loadUIText ();
		yield return new WaitForSeconds (2);

		lineIndexer++;
		if (lineIndexer == 6) {
			StopCoroutine (checkWinningLine ());
			spinBtn.SetActive (true);
			betBtn[0].SetActive (true); 	
			betBtn[1].SetActive (true); 
 		}
		else
			StartCoroutine(checkWinningLine());
 	}

 
	void resultCheck()
	{
 		string comparePart="";
		bool won5 = false;
		for (int i = 0; i<reelValues.Length; i++) {
			comparePart = reelValues[i].ToString()+reelValues[i].ToString()+reelValues[i].ToString()+reelValues[i].ToString()+reelValues[i].ToString();
			if (reelValues.Contains (comparePart))
			{
				GameSetting.CASHVALUE += GameSetting.BETVALUE * GameSetting.CARDVALUES[2,int.Parse( reelValues.Substring(i,1))];
				won5 = true;
				StartCoroutine(showLane());

				break;
			}
		}

		if (!won5) {
	
		
		
			//check for 4 
			bool won4 = false;
			for (int i = 0; i<reelValues.Length; i++) {
				comparePart = reelValues [i].ToString () + reelValues [i].ToString () + reelValues [i].ToString () + reelValues [i].ToString ();
				if (reelValues.Contains (comparePart)) {
					GameSetting.CASHVALUE += GameSetting.BETVALUE * GameSetting.CARDVALUES[1,int.Parse( reelValues.Substring(i,1))];
 					won4 = true;
					StartCoroutine (showLane ());
					break;
				}	
			}

			if (!won4)
			{
  				//check for 3
				bool won3 = false;
				for (int i = 0; i<reelValues.Length; i++) {
					comparePart = reelValues [i].ToString () + reelValues [i].ToString () + reelValues [i].ToString ();
					if (reelValues.Contains (comparePart)) {
						GameSetting.CASHVALUE += GameSetting.BETVALUE * GameSetting.CARDVALUES[0,int.Parse( reelValues.Substring(i,1))];
 						won3 = true;
						StartCoroutine (showLane ());
						break;
					}		
				}
			}
 		}
 	}

	IEnumerator showLane()
	{
		playWinningsound ();
 		lanesofWinning [lineIndexer - 1].SetActive (true);
		yield return new WaitForSeconds (1);
		lanesofWinning [lineIndexer - 1].SetActive (false); 
 	}

	void playWinningsound()
	{
		char winningPart = reelValues.GroupBy (x => x).OrderByDescending (x => x.Count ()).First ().Key;

		if (winningPart == '1' || 
		    winningPart == '2' || 
		    winningPart == '3' || 
		    winningPart == '4') 
		{
			playSound (winningSounds [int.Parse (winningPart.ToString ())]);
		} else
			playSound (winningSounds[0]);
	}







}
