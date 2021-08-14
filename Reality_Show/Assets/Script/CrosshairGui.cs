using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairGui : MonoBehaviour
{
    public Texture2D crosshairTexture;
    public Texture2D useTexture;
    public float RayLength = 3f;

    public bool DefaultReticle;
    public bool UseReticle;
    public bool ShowCursor = false;

    private bool IsCrosshairVisible = true;
    private Rect crosshairRect;
    private Ray playerAim;
    private Camera playerCamera;

    void Update() {

        playerCamera = Camera.main;
        Ray playerAim = playerCamera.GetComponent<Camera>
            ().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(playerAim, out hit, RayLength))
        {

            if (hit.collider.gameObject.tag == "Interact")
            {

                DefaultReticle = false;
                UseReticle = true;

            }
            if (hit.collider.gameObject.tag == "InteractItem")
            {

                DefaultReticle = false;
                UseReticle = true;

            }
            if (hit.collider.gameObject.tag == "Door")
            {

                DefaultReticle = false;
                UseReticle = true;
            }

        }
        else{
            DefaultReticle = true;
            UseReticle = false;
        }
        if (Input.GetKeyDown(KeyCode.C)){

            ShowCursor = !ShowCursor;

        }
        if (ShowCursor){

            Cursor.visible = (true);
            Cursor.lockState = CursorLockMode.None;

        }
        else{

            Cursor.visible = (false);
            Cursor.lockState = CursorLockMode.Locked;

        }
    }

    private void Awake(){
        if (DefaultReticle) {

            crosshairRect = new Rect((Screen.width - crosshairTexture.width) / 2,
                                (Screen.height - crosshairTexture.height) / 2,
                                crosshairTexture.width,
                                crosshairTexture.height);
        }

        if (UseReticle){
            
            crosshairRect = new Rect((Screen.width - useTexture.width) / 2,
                                (Screen.height - useTexture.height) / 2,
                                useTexture.width,
                                useTexture.height);

        }
    }

    private void OnGUI(){

        if (IsCrosshairVisible)
        {
            if (DefaultReticle)
            {
                GUI.DrawTexture(crosshairRect, crosshairTexture);

            }
            if (UseReticle)
            {
                GUI.DrawTexture(crosshairRect, useTexture);
            }
        }
    }
}
