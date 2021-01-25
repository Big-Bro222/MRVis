using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableReceiver : MonoBehaviour
{
    private Color onhovercolor;
    private Color defaultcolor;
    // Start is called before the first frame update

    private void Start()
    {
        onhovercolor = Color.red;
        defaultcolor = Color.yellow;
        GetComponent<MeshRenderer>().material.color = defaultcolor;
    }
    public void interact(bool isOnHover)
    {
        GetComponent<MeshRenderer>().material.color = isOnHover ? onhovercolor : defaultcolor;
    }
}
