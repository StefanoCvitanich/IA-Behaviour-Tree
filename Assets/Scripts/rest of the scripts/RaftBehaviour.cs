using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftBehaviour : MonoBehaviour {

	public Item raftItem;

	float addedHealthWhenUpgraded = 5;
	float maxRaftHealth = 5;
	float raftHealth = 5;

	public InventoryManagement playerInventory;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Damaged(float dmg)
	{
		raftHealth -= dmg;

		Debug.Log (raftHealth);

		if (raftHealth <= 0) {

			raftHealth = 0;

			DestroyRaft ();
		}
			
	}

	public void RaftUpgraded()
	{
		maxRaftHealth += addedHealthWhenUpgraded;

		raftHealth = maxRaftHealth;
	}

	private void DestroyRaft()
	{
		gameObject.SetActive (false);

		playerInventory.RemoveItem (raftItem);

		raftHealth = maxRaftHealth;
	}
}
