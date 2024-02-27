using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{

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
}
