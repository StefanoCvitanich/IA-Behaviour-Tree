using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Constitution : MonoBehaviour {

	public float health = 100;
	[HideInInspector]
	public float hunger = 100;
	[HideInInspector]
	public float thirst = 100;
	[HideInInspector]
	public float normalDehidration = 1.0f;
	[HideInInspector]
	public float normalStarvation = 1.0f;
	[HideInInspector]
	public float lostHealthWhenWeakened = 2.0f;
	[HideInInspector]
	[Range(0,100)]
	public float faminePercentage = 25;
	[HideInInspector]
	[Range(0,100)]
	public float dehidrationPercentage = 25;

    public Image healthBar;
	[HideInInspector]
    public Image hungerBar;
	[HideInInspector]
    public Image thirstBar;
	[HideInInspector]
    public Onset hungerEffect;

    private float maxHealth = 100;
	private float maxHunger = 100;
	private float maxThirst = 100;
	private float hungerTimer = 0;
	private float thirstTimer = 0;
	private float dyingTimer = 0;

    private bool hungerIsZero = false;
    private bool thirstIsZero = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		/*
		hungerTimer += Time.deltaTime;
		thirstTimer += Time.deltaTime;

		if (thirstTimer >= 1) {

			thirstTimer = 0;

            if (thirst > 0)
            {
                GettingThirsty(normalDehidration);
                thirstIsZero = false;
            }
            else
            {
                thirstIsZero = true;
            }
		}

		if (hungerTimer >= 1) {

			hungerTimer = 0;

            if (hunger > 0)
            {
                GettingHungry(normalStarvation);
                hungerIsZero = false;
            }
            else
            {
                hungerIsZero = true;
            }
		}

        dyingTimer += Time.deltaTime;
		if (dyingTimer >= 1) {
		
            if (hungerIsZero)
			    health -= lostHealthWhenWeakened;
            if (thirstIsZero)
                health -= lostHealthWhenWeakened;

            dyingTimer = 0;

			if (health <= 0)
				SceneManager.LoadScene ("Menu");
		}*/

        InterpolateHealthFill();/*
        InterpolateHungerFill();
        InterpolateThirstFill();

        if (InFamineThreshold())
        {
            if (!hungerEffect.gameObject.activeInHierarchy)
                hungerEffect.gameObject.SetActive(true);
        }
        else
        {
            if (!hungerEffect.disabled)
            {
                hungerEffect.StartOfset();
            }
        }*/
	}


	private bool Thisty(){
	
		if (thirst <= 0)
			return true;
		else
			return false;
	}


	private bool Hungry(){
	
		if (hunger <= 0)
			return true;
		else
			return false;
	}


	public float GetHunger(){
	
		return hunger;
	}


	public float GetThirst(){
	
		return thirst;
	}


	public float GetHealth(){
	
		return health;
	}


	public void Eat(float calories){

		hunger += calories;

		if (hunger > maxHunger)
			hunger = maxHunger;
	}


	public void Drink(float mililitres){
	
		thirst += mililitres;

		if (thirst > maxThirst)
			thirst = maxThirst;
	}


	public void GettingHungry(float consumedCalories){
	
		hunger -= consumedCalories;

		if (hunger < 0)
			hunger = 0;
	}


	public void GettingThirsty(float dehidration){
	
		thirst -= dehidration;

		if (thirst < 0)
			thirst = 0;
	}


	public bool InFamineThreshold(){
	
		if ((hunger * 100) / maxHunger < faminePercentage)
			return true;
		else
			return false;
	}


	public bool InDehidrationThreshold(){
	
		if ((thirst * 100) / maxThirst < dehidrationPercentage)
			return true;
		else
			return false;
	}

    public void InterpolateHealthFill()
    {
        float fill = health / maxHealth;
        healthBar.fillAmount = fill;
    }

    public void InterpolateHungerFill()
    {
        float fill = hunger / maxHunger;
        hungerBar.fillAmount = fill;
    }

    public void InterpolateThirstFill()
    {
        float fill = thirst / maxThirst;
        thirstBar.fillAmount = fill;
    }

    public void Hit(float damage)
    {
        health -= damage;
    }
}
