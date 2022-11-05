using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public Transform enemySprite;
    public Transform target;
    public int maximum_health = 100;
    int currrent_health;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float attackRange = 0.3f;
    public float detectRange = 1;
    public Transform attackPoint;
    public LayerMask playerLayer;

    float nextattackTime = 0;
    public float attack_CD = 1.2f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndofPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    public AudioManager audio;

    public Animator anim;
    public int attack_damage = 20;
    void Start()
    {
        currrent_health = maximum_health;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        audio = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioManager>();


        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null) return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndofPath = true;
            return;
        }
        else
        {
            reachedEndofPath = false;
        }

        Vector2 direction =((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);

        if (Vector2.Distance((Vector2)path.vectorPath[path.vectorPath.Count - 1], rb.position) <= detectRange) 
        {
            anim.SetFloat("walking", 0);
            if(Time.time >= nextattackTime)
            {
                attack();
                nextattackTime = Time.time + attack_CD;
            }   
        }
        else anim.SetFloat("walking", Mathf.Abs(direction.magnitude));

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance) currentWaypoint++;
        if (force.x >= 0.01f)enemySprite.localScale = new Vector3(1f, 1f, 1f);
        else if(force.x <= -0.01f) enemySprite.localScale = new Vector3(-1f, 1f, 1f);

    }

    public void takeDamage(int damage)
    {
        currrent_health -= damage;
        //play hurt animation
        if (currrent_health <= 0) Die();
        else anim.SetTrigger("Hurt");
    }

    void Die()
    {
        //Die animation
        anim.SetTrigger("IsDead");
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    void attack() 
    {
        //play animation
        anim.SetTrigger("Attack");
        //check if player is inside
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        //take damage
        foreach (Collider2D player in hitPlayers)
        {
            if (player.CompareTag("Player"))
            {
                player.GetComponent<PlayerController>().takeDamage(attack_damage);
            } 
        }
    }

    private void OnDrawGizmos()
    {
        if (attackPoint) Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
