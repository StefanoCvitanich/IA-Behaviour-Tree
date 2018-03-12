using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Onset : MonoBehaviour {

    public float speed;
    public bool disabled = true;
    public Image image;
    private bool onsetting = false;
    private bool ofsetting = false;
    private Color color;

	void Update () {
        if (onsetting)
        {
            if(image.color.a < 1)
            {
                color.a += speed * Time.deltaTime;
                image.color = color;
            }
            else
            {
                color.a = 1;
                image.color = color;
                onsetting = false;
            }
        }
        else if (ofsetting)
        {
            if (image.color.a > 0)
            {
                color.a -= speed * Time.deltaTime;
                image.color = color;
            }
            else
            {
                color.a = 0;
                image.color = color;
                ofsetting = false;
                gameObject.SetActive(false);
            }
        }
	}

    void OnEnable()
    {
        StartOnset();
    }

    void StartOnset()
    {
        onsetting = true;
        disabled = false;
        color = new Color(image.color.r, image.color.g, image.color.b, 0);
    }

    public void StartOfset()
    {
        ofsetting = true;
        disabled = true;
        color = new Color(image.color.r, image.color.g, image.color.b, 1);
    }
}
