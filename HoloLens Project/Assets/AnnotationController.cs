using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnotationController : MonoBehaviour
{
    private bool Annotation=true;
    [SerializeField] private GameObject AnnotationPanel;
    [SerializeField] private GameObject FixLabel;


    public void Switch()
    {
        Debug.Log("Switch in AnnotationController");
        Annotation = !Annotation;
        AnnotationPanel.SetActive(Annotation);
        FixLabel.SetActive(!Annotation);
    }
}
