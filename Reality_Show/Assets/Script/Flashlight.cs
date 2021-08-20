using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public bool isActive = false;
    public bool delay = false;

    [SerializeField] GameObject lightSource;
    public AudioSource clickS;

    void Start(){

        lightSource.gameObject.SetActive(false);
    }


    void Update() {

        if (Input.GetKeyDown(KeyCode.F)) {

            if (isActive == false) {

                lightSource.gameObject.SetActive(true);
                isActive = true;
            } else {

                lightSource.gameObject.SetActive(false);
                isActive = false;
            }
        }
    }
}
