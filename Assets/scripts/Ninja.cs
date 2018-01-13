using UnityEngine;
using System.Collections;

public class Ninja : MonoBehaviour {

	public float speed;
	public char element;
	public int maxHP;
	public int hp;
	public int maxMP;
	public int mp;
	public int mpRecoverQuantity;
	public float mpRecoverTime;
	public float mpRecoverTimeElapsed;
	public int strength;
	public bool ninjutsuEnabled;
	public float ninjutsuRecoverTime;
	public float ninjutsuRecoverTimeElapsed;
	
	// Use this for initialization
	void Start () {
		speed=3f;
		maxHP=50;
		hp=maxHP;
		maxMP=10;
		mp=maxMP;
		strength=10;
		mpRecoverQuantity=1;
		mpRecoverTime=3;
		element='w';
	}

	// Update is called once per frame
	void Update () {
		// The ninja will recover a certain quantity of MP every certain period of time
		if(mp<maxMP)
		{
			// Recover mp only when the mp is lower than the maximum mp.
			mpRecoverTimeElapsed+=Time.deltaTime;
			if(mpRecoverTimeElapsed>mpRecoverTime)
			{
				mpRecoverTimeElapsed=0;
				changeMP (mpRecoverQuantity);
			}
		}
		
		// The ninja will be able to do an element attack every certain period of time
		if(!ninjutsuEnabled)
		{
			// Only if the element attack is not enabled the elementAttackRecoveryElapsed will start counting
			ninjutsuRecoverTimeElapsed+=Time.deltaTime;
			if(ninjutsuRecoverTimeElapsed>ninjutsuRecoverTime)
			{
				ninjutsuRecoverTimeElapsed=0;
				ninjutsuEnabled=true;
			}
		}
		
	}

	public void Ninjutsu(int NinjutsuMPCost){
		
		// The special attack can only be used whenever there is enough mp to cast it
		if(NinjutsuMPCost<=this.mp && ninjutsuEnabled)
		{
			ninjutsuEnabled=false;
			this.changeMP (-NinjutsuMPCost);
		}
		else
		{
			// Not enough MP message is displayed to the player
			Debug.Log ("Not enough MP to cast a ninjutsu or the player hasn't recuperated from last ninjutsu");
		}
	}
	
	public void Move()
	{
	}


	public  void changeMP(int toRecover){
		this.mp+=toRecover;
		
		if(this.mp>this.maxMP)
			//In case the mp has gone farther than the maximum, make the mp equal to the maximum value possible on this monster.
			this.mp=this.maxMP;
		
		if(this.mp<0)
			// MP should never get a negative value so it will stay on 0
			this.mp=0;
	}
	
	public  void changeHP(int toRecover){
		this.hp+=toRecover;
		
		if(this.hp>this.maxHP)
			//In case the hp has gone farther than the maximum, make the hp equal to the maximum value possible on this monster.
			this.hp=this.maxHP;
		
		if(this.hp<=0)
			// HP should never get a negative value so it will stay on 0
			this.hp=0;
		
	}
	
	public  void changeStrength(int quantityToChangeStrength){
		this.strength+=quantityToChangeStrength;
	}

	

}
