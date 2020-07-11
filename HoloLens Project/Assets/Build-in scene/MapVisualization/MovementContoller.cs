using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementContoller : MonoBehaviour
{
    // Start is called before the first frame update
    private float _speed ;
    // Start is called before the first frame update
    void Start()
    {
        _speed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime,Space.Self);
    }
}
