using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    Vector2 movement;
    public float speed;
    public GameObject miniMap;
    public Transform attackPoint;
    public GameObject death_panel;

    public AudioManager audio;

    public float attackRange = .3f;
    public float attackRate = .5f;

    float nextAttackTime = 0f;

    public LayerMask enemyLayers;

    public int attackDamage = 40;

    public int full_health = 100;

    public int current_health;

    public TMP_Text health_value;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        current_health = full_health;
    }

    void Update()
    {
        health_value.text = "Current Health: " + current_health.ToString();
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x != 0) transform.localScale = new Vector3(movement.x, 1, 1);
        openMap(miniMap);
        SwitchAnim();

        if (Time.time >= nextAttackTime) if (Input.GetMouseButtonDown(0)) { audio.PlayAudio_Effect(audio.ClickClip); attack(); nextAttackTime = Time.time + attackRate; }
        if (current_health <= 0) Die();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        if (movement.sqrMagnitude != 0) audio.PlayAudio_Effect(audio.movingClip);
    }

    void SwitchAnim()
    {
        anim.SetFloat("speed", movement.magnitude);
    }

    void openMap(GameObject map)
    {
        if (Input.GetKey(KeyCode.M))
        {
            map.SetActive(true);
        }
        else map.SetActive(false);
    }

    public void takeDamage(int damage)
    {
        current_health -= damage;
        //play hurt animation
        if (current_health <= 0) Die();
        //else anim.SetTrigger("Hurt");
    }

    void attack()
    {
        //play the animation
        anim.SetTrigger("Attack");
        //detect all the enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().takeDamage(attackDamage);
            audio.PlayAudio_Effect(audio.hurtclip);
        }
    }

    void Die()
    {
        death_panel.SetActive(true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        anim.SetTrigger("Death");
    }

    private void OnDrawGizmos()
    {
        if(attackPoint)Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
