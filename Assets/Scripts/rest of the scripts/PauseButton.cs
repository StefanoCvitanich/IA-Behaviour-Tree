using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour {

	GUISkin guiSkin;
	float nativeVerticalResolution = 800.0f;
	float nativeHorizontalResolution = 600.0f;
	bool isPaused = false;
	public GameObject quitButton;
	public GameObject continueButton;
    public static bool gamePaused = false;
	private Rect quit;
	private Rect cont;
    private static PauseButton instance = null;

	// Use this for initialization
	void Start () {
        instance = this;
		quit = new Rect((Screen.width)/2,480,800,400);
		cont = new Rect((Screen.width)/2,640,800,400);
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown("escape"))     // If I using the Inventory and I press Esc time starts running again
		{
            SwitchPause();
            quitButton.SetActive(!quitButton.activeInHierarchy);
            continueButton.SetActive(!continueButton.activeInHierarchy);
            gamePaused = isPaused;
        }

	}

	public void Quit(){
		print("Quit!");
		Application.Quit();
	}

	public void Continue(){
		print("Continue");
		Time.timeScale = 1.0f;
		isPaused = false;
		Cursor.visible = false;
		quitButton.SetActive (false);
		continueButton.SetActive (false);
	}

    public void SwitchPause()
    {
        if (Time.timeScale == 0.0f)
            Time.timeScale = 1.0f;
        else
            Time.timeScale = 0.0f;
        Cursor.visible = !Cursor.visible;
        isPaused = !isPaused;
    }

    public static PauseButton GetInstance()
    {
        return instance;
    }

	/*public void Pause ()
	{
		// Set up gui skin
		GUI.skin = guiSkin;

		// Our GUI is laid out for a 1920 x 1200 pixel display (16:10 aspect). The next line makes sure it rescales nicely to other resolutions.
		GUI.matrix = Matrix4x4.TRS (new Vector3(0, 0, 0), Quaternion.identity, new Vector3 (Screen.width / nativeHorizontalResolution, Screen.height / nativeVerticalResolution, 1));
		if(isPaused)
		{
			// RenderSettings.fogDensity = 1;
			if(GUI.Button (quit, "Quit", "button2"))
			{
				print("Quit!");
				Application.Quit();
			}
			if(GUI.Button (Rect((Screen.width)/2,560,140,70), "Restart", "button2"))
			{
				print("Restart");
				Application.LoadLevel("SomeLevelHere");
				Time.timeScale = 1.0;
				isPaused = false;
			}
			if(GUI.Button (cont, "Continue", "button2"))
			{
				print("Continue");
				Time.timeScale = 1.0f;
				isPaused = false;   
			}

		} 
	}*/
}
