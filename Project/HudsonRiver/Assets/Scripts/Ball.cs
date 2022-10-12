using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float xImpulse = 2f;
    [SerializeField] float yImpulse = 15f;
    [SerializeField] Vector2 size = new Vector3(0.6f,0.6f);
    [SerializeField] float decreaseRate_Size = 0.02f;
    [SerializeField] float decreaseRate_Mass = 0.02f;
    [SerializeField] float shrinkThreshold_size = 0.04f;
    [SerializeField] float shrinkThreshold_mass = 0.04f;
    Rigidbody2D _rigid;
    public  bool start = false;
    public  bool playing = false;
    public bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
         _rigid = GetComponent<Rigidbody2D>();
        this.transform.localScale = size;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) & !playing) start = true;
        if (start) playBall();
        if (activated) _rigid.bodyType = RigidbodyType2D.Dynamic;
        if (transform.localScale.x <= shrinkThreshold_size || _rigid.mass < shrinkThreshold_mass) Destroy(this.gameObject);
    }

    void playBall()
    {
        start = false;
        _rigid.velocity = new Vector2(xImpulse, yImpulse);
        playing = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(playing) 
        {
            size -= new Vector2(decreaseRate_Size, decreaseRate_Size);
            _rigid.mass -= decreaseRate_Mass;
            transform.localScale = size;
        }
        if (collision.gameObject.CompareTag("Ball"))
        {
            activated = true;
        }
    }
}
