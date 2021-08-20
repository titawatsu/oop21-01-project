using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObj : MonoBehaviour
{

    public Transform TransformCamera;
    public LayerMask RayMask;

    private Transform currentTransform;

    private float length;

    private RaycastHit hit;

    bool guiShow = false;


    private void Update()
    {
        CrosshairGui Reticle = this.GetComponent<CrosshairGui>();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            if (Physics.Raycast(TransformCamera.position, TransformCamera.forward, out hit, 3f, RayMask))
            {

                if (hit.transform.CompareTag("PickableObject"))
                {
                    guiShow = true;
                    SetNewTransform(hit.transform);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RemoveTransform();
            guiShow = false;
        }
        if (currentTransform)
        {
            MoveTransformAround();

        } else {

            guiShow = false;
        }

        if (guiShow == true)
        {

            Reticle.UseReticle = true;
            Reticle.DefaultReticle = false;

        }
        if (guiShow == false)
        {

            Reticle.UseReticle = false;
            Reticle.DefaultReticle = true;

        }
    }

    public void SetNewTransform(Transform newTransfrom)
    {

        if (currentTransform)
            return;
        currentTransform = newTransfrom;

        length = Vector3.Distance(TransformCamera.position, newTransfrom.position);

        currentTransform.GetComponent<Rigidbody>().isKinematic = true;
        currentTransform.GetComponent<Rigidbody>().useGravity = false;
        currentTransform.GetComponent<Rigidbody>().detectCollisions = true;
    }

    private void MoveTransformAround()
    {
        currentTransform.position = TransformCamera.position + TransformCamera.forward * length;

    }

    public void RemoveTransform()
    {
        if (!currentTransform)
            return;

        currentTransform.GetComponent<Rigidbody>().isKinematic = false;
        currentTransform.GetComponent<Rigidbody>().useGravity = true;

        currentTransform = null;
    }
}
