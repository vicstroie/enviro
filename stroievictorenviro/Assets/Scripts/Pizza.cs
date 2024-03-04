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
    private int sign = 1;
    private bool dropped;
    public bool chosen;
    public float speed;

    //sprite stuff
    private SpriteRenderer sr;
    public Sprite moldy;
    private bool molded;
    bool waiting;
    private int timer;

    private int rotFacing;
    int rotCount;
    int munchCount;

    AudioSource aS;





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
        aS = this.GetComponent<AudioSource>();
        StartState(AIState.Dropped);
        chosen = false;
        molded = false;
        waiting = false;
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

                rotCount = 0;
                munchCount = 0;
                rotFacing = 1;
                currentState = AIState.Eaten;

                break;
            case AIState.Mold:
                randomY = Random.Range(-9.5f, -4);
                int rand = Random.Range(0, 2);
                sign = 1;


                if (transform.position.x > 0)
                {
                    sign = 1;
                }
                else
                {
                    sign = -1;
                }
                sr.sprite = moldy;
                rotFacing = 1;
                rotCount = 0;
                sr.flipX = true;
                transform.localScale = Vector3.one * 0.8f;
                


                rotCount = 0;


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


                if (rotFacing == 1)
                {
                    transform.Rotate(Vector3.forward, 0.5f);

                }
                else
                {

                    transform.Rotate(Vector3.back, 0.5f);
                    rotCount++;
                }


                if (rotCount > 40)
                {
                    rotFacing = 1;
                    rotCount = 0;

                    munchCount++;
                }

                if (transform.rotation.eulerAngles.z > 30)
                {
                    rotFacing = -1;

                }




                if (munchCount > 3)
                {

                    Destroy(this.gameObject);
                    Manager.reference.GetComponent<Manager>().pizzaNum--;
                    Destroy(this);
                }





                break;
            case AIState.Mold:
                 transform.position += new Vector3(speed * Time.deltaTime * sign, 0, 0);

                



                if (rotFacing == 1)
                {
                    transform.Rotate(Vector3.forward, 0.1f);
                    rotCount++;
                }
                else
                {

                    transform.Rotate(Vector3.back, 0.1f);
                    rotCount++;
                }

                if (rotCount > 200) {
                    rotCount = 0;
                    rotFacing *= -1;
                    aS.Play();
                
                }

                if (transform.position.x > 20 || transform.position.x < -20)
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
        if(!chosen && collision.gameObject.CompareTag("rat") && currentState == AIState.Wait)
        {
            collision.gameObject.GetComponent<Rat>().FoundFood(this.gameObject);
            waiting = true;
            
        

        }

        
    }


    private void FixedUpdate()
    {
        if(currentState == AIState.Wait && !waiting)
        {
            timer++;
            if (timer > 360)
            {
                molded = true;
                timer = 0;
            }
        }
        
    }

}
