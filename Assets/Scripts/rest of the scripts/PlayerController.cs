using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Magic.Pooling;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;
    public float gravity = 20.0f;
    public InventoryManagement inventory;
    public GameObject craftingPanel;
    public Constitution constitution;
    public float knifeDamage;
    public float knifeRange;
    public GameObject knifeGo;
    public float javelinDamage;
    public float javelinForce;
    public GameObject javelinGo;
    public GameObject javelinPfab;
    public float spearDamage;
    public float spearRange;
    public GameObject spearGo;

    [HideInInspector]
    public string weaponName;

    public Transform leftLeg;
    public Transform rightLeg;
    public Transform raft;

    private bool raftEnabled = false;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 rotDirection = Vector3.zero;
    private CharacterController controller;
    private float raftSpeed;
    private float originalSpeed;
    private bool isOverKelp = false;

    private float timer = 0.5f;

    void Start()
    {
        weaponName = "knife";
        transform.position = TerrainGen.GetSpawnPoint();
        controller = GetComponent<CharacterController>();
        raftSpeed = speed * 2;
        originalSpeed = speed;
    }

    bool HasRaft()
    {
        foreach (Item i in inventory.items) {
            if (i != null && i.id == 20)
                return true;
        }
        return false;
    }

    void MeleeAttack(float damage, float range)
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + transform.forward * (range / 2), new Vector3(1, 1, range / 2), GlobalRef.cameraPos.rotation);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "Enemy")
            {
                Debug.Log("Hit Enemy");
                EnemyBehav enemy = colliders[i].gameObject.GetComponent<EnemyBehav>();
                enemy.Hit(damage);
            }
        }
    }

    void ThrowJavelin(float damage, float force)
    {
        GameObject javelin = PoolManager.Spawn(javelinPfab, transform.position + Vector3.up * 0.75f, GlobalRef.cameraPos.rotation, "javelins");
        javelin.GetComponent<JavelinBehav>().damage = damage;
        javelin.GetComponent<JavelinBehav>().speed = javelinForce;
    }

    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse1) && timer <= 0)
        {
            if (weaponName == "knife")
            {
                MeleeAttack(knifeDamage, knifeRange);
            }
            else if (weaponName == "spear")
            {
                MeleeAttack(spearDamage, spearRange);
            }
            else if (weaponName == "javelin")
            {
                ThrowJavelin(javelinDamage, javelinForce);
            }
            timer = 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.gameObject.SetActive(!inventory.gameObject.activeInHierarchy);
			craftingPanel.SetActive(!craftingPanel.gameObject.activeInHierarchy);
            PauseButton.GetInstance().SwitchPause();
        }

        Collider[] overlaps = Physics.OverlapSphere(transform.position, 0.15f);
        for (int i = 0; i < overlaps.Length; i++)
        {
            if (overlaps[i].tag == "KelpBank")
            {
                isOverKelp = true;
                break;
            }
            if (i == overlaps.Length - 1)
            {
                isOverKelp = false;
                break;
            }
        }

        if (isOverKelp && HasRaft())
        {
            speed = raftSpeed / 2;
        }
        else if (isOverKelp && !HasRaft())
        {
            speed = originalSpeed / 2;
        }
        else if (!isOverKelp)
        {
            SetSpeeds();
        }
    }

    void SetSpeeds()
    {
        if (transform.position.y < -0.25 && HasRaft())
        {
            raft.gameObject.SetActive(true);
            //leftLeg.Rotate (0, -90, 0);
            //rightLeg.Rotate (0, -90, 0);
            raftEnabled = true;
            speed = raftSpeed;
        }
        else if (transform.position.y < -0.25 && !HasRaft())
        {
            raft.gameObject.SetActive(false);
            //leftLeg.Rotate (0, -90, 0);
            //rightLeg.Rotate (0, -90, 0);
            raftEnabled = false;
            speed = originalSpeed;
        }
        else if (raftEnabled)
        {
            raft.gameObject.SetActive(false);
            //leftLeg.Rotate (0, 90, 0);
            //rightLeg.Rotate (0, 90, 0);
            raftEnabled = false;
            speed = originalSpeed;
        }
    }

    void LateUpdate()
    {
        if (controller.isGrounded)
        {
			moveDirection = new Vector3(Input.GetAxis("Horizontal") / 2, 0, Input.GetAxis ("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            if(constitution.InDehidrationThreshold())
                moveDirection *= speed / 2;
            else
                moveDirection *= speed;
            rotDirection = new Vector3 (0, Input.GetAxis ("Mouse X"), 0);

        }
        moveDirection.y -= gravity * Time.deltaTime;

        if (!Cursor.visible)
        {
            controller.Move(moveDirection * Time.deltaTime);
            transform.Rotate(rotDirection.x, rotDirection.y, rotDirection.z);
        }
    }

	/*void OnCollisionEnter(Collision c)
	{

		Debug.Log ("sahbajdhvashd");
		if (c.gameObject.tag == "Water") 
		{
			raft.gameObject.SetActive (true);
			leftLeg.Rotate (0, -90, 0);
			rightLeg.Rotate (0, -90, 0);
		}
	}

	void OnCollisionExit(Collision c)
	{
		Debug.Log ("sahbajdhvashd");
		if (c.gameObject.tag == "Water") 
		{
			raft.gameObject.SetActive (false);
			leftLeg.Rotate (0, 90, 0);
			rightLeg.Rotate (0, 90, 0);
		}
	}*/
}
