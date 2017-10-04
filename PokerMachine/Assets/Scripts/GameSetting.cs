using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GameSetting {

	// initialization..
	public static int BETVALUE = 1;
	public static int CREDITVALUE = 10000;
	public static int CASHVALUE = 0;

	// Reel vlaues
	public static int[] WHEELSPRITE1 = new int[]{1,5,2,1,6,5,8,5,1,2,3,7,4,5,8,1,4,3,2,5,6};
	public static int[] WHEELSPRITE2 = new int[]{5,1,6,3,7,8,1,3,2,4,6,8,5,4,5,3,8,7,5,4,1,7,4,8,4};
	public static int[] WHEELSPRITE3 = new int[]{8,4,1,3,2,6,7,2,3,4,1,5,6,7,8,2,5,4,3,1,2,7,6,7,1,4,3,2,4};
	public static int[] WHEELSPRITE4 = new int[]{1,7,4,2,3,8,4,3,2,5,6,7,2,3,4,5,8,1,2,6,2,4,2,6,3,7,8,4,6,2,3,1,2,5,6,3,4};
	public static int[] WHEELSPRITE5 = new int[]{8,5,1};

	 
	// Cash values
	public static int[,] CARDVALUES = {
 		{0,250,200,150,100,90,80,70,60},  // 3 wins
		{0,500,450,400,350,300,250,200,100}, // 4 wins
		{0,1000,800,700,600,700,600,500,400} // 5 wins
 	};


}
