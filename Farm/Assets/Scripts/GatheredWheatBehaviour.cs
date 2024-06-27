using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatheredWheatBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
