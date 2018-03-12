using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public void StartButton(){
	
		SceneManager.LoadScene ("stefano");
	}

	public void ControlsButton(){
	
		SceneManager.LoadScene ("ControlsScene");
	}

	public void QuitButton(){
	
		Application.Quit ();
	}

	public void MenuButton(){
	
		SceneManager.LoadScene ("Menu");
	}
}
