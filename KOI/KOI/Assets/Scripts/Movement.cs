using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Movement : MonoBehaviour
{
    public static Movement Instance;

    public ParticleSystem partBurst;
    
    [SerializeField] public TextMeshProUGUI hp;
    public TextMeshProUGUI startMessage;
    [SerializeField] public int hitPoints = 100;
    
    [SerializeField] int speed = 10;
    public GameObject fishy;
    private Camera cam;
    
    private Rigidbody2D rb;
    private Renderer rend;
    private Vector3 mousePos;
    private float mousePosX;
    
    private float r = .01f;
    private float g = .18f;
    private float b = .3f;
    private float fc = 1f;
    
    public int scoreCount;
    
    // an abundance of booleans
    public bool died = false;
    public bool noLight = false;
    private bool burst = false;
    private bool pow = true;
    private bool faded = false;
    public bool start = false;
    public bool soundCheckLight = false;
    public bool soundCheckCol = false;
    private bool coStart = true;
    
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        rb = fishy.GetComponent<Rigidbody2D>();
        rend = fishy.GetComponent<Renderer>();
        cam = Camera.main;

        partBurst.Stop();
    }
    
    void Update()
    {
        //  display hit points
        hp.text = "Health: " + hitPoints.ToString();
        if (hitPoints <= 0)
        {
            hp.text = "Health: 0";
        }
        
        // get mouse position
        //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        
        // rotate fish to face mouse
        Vector3 diff = mousePos - transform.position;
        diff.Normalize();
        float rotation_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        fishy.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + 100);
        
        // once player clicks, move fish down, and towards the x coord of mouse
        if (Input.GetMouseButtonDown(0) && !start)
        {
            start = true;
            Time.timeScale = 1;
            Destroy(startMessage);
        }
        if (start)
        {
            Vector2 targ = new Vector2(mousePos.x, fishy.transform.position.y-2);
            fishy.transform.position = Vector2.MoveTowards(transform.position, targ, speed * Time.deltaTime);
            if (coStart)
            {
                StartCoroutine(ChangeFishColor());
                StartCoroutine(AddScore());
                coStart = false;
            }
        }
        
        //check edges
        if (fishy.transform.position.x < -9)
        {
            rb.AddForce(new Vector2(.2f, 0), ForceMode2D.Impulse);
        }
        if (fishy.transform.position.x > 9)
        {
            rb.AddForce(new Vector2(-0.2f, 0), ForceMode2D.Impulse);
        }

        // change color
        rend.material.color = new Color(fc, fc, fc);
        //print(r + ", " + g + ", " + b);

        // make background brighter if player hits light burst
        if (burst)
        {
            r = .01f;
            g = .18f;
            b = .3f;
            fc = 1;
            
            burst = false;
        }
        
        // check lose conditions
        if (hitPoints <= 0)
        {
            died = true;
            Time.timeScale = 0;
        }
        if (fc <= 0 && faded)
        {
            noLight = true;
            Time.timeScale = 0;
        }
    }

    // add score
    public IEnumerator AddScore()
    {
        while (true)
        {
            if (Time.timeScale == 1)
            {
                scoreCount+=1;
                yield return new WaitForSeconds(.1f);
            }
        }
    }
    
    // make fish slowly darker
    IEnumerator ChangeFishColor()
    {
        while (true)
        {
            fc -= .005f;
            yield return new WaitForSeconds(.1f);
        }
    }
    // make background slowly darker
    IEnumerator ChangeColor()
    {
        while (true)
        {
            if (!burst)
            {
                r -= .005f;
                g -= .005f;
                b -= .005f;
            }
            cam.backgroundColor = new Color(r, g, b);
            yield return new WaitForSeconds(.2f);
        }
    }

    // take damage or flag light collision
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "light")
        {
            faded = true;
            if (pow)
            {
                // start dimming background
                StartCoroutine(ChangeColor());
                pow = false;
            }
            scoreCount+=10000;
            burst = true;
            soundCheckLight = true;
            partBurst.Play();
        }
        else
        {
            soundCheckCol = true;
            hitPoints -= 5;
        }
    }
}
