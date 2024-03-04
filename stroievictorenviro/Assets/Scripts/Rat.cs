using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class Rat : MonoBehaviour
{

    //Variables
    public int facing = 1; //to the right
    public int rotFacing = 1;
    public float xSpeed; //0.1
    public float xBuffer; //18.5
    private SpriteRenderer sprite;
    

    float randX;
    float randY;
    float xpos;
    float ypos;
    Vector3 MovePosition;
    Vector3 dir;
    public Vector3 maxUpRot = new Vector3(0, 0, 20);
    public Vector3 maxDownRot = new Vector3(0, 0, 20);
    private int rotCount;
    private int sniffCount;
    int pizzaCount;
    bool leave;

    private GameObject pizza;




    public AIState currentState;
    public enum AIState
    {
        Test,
        Wander,
        Sniff,
        Searching,
        Eat,
        Leave
    }

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        StartState(AIState.Wander);
        leave = false;
        pizzaCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }

    //This is the only method your code really cares about
    public void StartState(AIState newState)
    {
        EndState(currentState);

        switch (newState)
        {
            case AIState.Wander:

                

                if(leave)
                {
                    int rand = Random.Range(0, 2);
                    int sign = 1;


                    if (rand == 0)
                    {
                        sign = 1;
                    }
                    else
                    {
                        sign = -1;
                    }

                    randX = 23 * sign;


                    randY = Random.Range(ypos - 3, ypos + 3);
                    if (randY < -9.5f) randY = -9.5f;
                    if (randY > -3.8f) randY = -3.8f;
                } else
                {
                    randX = Random.Range(xpos - 10, xpos + 10);
                    if (randX < -17) randX = -17;
                    if (randX > 17) randX = 17;
                    randY = Random.Range(ypos - 3, ypos + 3);
                    if (randY < -9.5f) randY = -9.5f;
                    if (randY > -3.8f) randY = -3.8f;
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

                currentState = AIState.Wander;
                break;
            case AIState.Sniff:
                sniffCount = 0;
                currentState = AIState.Sniff;
                break;
            case AIState.Searching:

                randX = pizza.transform.position.x;
                randY = pizza.transform.position.y;

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

                currentState = AIState.Searching;
                break;
            case AIState.Eat:
                rotCount = 0;
                sniffCount = 0;
                pizza.GetComponent<Pizza>().StartState(Pizza.AIState.Eaten);
                currentState = AIState.Eat;
                break;
        }
    }

    private void UpdateState()
    {
        switch (currentState)
        {
            case AIState.Test:
                if(Input.GetKey(KeyCode.D))
                {
                    transform.position += new Vector3(xSpeed, 0, 0) * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.position += new Vector3(-xSpeed, 0, 0) * Time.deltaTime;
                }
                break;




            case AIState.Wander:
              

                xpos = transform.position.x;
                ypos = transform.position.y;
               

                if ((xpos < randX + 2 && xpos > randX - 2) && (ypos < randY + 1.5 && ypos > randY - 1.5))
                {
                    
                    if(leave)
                    {
                        Manager.reference.GetComponent<Manager>().ratNum--;
                        Destroy(this.gameObject);
                    } else
                    {
                        StartState(AIState.Sniff);
                    }
                        
                    
                } else 
                {
                    transform.position += dir * xSpeed * Time.deltaTime;
                }
               

                switch (facing)
                {

                    case 1:
                        sprite.flipX = false;
                        //randX = -16;
                        break;
                    case -1:
                        sprite.flipX = true;
                        //randX = 16;
                        break;
                }


                if (transform.position.x > xBuffer || transform.position.x < -xBuffer) facing *= -1;

                break;
            case AIState.Sniff:



                //rotate counterclockwise

                if(rotFacing == 1)
                {
                    transform.Rotate(Vector3.forward, 0.5f);

                } else {
                    
                    transform.Rotate(Vector3.back, 0.5f);
                    rotCount++;
                }

                
                if(rotCount > 40)
                {
                    rotFacing = 1;
                    rotCount = 0;
                    
                    sniffCount++;
                }
              
                if(transform.rotation.eulerAngles.z > 30)
                {
                    rotFacing = -1;
                }




                if (sniffCount > 3) {
                    
                    StartState(AIState.Wander);
                }
                
                break;

            case AIState.Searching:
                xpos = transform.position.x;
                ypos = transform.position.y;
                //Debug.Log(xpos + ", " + ypos + "Rand: " + randX + ", " + randY);

                if ((xpos < randX + 0.5 && xpos > randX - 0.5) && (ypos < randY + 0.3 && ypos > randY - 0.3))
                {
                        StartState(AIState.Eat);
                        

                }
                else
                {
                    transform.position += (dir * xSpeed) * Time.deltaTime;
                }
                break;
            case AIState.Eat:
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
                    //Debug.Log("turned");
                    sniffCount++;
                }




                //rotate clockwise


                if (transform.rotation.eulerAngles.z > 30)
                {
                    rotFacing = -1;
                    //Debug.Log(transform.rotation.eulerAngles.z);
                }

                if (sniffCount > 3)
                {
                    pizzaCount++;
                    if (pizzaCount >= 3) leave = true;
                    StartState(AIState.Wander);
                }

                break;
        }
    }


    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == pizza && currentState == AIState.Eat)
        {
            collision.gameObject.GetComponent<Pizza>().StartState(Pizza.AIState.Eaten);
        }
    }
    */

    private void EndState(AIState oldState)
    {
        switch (oldState)
        {
            case AIState.Wander:
                
                break;
            case AIState.Sniff:
                
                break;
            case AIState.Eat:
                
                break;
        }
    }


    public void TurnAround()
    {
        facing *= -1;
        
        switch(facing) {

            case 1:
                sprite.flipX = false;
                randX = -16;
                break;
            case -1:
                sprite.flipX = true;
                randX = 16;
                break;
                
        
        

        }
    }


    public void FoundFood(GameObject targetPizza)
    {
        //if (currentState == AIState.Sniff) { 
        //if (!targetPizza.gameObject.GetComponent<Pizza>().chosen)

                pizza = targetPizza;
                StartState(AIState.Searching);
                pizza.GetComponent<Pizza>().chosen = true;
                //Debug.Log("found!");


        //}
    }

    
    


    public AIState GetState()
    {
        return currentState;
    }

}
