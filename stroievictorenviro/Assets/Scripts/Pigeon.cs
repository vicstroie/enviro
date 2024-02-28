using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements.Experimental;

public class Pigeon : MonoBehaviour
{
    private AIState currentState = AIState.Fly;
    public enum AIState
    {
        Fly, //Fly around
        Roost, //Sit on pipe
        Eat
    }

   
    void Start()
    {
       
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

                break;
            case AIState.Roost:

                break;
            case AIState.Eat:

                break;
        }
    }

    private void UpdateState()
    {
        switch (currentState)
        {
            case AIState.Fly:
                

                break;
            case AIState.Roost:

                break;
            case AIState.Eat:

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
