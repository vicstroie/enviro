using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements.Experimental;
using Random = UnityEngine.Random;

public class Pigeon : MonoBehaviour
{

    private float randX;
    private float randY;
    float xpos;
    float ypos;
    Vector3 MovePosition;
    Vector3 dir;
    int facing;
    public float xBuffer;
    public float xSpeed;
    private SpriteRenderer sr;
    public Sprite sitting;
    public Sprite flying;
    public Sprite eat;
    int timer = 0;
    bool leave = false;


    private AIState currentState = AIState.Fly;





    public enum AIState
    {
        Fly, //Fly around
        Land,
        Roost, //Sit on pipe
        Eat,
        Walk
    }

   
    void Start()
    {
        facing = 1;
        xBuffer = 18.5f;
        sr = gameObject.GetComponent<SpriteRenderer>();
        StartState(AIState.Fly);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }

    
    public void StartState(AIState newState)
    {
        EndState(currentState);

        switch (newState)
        {
            case AIState.Fly:

                if(leave)
                {
                    int rand = Random.Range(0, 2);
                    int sign = 1;


                    if(rand == 0)
                    {
                        sign = 1;
                    } else
                    {
                        sign = -1;
                    }

                    randX = 23 * sign;

                    randY = Random.Range(ypos - 3, ypos + 3);
                    if (randY > 9.5f) randY = 9.5f;
                    if (randY < 0) randY = 0;
                } else
                {
                    randX = Random.Range(xpos - 10, xpos + 10);
                    if (randX < -17) randX = -17;
                    if (randX > 17) randX = 17;
                    randY = Random.Range(ypos - 3, ypos + 3);
                    if (randY > 9.5f) randY = 9.5f;
                    if (randY < 0) randY = 0;
                }


                MovePosition = new Vector3(randX, randY, 0);



                dir = (MovePosition - transform.position).normalized;

                if (dir.x > 0)
                {
                    facing = 1;
                }
                else
                {
                    facing = -1;
                }

                sr.sprite = flying;
                currentState = newState;
                transform.localScale = Vector3.one * 0.5f;
                break;

            case AIState.Land:
                randX = Random.Range(-13, 2);

                randY = 1.8f;


                MovePosition = new Vector3(randX, randY, 0);



                dir = (MovePosition - transform.position).normalized;

                if (dir.x > 0)
                {
                    facing = 1;
                }
                else
                {
                    facing = -1;
                }
                currentState = newState;
                break;
            case AIState.Roost:
                sr.sprite = sitting;
                transform.localScale = Vector3.one * 0.2f;
                currentState = newState;

                break;
            case AIState.Eat:
                timer = 0;
                currentState = newState;
                break;

            case AIState.Walk:
                if(transform.position.x >= -7)
                {
                    randX = transform.position.x - 3;
                    facing = -1;
                }
                else if(transform.position.x < -7) {
                    randX = transform.position.x + 3;
                    facing = 1;
                }
                if (randX > 2) randX = 2;
                if (randX < -13) randX = -13;
                randY = transform.position.y;

                MovePosition = new Vector3(randX, randY, 0);



                dir = (MovePosition - transform.position).normalized;

                sr.sprite = sitting;
                transform.localScale = Vector3.one * 0.2f;
                currentState = newState;
                break;

        }
    }

    private void UpdateState()
    {
        switch (currentState)
        {
            case AIState.Fly:

                xpos = transform.position.x;
                ypos = transform.position.y;

                if ((xpos < randX + 0.5 && xpos > randX - 0.5) && (ypos < randY + 0.3 && ypos > randY - 0.3))
                {

                    if(leave)
                    {
                        Manager.reference.GetComponent<Manager>().pigeonNum--;
                        Destroy(this.gameObject);
                    } else
                    {
                        int rand = Random.Range(0, 5);
                        Debug.Log(rand);
                        if (rand == 3)
                        {
                            StartState(AIState.Land);
                        }
                        else
                        {
                            StartState(AIState.Fly);
                        }
                    }


                    
                    
                }
                else
                {
                    transform.position += dir * xSpeed * Time.deltaTime;
                }


                switch (facing)
                {

                    case 1:
                        sr.flipX = false;
                        //randX = -16;
                        break;
                    case -1:
                        sr.flipX = true;
                        //randX = 16;
                        break;
                }


                if (transform.position.x > xBuffer || transform.position.x < -xBuffer) facing *= -1;

                break;
            case AIState.Land:
                xpos = transform.position.x;
                ypos = transform.position.y;

                if ((xpos < randX + 0.5 && xpos > randX - 0.5) && (ypos < randY + 0.3 && ypos > randY - 0.3))
                {
                   StartState(AIState.Roost);
                }
                else
                {
                    transform.position += (dir * xSpeed) * Time.deltaTime;
                }




                break;
            case AIState.Roost:

                timer++;
                if(timer > 240)
                {
                    timer = 0;
                    StartState(AIState.Eat);
                }


                break;
            case AIState.Eat:
                    sr.sprite = eat;
                    transform.localScale = Vector3.one * 0.125f;
                    timer++;
                    if(timer > 360)
                    {
                        StartState(AIState.Walk);
                    }
                
                

                  break;
            case AIState.Walk:
                xpos = transform.position.x;
                ypos = transform.position.y;

                if (xpos < randX + 0.5 && xpos > randX - 0.5)
                {
                    int rand = Random.Range(0, 3);

                    if(rand == 1)
                    {
                        leave = true;
                        StartState(AIState.Fly);
                        

                    } else
                    {
                        StartState(AIState.Eat);
                    }
                } else
                {
                    transform.position += dir * xSpeed * Time.deltaTime;
                }
                    break;
        }
    }

    private void EndState(AIState oldState)
    {
        switch (oldState)
        {
            case AIState.Fly:

                break;
            case AIState.Roost:

                break;
            case AIState.Eat:

                break;
        }
    }

}
