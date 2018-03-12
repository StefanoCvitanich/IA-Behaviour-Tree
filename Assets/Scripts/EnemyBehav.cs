using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Magic.BehaviourTrees;
using Magic.Pooling;

public class EnemyBehav : MonoBehaviour {

    public float health;
    public ParticleSystem bloodParticles;
    public float moveSpeed;
    public float attackRange;
    public float damagePerSecond;
    public float rangeOfSight;
    public float diveSpeed;
    public float despawnHeight;

    private Constitution player;
    private BhTree tree = new BhTree();
    private float distanceToPlayer;

    void SetupBhTree()
    {
        // Setup Base
        tree.AddNode(new Selector(), "picker");
        tree.AddNode("onPursuit", new Sequencer(), "picker");
        tree.AddNode("attack", new Sequencer(), "picker");
        tree.AddNode("dive", new Sequencer(), "picker");

        // On Pursue
        tree.AddNode("pdec0", new Decorator(), "onPursuit"); //Pursue Decorator 0
        tree.AddNode("pTouchingIsland", new Conditional(IsTouchingIsland), "pdec0");
        tree.AddNode("pdec1", new Decorator(), "onPursuit");
        tree.AddNode("pIsInRange", new Conditional(PlayerInRange), "pdec1");
        tree.AddNode("pdec2", new Decorator(), "onPursuit");
        tree.AddNode("pIsOnIsland", new Conditional(PlayerOnIsland), "pdec2");
        tree.AddNode("pIsOnRoS", new Conditional(PlayerInRoS), "onPursuit");
        tree.AddNode("pActPursue", new Action(PursuePlayer), "onPursuit");

        // Attack
        tree.AddNode("adec0", new Decorator(), "attack"); // Attack Decorator 0
        tree.AddNode("aTouchingIsland", new Conditional(IsTouchingIsland), "adec0");
        tree.AddNode("aIsInRange", new Conditional(PlayerInRange), "attack");
        tree.AddNode("adec1", new Decorator(), "attack");
        tree.AddNode("aIsOnIsland", new Conditional(PlayerOnIsland), "adec1");
        tree.AddNode("aAttack", new Action(AttackPlayer), "attack");

        // Dive
        tree.AddNode("dlogic1", new Logic(LogicType.Or), "dive"); //Dive Logic 0
        tree.AddNode("ddec1", new Decorator(), "dlogic1");
        tree.AddNode("dPlayerInRoS", new Conditional(PlayerInRoS), "ddec1");
        tree.AddNode("dPlayerOnIsland", new Conditional(PlayerOnIsland), "dlogic1");
        tree.AddNode("dTouchingIsland", new Conditional(IsTouchingIsland), "dlogic1");
        tree.AddNode("dDive", new Action(Dive), "dive");
        tree.AddNode("dDespawnHeight", new Conditional(AtDespawnHeight), "dive");
        tree.AddNode("dDespawn", new Action(Despawn), "dive");
    }

    bool IsTouchingIsland()
    {
        Collider[] colliders = Physics.OverlapCapsule(transform.position, transform.position + Vector3.up, 0.3f);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "Island")
            {
                return true;
            }
        }
        return false;
    }

    NodeStatus Despawn()
    {
        PoolManager.DeSpawn(gameObject, "enemies");
        return NodeStatus.True;
    }

    bool AtDespawnHeight()
    {
        if (transform.position.y <= despawnHeight)
            return true;
        else return false;
    }

    NodeStatus Dive()
    {
        transform.Translate(Vector3.down * diveSpeed * Time.deltaTime);
        return NodeStatus.True;
    }

    NodeStatus AttackPlayer()
    {
        player.Hit(damagePerSecond * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        return NodeStatus.True;
    }

    bool PlayerOnIsland()
    {
        if (player.transform.position.y > -0.2f)
            return true;
        else return false;
    }

    bool PlayerInRange()
    {
        if (distanceToPlayer <= attackRange)
        {
            return true;
        }
        else return false;
    }

    bool PlayerInRoS()
    {
        if (distanceToPlayer <= rangeOfSight)
            return true;
        else return false;
    }

    NodeStatus PursuePlayer()
    {
        if (distanceToPlayer > 0.31f)
        {
            Vector3 dir = player.transform.position - transform.position;
            dir.y = 0;
            dir.Normalize();
            Quaternion lookDir = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = lookDir;
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        return NodeStatus.True;
    }

	void Start () {
        player = GlobalRef.playerPos.gameObject.GetComponent<Constitution>();
        SetupBhTree();
	}
	
    public void Hit(float damage)
    {
        bloodParticles.Play();
        health -= damage;
    }

	void Update () {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        tree.UpdateTree();
        if (health <= 0)
        {
            PoolManager.DeSpawn(gameObject, "enemies");
        }
    }

	void OnTriggerStay(Collider col)
	{
		Collider[] collisions = Physics.OverlapSphere (transform.position, 1.0f);

		foreach (Collider c in collisions) {

			if(c.gameObject.name == "Raft")
			{
				c.GetComponent<RaftBehaviour>().Damaged (Time.deltaTime);
			}
		}
	}
}
