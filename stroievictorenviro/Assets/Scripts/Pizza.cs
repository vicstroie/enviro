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
    private bool dropped;
    public bool chosen;

    //sprite stuff
    private SpriteRenderer sr;
    public Sprite moldy;






    private AIState currentState = AIState.Wait;
    public enum AIState
    {
        Wait,
        Eaten,
        Mold
    }

    
    void Start()
    {
        randomY = Random.Range(-9.5f, -4);
        sr = this.GetComponent<SpriteRenderer>();
        dropped = true;
        chosen = false;
    }

    
    void Update()
    {
        UpdateState();
        if(dropped)
        {
            transform.position += new Vector3(0, -0.1f, 0);

            if(transform.position.y <= randomY)
            {
                dropped = false;
            }
        }
    }

    
    public void StartState(AIState newState)
    {
        EndState(currentState);

        switch (newState)
        {
            case AIState.Wait:
                currentState = AIState.Wait;

                break;
            case AIState.Eaten:
                currentState = AIState.Eaten;

                break;
            case AIState.Mold:
                currentState = AIState.Mold;
                sr.sprite = moldy;
                break;
        }
    }

    private void UpdateState()
    {
        switch (currentState)
        {
            case AIState.Wait:
               

                break;
            case AIState.Eaten:

                break;
            case AIState.Mold:

                break;
        }
    }

    private void EndState(AIState oldState)
    {
        switch (oldState)
        {
            case AIState.Wait:

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


}
