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
                if (Vector3.Distance(TransformCamera.position, transform.position) <= distanceRange)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Destroy(gameObject);

                        UiPickUpObj.SetActive(false);
                        ScoreManager.instance.AddScore();
                    }
                }
            }
        }
    }
    
    /*
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UiPickUpObj.SetActive(true);
            StartCoroutine("WaitForSec");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(gameObject);

                UiPickUpObj.SetActive(false);
                ScoreManager.instance.AddScore();
            }
        }
    }*/
    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(1);
        UiPickUpObj.SetActive(false);
    }
    /*
    void DestroyWithTag(string destroyTag)
    {
        GameObject[] destroyObject;
        destroyObject = GameObject.FindGameObjectsWithTag(destroyTag);
        foreach (GameObject oneObject in destroyObject)
            Destroy(oneObject);
    }
    */
}
