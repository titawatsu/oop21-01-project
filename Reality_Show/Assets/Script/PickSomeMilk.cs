using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickSomeMilk : MonoBehaviour
{
    [SerializeField] private float distanceRange = 3f;

    public Transform TransformCamera;
    public LayerMask RayMask;

    private RaycastHit hit;
    public GameObject UiPickUpObj;

    void Start()
    {
        UiPickUpObj.SetActive(false);
    }


    void Update()
    {
        if (Physics.Raycast(TransformCamera.position, TransformCamera.forward, out hit, distanceRange, RayMask))
        {

            if (hit.transform.CompareTag("Item"))
            {
                UiPickUpObj.SetActive(true);
                StartCoroutine("WaitForSec");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(this.gameObject);
                    
                    UiPickUpObj.SetActive(false);
                    ScoreManager.instance.AddScore();
                }
            }
        }
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(1);
        UiPickUpObj.SetActive(false);
    }
}
