using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour {

    public float delay; 

	void Update () {
		if(delay > 0)
        {
            delay -= Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
        }
	}
}
