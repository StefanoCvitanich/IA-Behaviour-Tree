using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {

    public string resName;
    public Item[] resources;
    public bool canGather;
    public List<Item> res = new List<Item>();
    private Collider col;
    private bool gathering = false;
    private PlayerController player;

    void Start()
    {
        col = transform.GetChild(0).GetComponent<Collider>();
        for (int i = 0; i < resources.Length; i++)
        {
            if(resources[i] != null)
            {
                resources[i].source = this;
                res.Add(Instantiate(resources[i]));
            }
        }
    }

	void Update()      //Se buggea (No se va de pantalla) el cartel de gather si apretaste F y despues te fuiste sin recolectar nada
    {
        if (!player)
        {
            player = GlobalRef.playerPos.gameObject.GetComponent<PlayerController>();
        }

        if (canGather && !gathering)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(GlobalRef.cameraPos.position, GlobalRef.cameraPos.forward, out hitInfo) && hitInfo.collider == col)
            {
                GlobalRef.gNotice.gameObject.SetActive(true);
                GlobalRef.gNotice.text = "Press F to gather from " + resName + ".";

                if (Input.GetKeyDown(KeyCode.F))
                {
                    gathering = true;
                    PauseButton.GetInstance().SwitchPause();
                    StartGather();
                    return;
                }
            }
            else
            {
                if (GlobalRef.gNotice.gameObject.activeInHierarchy)
                {
                    GlobalRef.gNotice.gameObject.SetActive(false);
                }
                if (gathering)
                {
                    StopGather();
                    Cursor.visible = false;
                }
            }
        }

        if (gathering)
        {
            GlobalRef.gNotice.text = "Press F to finish.";
        }

        if (gathering && Input.GetKeyDown(KeyCode.F))
        {
            StopGather();
            PauseButton.GetInstance().SwitchPause();
            GlobalRef.gNotice.gameObject.SetActive(false);
            gathering = false;
            GlobalRef.gNotice.text = "";
        }
    }

    void StartGather()
    {
        GlobalRef.resPopup.gameObject.SetActive(true);
        for (int i = 0; i < res.Count; i++)
        {
            if(res[i] != null)
            {
                GlobalRef.resPopup.AddItem(res[i]);
            }
        }
    }

    void StopGather()
    {
        for (int i = 0; i < res.Count; i++)
        {
            if (res[i] != null)
            {
                GlobalRef.resPopup.RemoveItem(res[i]);
            }
        }
        GlobalRef.resPopup.gameObject.SetActive(false);
        if (isEmpty())
        {
            Destroy(gameObject);
        }
    }

    bool isEmpty()
    {
        for (int i = 0; i < res.Count; i++)
        {
            if (res[i] != null)
            {
                return false;
            }
        }
        return true;
    }
}
