using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalRef : MonoBehaviour {

    public Transform camPos;
    public Transform plPos;
    public Text gatherNotice;
    public InventoryManagement resourcePopup;
    public Image resourceGatherToolUI;
    public GameObject startHintText;
    static public Transform cameraPos;
    static public Transform playerPos;
    static public Text gNotice;
    static public InventoryManagement resPopup;
    static public Image resGatherTool;
    static public GameObject startHint;

	void Start () {
        cameraPos = camPos;
        playerPos = plPos;
        gNotice = gatherNotice;
        resPopup = resourcePopup;
        resGatherTool = resourceGatherToolUI;
        startHint = startHintText;
    }
}
