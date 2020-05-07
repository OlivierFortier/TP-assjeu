using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttaquerArme : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision cibleTouche) {

        if(cibleTouche.gameObject.CompareTag("ennemi")) {
            print("ayoye ca fait mal");
        }
        
    }
}
