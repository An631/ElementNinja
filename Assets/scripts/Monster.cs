using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {
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
	public bool spAttackEnabled;
	public float spAttackRecoverTime;
	public float spAttackRecoverTimeElapsed;
	public GUIText HPLabel;

	//Private variables


	// Update is called once per frame
	void Update () {
		// The monster will recover a certain quantity of MP every certain period of time
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

		// The monster will be able to do a special attack every certain period of time
		if(spAttackEnabled==false)
		{
			// Only if the special attack is not enabled the spAttackRecoveryElapsed will start counting
			spAttackRecoverTimeElapsed+=Time.deltaTime;
			if(spAttackRecoverTimeElapsed>spAttackRecoverTime)
			{
				spAttackRecoverTimeElapsed=0;
				spAttackEnabled=true;
			}
		}
		
	}
	
	public virtual void Move(){
		
	}

	public virtual void Attack(){

	}

	public void OnCollisionEnter(Collision col) {
		Debug.Log ("HItted");
	}
	public void OnTriggerEnter(Collider col) {
		Debug.Log ("HItted");
	}
	public virtual void SpecialAttack(int specialAttackMPCost){

		// The special attack can only be used whenever there is enough mp to cast it
		if(specialAttackMPCost<=this.mp)
		{
			spAttackEnabled=false;
			this.changeMP (-specialAttackMPCost);
		}
		else
		{
			// Not enough MP message is displayed to the player
			Debug.Log ("Not enough MP to cast a special attack");
		}
	}


	public virtual void changeMP(int toRecover){
		this.mp+=toRecover;

		if(this.mp>this.maxMP)
			//In case the mp has gone farther than the maximum, make the mp equal to the maximum value possible on this monster.
			this.mp=this.maxMP;

		if(this.mp<0)
			// MP should never get a negative value so it will stay on 0
			this.mp=0;
	}

	public virtual void changeHP(int toRecover){
		this.hp+=toRecover;

		if(this.hp>this.maxHP)
			//In case the hp has gone farther than the maximum, make the hp equal to the maximum value possible on this monster.
			this.hp=this.maxHP;

		if(this.hp<=0)
		// HP should never get a negative value so it will stay on 0
			this.hp=0;
		this.showHP();	
	}

	public virtual void changeStrength(int quantityToChangeStrength){
		this.strength+=quantityToChangeStrength;
	}

	public virtual void showHP(){

		//this.gameObject.AddComponent("GUIText");

		HPLabel.text=HPLabel.text.Split(' ')[0];
		HPLabel.text=HPLabel.text+" "+this.hp;
	}


	public virtual void Die(){
		//Run dying animation()
		Destroy (this.HPLabel);
		Destroy(this.gameObject);

	}
}
