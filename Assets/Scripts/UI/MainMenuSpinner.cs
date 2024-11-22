using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSpinner : MonoBehaviour
{
    private Vector3 rotat;
    private Vector3 currentRotat;
    
    // Start is called before the first frame update
    void Start()
    {
        rotat = new Vector3(Random.Range(1, 5), Random.Range(1, 5), Random.Range(1, 5));
        currentRotat = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        currentRotat += rotat;
        transform.eulerAngles = currentRotat;
    }
}
