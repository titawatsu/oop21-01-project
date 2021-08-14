using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GrabObjectClass
{
    public bool FreezeRotation;
    public float PickupRange = 3f;
    public float ThrowStrength = 50f;
    public float distance = 3f;
    public float maxDistanceGrab = 4f;
}

[System.Serializable]
public class ItemGrabClass
{
    public bool FreezeRotation;
    public float ItemPickupRange = 2f;
    public float ItemThrow = 45f;
    public float ItemDistance = 1f;
    public float ItemMaxGrab = 2.5f;
}

[System.Serializable]
public class DoorGrabClass
{
    public float DoorPickupRange = 2f;
    public float DoorThrow = 10f;
    public float DoorDistance = 2f;
    public float DoorMaxGrab = 3f;
}

[System.Serializable]
public class TagsClass
{
    public string InteractTag = "Interact";
    public string InteractItemsTag = "InteractItem";
    public string DoorsTag = "Door";
}
public class RigidbodyInteracting : MonoBehaviour
{
    public GameObject playerCamera;

    public string GrabButton = "Grab";
    public string ThrowButton = "Throw";
    public string UseButton = "Use";
    public GrabObjectClass ObjectGrab = new GrabObjectClass();
    public ItemGrabClass ItemGrab = new ItemGrabClass();
    public DoorGrabClass DoorGrab = new DoorGrabClass();
    public TagsClass Tags = new TagsClass();

    private float PickupRange = 3f;
    private float ThrowStrength = 50f;
    private float distance = 3f;
    private float maxDistanceGrab = 4f;

    private Ray playerAim;
    private GameObject objectHeld;
    private bool isObjectHeld;
    private bool tryPickupObject;

    void Start(){
        isObjectHeld = false;
        tryPickupObject = false;
        objectHeld = null;

    }
 
    void FixedUpdate(){

        if (Input.GetButton(GrabButton))
        {
            if (!isObjectHeld){

                TryPickObject();

            } else {
                HoldObject();
            }
        } else if (isObjectHeld){
            DropObject();

        }
        if (Input.GetButton(ThrowButton) && isObjectHeld){

            isObjectHeld = false;
            objectHeld.GetComponent<Rigidbody>().useGravity = true;
            ThrowObject();
        }

        if (Input.GetButton(UseButton)){

            isObjectHeld = false;
            TryPickObject();
            tryPickupObject = false;
            Use();
        }
    }

    private void TryPickObject()
    {
        Ray playerAim = playerCamera.GetComponent<Camera>
            ().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast (playerAim, out hit, PickupRange)){
            objectHeld = hit.collider.gameObject;
            if(hit.collider.tag == Tags.InteractItemsTag &&
                tryPickupObject){
                isObjectHeld = true;
                objectHeld.GetComponent<Rigidbody>().useGravity
                    = false;
                if (ObjectGrab.FreezeRotation)
                {
                    objectHeld.GetComponent<Rigidbody>
                        ().freezeRotation = true;

                }
                if(ObjectGrab.FreezeRotation == false)
                {
                    objectHeld.GetComponent<Rigidbody>
                        ().freezeRotation = false;

                }

                PickupRange = ObjectGrab.PickupRange;
                ThrowStrength = ObjectGrab.ThrowStrength;
                distance = ObjectGrab.distance;
                maxDistanceGrab = ObjectGrab.maxDistanceGrab;

            }
            if (hit.collider.tag == Tags.InteractItemsTag &&
                tryPickupObject){

                isObjectHeld = true;
                objectHeld.GetComponent<Rigidbody>().useGravity
                    = true;
                if (ItemGrab.FreezeRotation) {
                    objectHeld.GetComponent<Rigidbody>
                        ().freezeRotation = true;
                }
                if (ItemGrab.FreezeRotation == false)
                {
                    objectHeld.GetComponent<Rigidbody>
                        ().freezeRotation = false;
                }

                PickupRange = ItemGrab.ItemPickupRange;
                ThrowStrength = ItemGrab.ItemThrow;
                distance = ItemGrab.ItemDistance;
                maxDistanceGrab = ItemGrab.ItemMaxGrab;
            }
            if (hit.collider.tag == Tags.DoorsTag &&
                tryPickupObject){

                isObjectHeld = true;
                objectHeld.GetComponent<Rigidbody>
                    ().useGravity = true;
                objectHeld.GetComponent<Rigidbody>
                    ().freezeRotation = false;

                PickupRange = DoorGrab.DoorPickupRange;
                ThrowStrength = DoorGrab.DoorThrow;
                distance = DoorGrab.DoorDistance;
                maxDistanceGrab = DoorGrab.DoorMaxGrab;
            }
        }
    }
    private void HoldObject()
    {
        Ray playerAim = playerCamera.GetComponent<Camera>
            ().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Vector3 nextPos = playerCamera.transform.position +
            playerAim.direction * distance;

        Vector3 currPos = objectHeld.transform.position;

        objectHeld.GetComponent<Rigidbody>().velocity = (nextPos - currPos) * 10;

        if (Vector3.Distance(objectHeld.transform.position,
            playerCamera.transform.position) > maxDistanceGrab){

            DropObject();

        }
    }

    private void DropObject()
    {
        isObjectHeld = false;
        tryPickupObject = false;
        objectHeld.GetComponent<Rigidbody>().useGravity = true;
        objectHeld.GetComponent<Rigidbody>().freezeRotation = false;
        objectHeld = null;
    }

    private void ThrowObject()
    {
        objectHeld.GetComponent<Rigidbody>
            ().AddForce(playerCamera.transform.forward * ThrowStrength);
        objectHeld.GetComponent<Rigidbody>().freezeRotation = false;
        objectHeld = null;
    }

    private void Use()
    {
        objectHeld.SendMessage
            ("UseObject", SendMessageOptions.DontRequireReceiver);
        //Every script attached to the PickupObject that has a UseObject function will be called.
        objectHeld = null;
    }
}
