using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickSomeMilk : MonoBehaviour
{
    [SerializeField] private float distanceRange = 5f;
    public int score = 0;

    public Transform TransformCamera;
    public LayerMask RayMask;

    private Transform currentTransform;

    private float length;

    private RaycastHit hit;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            if (Physics.Raycast(TransformCamera.position, TransformCamera.forward, out hit, 3f, RayMask))
            {

                if (hit.transform.CompareTag("Item"))
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
