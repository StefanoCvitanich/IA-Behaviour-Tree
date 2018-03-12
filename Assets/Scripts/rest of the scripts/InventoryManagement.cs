using UnityEngine;
using UnityEngine.UI;

public class InventoryManagement : MonoBehaviour
{
	public Image[] itemImages = new Image[numItemSlots];
	public Item[] items = new Item[numItemSlots];
	public const int numItemSlots = 15;

	//public GameObject descriptionPanel;
	//public Text descriptionText;
    
	public void AddItem(Item itemToAdd)
	{
		for (int i = 0; i < items.Length; i++)
		{
			if (items[i] == null)
			{
				items[i] = itemToAdd;
				itemImages[i].sprite = itemToAdd.sprite;
				itemImages[i].enabled = true;
				return;
			}
		}
	}

	public void RemoveItem (Item itemToRemove)
	{
		for (int i = 0; i < items.Length; i++)
		{
			if (items[i] == itemToRemove)
			{
				items[i] = null;
				itemImages[i].sprite = null;
				itemImages[i].enabled = false;
				return;
			}
		}
	}

	public bool hasAvailableSpace(){

		foreach (Item item in items) {

			if (item == null)
				return true;
		}

		return false;
	}

    public void TransferItemToPlayer(int index)
    {
        GlobalRef.playerPos.GetComponent<PlayerController>().inventory.GetComponent<InventoryManagement>().AddItem(Instantiate(items[index]));
        items[index].source.res.Remove(items[index]);
        RemoveItem(items[index]);
    }

    public void UseItem(int index)
    {
        if (items[index].isConsumable)
        {
            GlobalRef.playerPos.GetComponent<Constitution>().Eat(items[index].hungerRestored);
            GlobalRef.playerPos.GetComponent<Constitution>().Drink(items[index].thirstRestored);
            RemoveItem(items[index]);
			return;
        }

        if (items[index].isWeapon)
        {
            PlayerController player = GlobalRef.playerPos.GetComponent<PlayerController>();
            player.weaponName = items[index].weaponName;
            if(player.weaponName == "javelin")
            {
                player.javelinGo.SetActive(true);
                player.knifeGo.SetActive(false);
                player.spearGo.SetActive(false);
            }
            else if (player.weaponName == "spear")
            {
                player.javelinGo.SetActive(false);
                player.knifeGo.SetActive(false);
                player.spearGo.SetActive(true);
            }
        }
    }

	/*public void ActivatePanel(int index){

		if (items [index] != null) {
			descriptionText.text = items [index].description;
			descriptionPanel.SetActive (true);
		}
		else
			descriptionPanel.SetActive (false);
	}

	public void DeactivatePanel(){

		descriptionPanel.SetActive (false);
	}*/
}