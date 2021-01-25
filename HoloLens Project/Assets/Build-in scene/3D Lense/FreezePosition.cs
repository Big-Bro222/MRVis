using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePosition : MonoBehaviour
{
    public bool islock;
    private Vector3 currentPos;

    public void Lock(bool lockstate)
    {
        islock = lockstate;
    }

    public void LockAB()
    {
        islock = !islock;
    }

    // Start is called before the first frame update

}
