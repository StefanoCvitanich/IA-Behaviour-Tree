using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting_System : MonoBehaviour {

	private Item[] itemsVec; // The Items in the Player's Inventory

	public List<CraftingRecipe> playerRecipesList;  // Manually add the raft the and the stone axe recipes 
													//to the List for the beginning of the game

	private InventoryManagement playerInventory;

	void Start()
	{
		playerInventory = GameObject.Find ("Player").GetComponent<PlayerController> ().inventory;

		itemsVec = playerInventory.items;
	}


	public bool checkInventory (CraftingRecipe crafRec){
	
		bool canCraft = false;
		int remainingMaterials = crafRec.recipeMaterials.Count;

		if (HasRaft () && crafRec.recipeName == "raft")  //Me fijo si intenta craftear una balsa y si ya tiene una en el inventario
			return canCraft;

		foreach (Item crafMat in crafRec.recipeMaterials) {

			foreach (Item item in itemsVec) {

				if (item != null) {

					if (crafMat.id == item.id && item.willBeUsedToCraft == false) { //second part prevents the same item 
																					// from being used twice
						remainingMaterials--;
						item.willBeUsedToCraft = true;
						break;
					}
				}
			}
		}

		if (remainingMaterials == 0 && playerInventory.hasAvailableSpace())
			canCraft = true;
	
		return canCraft;
	}

	public bool AddRecipe (CraftingRecipe foundRecipe){
	
		foreach (CraftingRecipe crafRec in playerRecipesList) {

			if(crafRec.recipeName == foundRecipe.recipeName)
				return false;

			else {

				playerRecipesList.Add (foundRecipe);

				return true;
			}
		}

		return false;
	}

	public void EliminateItemsUsedInCrafting() //This will delete the items used for crafting
	{
		foreach (Item item in itemsVec) {
		
			if (item != null) {
				
				if (item.willBeUsedToCraft)
					playerInventory.RemoveItem (item);
			}
		}
	}

	public void RestoreItem()  //This will restore the item to the state prior to the crafting button's OnClick Event
	{
        if (itemsVec == null)
            return;
		foreach (Item item in itemsVec) {

			if(item != null)
				item.willBeUsedToCraft = false;
		}
	}

	bool HasRaft()
	{
		foreach (Item i in itemsVec) {
			if (i != null && i.id == 20)
				return true;
		}
		return false;
	}
}
