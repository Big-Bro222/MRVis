using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("selfDestroy", 0.5f);
    }

    private void selfDestroy()
    {
        Destroy(gameObject);
    }
}
