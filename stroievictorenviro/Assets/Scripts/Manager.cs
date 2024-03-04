using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Manager : MonoBehaviour
{
    //prefabs
    public GameObject ratPrefab;
    public GameObject pizzaPrefab;
    public GameObject pigeonPrefab;


    public int ratNum = 0;
    public int pizzaNum = 0;
    public int pigeonNum = 0;

    public int ratTimer;
    public int pizzaTimer;
    public int pigeonTimer;

    public static Manager reference;
    // Start is called before the first frame update
    void Start()
    {
        //all public methods and variables will now be accessible using Manager.reference
        reference = this;
        
        ratTimer = 0;
        pizzaTimer = 0;
        pigeonTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {

        
        
        if(ratNum < 2)
        {
            if (ratTimer > 240)
            {
                SpawnRat();
                ratTimer = 0;
            }
            ratTimer++;

        }

        if (pizzaNum < 3)
        {
            if(pizzaTimer > 720)
            {
                SpawnPizza();
                pizzaTimer = 0;
            }
            pizzaTimer++;
        }

        if (pigeonNum < 2)
        {
            if (pigeonTimer > 240)
            {
                SpawnPigeon();
                pigeonTimer = 0;
            }
            pigeonTimer++;

        }
        
    }

    //Is the return type necessary?
    private GameObject SpawnRat()
    {
        int randSign = Random.Range(0, 2);
        
        float randY = Random.Range(-9.5f, -4f);
        int sign = -1;

        Vector3 ratPos = new Vector3 (17 * sign, randY, transform.position.z);
        GameObject rat = GameObject.Instantiate(ratPrefab, ratPos, Quaternion.identity);


        ratNum++;
        return rat; //Take out?
    }

    private void SpawnPizza()
    {
        float randX = Random.Range(-17, 17);
        Vector3 pizzaPos = new Vector3(randX, transform.position.y, transform.position.z);
        GameObject.Instantiate(pizzaPrefab, pizzaPos, Quaternion.identity);
        pizzaNum++;
    }

    private void SpawnPigeon() {
        float randX = Random.Range(-17, 17);
        Vector3 pigeonPos = new Vector3(randX, transform.position.y + 7, transform.position.z);
        GameObject.Instantiate(pigeonPrefab, pigeonPos, Quaternion.identity);
        pigeonNum++;
    }    
}
