using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Rat : MonoBehaviour
{

    //Variables
    public int facing = 1; //to the right
    public float xSpeed; //0.1
    public float xBuffer; //18.5
    private SpriteRenderer sprite;




    private AIState currentState = AIState.Wander;
    public enum AIState
    {
        Wander,
        Sniff,
        Eat
    }

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
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

                break;
            case AIState.Sniff:

                break;
            case AIState.Eat:

                break;
        }
    }

    private void UpdateState()
    {
        switch (currentState)
        {
            case AIState.Wander:
                transform.position += new Vector3(xSpeed * facing, 0, 0);

                if (transform.position.x > xBuffer || transform.position.x < -xBuffer) TurnAround();

                break;
            case AIState.Sniff:
                
                break;
            case AIState.Eat:
                
                break;
        }
    }

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
                break;
            case -1:
                sprite.flipX = true;
                break;
                
        
        }
    }

}
