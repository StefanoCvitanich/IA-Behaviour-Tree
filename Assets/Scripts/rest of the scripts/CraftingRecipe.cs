using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject {

	public string recipeName;

	public Item craftedItem;

	public List<Item> recipeMaterials;
}
