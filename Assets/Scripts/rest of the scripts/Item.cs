using UnityEngine;

[CreateAssetMenu]  //You can creat an "Item" using "Create" in the Menu bar
public class Item : ScriptableObject {

    public Sprite sprite;
    public bool destroyParentOnHarvest;
	public GameObject item;
	public int id;
	public int minQuant;
	public int maxQuant;
    public Resource source;
    public bool isConsumable;
    public int hungerRestored;
    public int thirstRestored;
	public bool willBeUsedToCraft = false;
    public bool isWeapon;
    public string weaponName;
	public string description;
}
