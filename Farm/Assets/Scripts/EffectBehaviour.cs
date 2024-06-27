using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBehaviour : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(DeleteEffect());
    }

    private IEnumerator DeleteEffect() {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }

}
