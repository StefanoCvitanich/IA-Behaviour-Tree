using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {

    public float dayNightCycleLenght;
    public MeshRenderer waterRenderer;
    public MeshRenderer skyWaterRenderer;
    public ParticleSystem[] fogLayers;
    public float starFadeTime;
    private float degPerSecond;
    private ParticleSystem[] stars;

	void Start () {
        degPerSecond = 360 / dayNightCycleLenght;
        GameObject[] starsGo = GameObject.FindGameObjectsWithTag("Star");
        stars = new ParticleSystem[starsGo.Length];
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i] = starsGo[i].GetComponent<ParticleSystem>();
        }
	}
	
	void Update () {
        transform.Rotate(-degPerSecond * Time.deltaTime, 0, 0);
        if (transform.rotation.eulerAngles.x < 180 && transform.rotation.eulerAngles.x > 0)
        {
            foreach (ParticleSystem p in stars)
            {
                if (p.startColor.a > 0f)
                {
                    p.startColor = new Color(1, 1, 1, p.startColor.a - starFadeTime * Time.deltaTime);
                }
            }
            if (transform.rotation.eulerAngles.x < 180 && transform.rotation.eulerAngles.x > 90)
            {
                float greyFactor = Squish(transform.rotation.eulerAngles.x, 180, 90);
                greyFactor = InverseProportion(greyFactor, 1);
                greyFactor = Mathf.Lerp(0.3f, 0.87f, greyFactor);
                Color c = new Color(greyFactor, greyFactor, greyFactor);
                waterRenderer.material.color = c;
                skyWaterRenderer.material.color = c;

                foreach (ParticleSystem p in fogLayers)
                {
                    Color nc = new Color(greyFactor, greyFactor, greyFactor);
                    p.startColor = nc;
                }
            }
            if (transform.rotation.eulerAngles.x < 90 && transform.rotation.eulerAngles.x > 0)
            {
                float greyFactor = Squish(transform.rotation.eulerAngles.x, 90, 0);
                greyFactor = Mathf.Lerp(0.3f, 0.87f, greyFactor);
                Color c = new Color(greyFactor, greyFactor, greyFactor);
                waterRenderer.material.color = c;
                skyWaterRenderer.material.color = c;

                foreach (ParticleSystem p in fogLayers)
                {
                    Color nc = new Color(greyFactor, greyFactor, greyFactor);
                    p.startColor = nc;
                }
            }
        }
        else if (transform.rotation.eulerAngles.x < 360 && transform.rotation.eulerAngles.x > 180)
        {
            foreach (ParticleSystem p in stars)
            {
                if (p.startColor.a < 1f)
                {
                    p.startColor = new Color(1, 1, 1, p.startColor.a + starFadeTime * Time.deltaTime);
                }
            }
            if (transform.rotation.eulerAngles.x < 360 && transform.rotation.eulerAngles.x > 270)
            {
                float greyFactor = Squish(transform.rotation.eulerAngles.x, 360, 270);
                greyFactor = Mathf.Lerp(0.1f, 0.3f, greyFactor);
                Color c = new Color(greyFactor, greyFactor, greyFactor);
                waterRenderer.material.color = c;
                skyWaterRenderer.material.color = c;

                foreach (ParticleSystem p in fogLayers)
                {
                    Color nc = new Color(greyFactor, greyFactor, greyFactor);
                    p.startColor = nc;
                }
            }
            if (transform.rotation.eulerAngles.x < 270 && transform.rotation.eulerAngles.x > 180)
            {
                float greyFactor = Squish(transform.rotation.eulerAngles.x, 270, 180);
                greyFactor = InverseProportion(greyFactor, 1);
                greyFactor = Mathf.Lerp(0.1f, 0.3f, greyFactor);
                Color c = new Color(greyFactor, greyFactor, greyFactor);
                waterRenderer.material.color = c;
                skyWaterRenderer.material.color = c;

                foreach (ParticleSystem p in fogLayers)
                {
                    Color nc = new Color(greyFactor, greyFactor, greyFactor);
                    p.startColor = nc;
                }
            }
        }
	}

    float Squish(float n, float max, float min)
    {
        return (n - min) / (max - min);
    }

    float InverseProportion(float val, float max)
    {
        return Mathf.Abs(val - max);
    }
}