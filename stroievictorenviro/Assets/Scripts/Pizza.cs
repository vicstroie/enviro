using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements.Experimental;
using Random = UnityEngine.Random;

public class Pizza : MonoBehaviour
{
    //variables
    private float randomY;
    private int sign;
    private bool dropped;
    public bool chosen;
    public float speed;

    //sprite stuff
    private SpriteRenderer sr;
    public Sprite moldy;
    private bool molded;
    private int timer;






    private AIState currentState = AIState.Dropped;
    public enum AIState
    {
       Dropped, 
        Wait,
        Eaten,
        Mold
    }

    
    void Start()
    {
        
        sr = this.GetComponent<SpriteRenderer>();
        StartState(AIState.Dropped);
        chosen = false;
        molded = false;
        timer = 0;
    }

    
    void Update()
    {
        UpdateState();
    }

    
    public void StartState(AIState newState)
    {
        EndState(currentState);

        switch (newState)
        {
            case AIState.Dropped:
                randomY = Random.Range(-9.5f, -4);
                currentState = AIState.Dropped;
                break;
            case AIState.Wait:
                currentState = AIState.Wait;

                break;
            case AIState.Eaten:
                currentState = AIState.Eaten;

                break;
            case AIState.Mold:
                randomY = Random.Range(-9.5f, -4);
                int rand = Random.Range(0, 2);
                if (rand == 0) { sign = 1; } else if (rand == 1) {  sign = -1; }
                sr.sprite = moldy;
                sr.flipX = true;
                transform.localScale = Vector3.one * 0.8f;
                Debug.Log("moldy!");
                currentState = AIState.Mold;
                
                break;
        }
    }

    private void UpdateState()
    {
        switch (currentState)
        {
            case AIState.Dropped:
                transform.position += new Vector3(0, -0.1f, 0);

                if (transform.position.y <= randomY)
                {
                    StartState(AIState.Wait);
                }
                break;
            case AIState.Wait:
               
                if(molded) StartState(AIState.Mold);

                break;
            case AIState.Eaten:

                break;
            case AIState.Mold:
                transform.position += new Vector3(randomY, speed * sign, 0) * Time.deltaTime;
                if(transform.position.x > 20 || transform.position.x < -20)
                {
                    Manager.reference.GetComponent<Manager>().pizzaNum--;
                    Destroy(this.gameObject);
                    
                }
                break;
        }
    }

    private void EndState(AIState oldState)
    {
        switch (oldState)
        {
            case AIState.Wait:
                randomY = 0;
                break;
            case AIState.Eaten:

                break;
            case AIState.Mold:

                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "rat" && collision.gameObject.GetComponent<Rat>().currentState == Rat.AIState.Sniff )
        {
            collision.gameObject.GetComponent<Rat>().FoundFood(this.gameObject);
            chosen = true;
            Debug.Log("sniffed!");

        }
        Debug.Log("triggered!");
    }


    private void FixedUpdate()
    {
        if(currentState == AIState.Wait)
        {
            timer++;
            if (timer > 240)
            {
                molded = true;
            }
        }
        
    }

}
