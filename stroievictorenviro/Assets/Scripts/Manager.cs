using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager reference;
    // Start is called before the first frame update
    void Start()
    {
        //all public methods and variables will now be accessible using Manager.reference
        reference = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
