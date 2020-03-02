using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement_del : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
