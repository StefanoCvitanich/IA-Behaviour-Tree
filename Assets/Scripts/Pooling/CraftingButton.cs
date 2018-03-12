using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingButton : MonoBehaviour {

	public Button crafButton;

	public CraftingRecipe recipe;

	public string recipeDescription;

	public Text descriptionText;

	public GameObject descriptionPanel;

	public GameObject playersRaft;

	private GameObject player;

	private Crafting_System craftingPanel;

	private bool recipeWasObtained;

	private List<CraftingRecipe> playerRecipes;

	private ColorBlock cb;

	// Use this for initialization
	void Start () {

		player = GameObject.Find ("Player");

		craftingPanel = GameObject.Find ("Crafting_Panel").GetComponent<Crafting_System>();

		playerRecipes = craftingPanel.playerRecipesList;

		//descriptionPanel = GameObject.Find ("Description Panel");

		//descriptionText = descriptionPanel.transform.Find ("Description Text").GetComponent<Text> ();

		CheckStatus ();

	}

	// Update is called once per frame
	void Update () {

		if (!recipeWasObtained)
			wasRecipeObtained ();

		CheckStatus ();

	}

	private void wasRecipeObtained()
	{
		playerRecipes = craftingPanel.playerRecipesList;  // This updates the List

		foreach (CraftingRecipe crafRec in playerRecipes) {

			if (recipe.recipeName == crafRec.recipeName) {

				recipeWasObtained = true;

				break;
			}
		}
	}

	private void CheckStatus() //Gives the player visual feedback on wether they can craft the item or not by changing the button's colour
	{
		if (recipeWasObtained && craftingPanel.checkInventory (recipe)) {
		
			craftingPanel.RestoreItem ();

			cb = crafButton.colors;
			cb.normalColor = Color.green;
			crafButton.colors = cb;

		} 

		else {

			craftingPanel.RestoreItem ();

			cb = crafButton.colors;
			cb.normalColor = Color.red;
			crafButton.colors = cb;

		}
	}

	public void CraftItem(){ // OnClick method that should instanciate the newly crafted item into the players inventory.

		//Check if theres available space and if the materials needed are in players inventory (use Crafting_System CheckInventory method)

		if (recipeWasObtained && craftingPanel.checkInventory (recipe)) {

			craftingPanel.EliminateItemsUsedInCrafting ();  //This will delete the items used for crafting

			if(recipe.craftedItem != null) //In case the recipe does not generates any new item
				player.GetComponent<PlayerController> ().inventory.AddItem (recipe.craftedItem);

			if (recipe.recipeName == "upgradeRaft") // Exclusive for the upgrade raft button/recipe
				playersRaft.GetComponent<RaftBehaviour> ().RaftUpgraded ();
			
		} else
			craftingPanel.RestoreItem (); //This will restore the item to the state prior to the crafting button's OnClick Event
	}

	public void ActivatePanel(){
		
		descriptionText.text = recipeDescription;
		descriptionPanel.SetActive (true);
	}

	public void DeactivatePanel(){

		descriptionPanel.SetActive (false);
	}
}
