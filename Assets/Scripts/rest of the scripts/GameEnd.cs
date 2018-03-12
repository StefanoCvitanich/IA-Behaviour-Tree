using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour {

    public GameObject gameEnd;

	void OnTriggerEnter(Collider c)
    {
        if(c.tag == "Player")
        {
            gameEnd.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }
}
