using UnityEngine;
using System.Collections;

public class Dracula : Monster {

	// Use this for initialization of dracula
	void Start () 
	{
		speed=0.1f;
		element = 'f';
		maxHP=100;
		hp=maxHP;
		maxMP=100;
		mp=maxMP;
		strength=10;
		mpRecoverQuantity=3;
		mpRecoverTime=1;

		Vector3 monsterPositionOnScreen=Camera.main.WorldToViewportPoint(this.transform.position);
		monsterPositionOnScreen.y=monsterPositionOnScreen.y+0.1f;
		monsterPositionOnScreen.x=monsterPositionOnScreen.x-0.06f;
		monsterPositionOnScreen.z=0;
		HPLabel=(GUIText)Instantiate (HPLabel, monsterPositionOnScreen,Quaternion.identity);
		HPLabel.text="Dracula";
	}


}
