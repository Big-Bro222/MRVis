using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DiceBehaviorHandler : MonoBehaviour
{
    public Transform dataPointFolder;
    [Range(0, 4f)]
    public float rotationDuration;

    [Range(0, 5f)]
    public float translateDuration;



    private Vector3[] dataPointOriginalPosition;
    public enum State
    {
        ThreeDimension,
        Fat_Sugar,
        Fat_Protein,
        Protein_Sugar,
    };

    public State state;
    void Start()
    {
        state = State.ThreeDimension;
        dataPointOriginalPosition = new Vector3[dataPointFolder.childCount];
        for (int i = 0; i < dataPointFolder.childCount-1; i++)
        {
            dataPointOriginalPosition[i] = dataPointFolder.GetChild(i).localPosition;

        }
    }

    public void Translate( string stateStr )
    {
        string[] stateArray = { "ThreeDimension", "Fat_Sugar", "Fat_Protein", "Sugar_Protein" };
        string[] functionArray = { "", "ThreeDimentionToFatSugar", "ThreeDimentionToFatProtein", "ThreeDimentionToProteinSugar" };
        int index = Array.IndexOf(stateArray, stateStr);
        if (index == (int)state)
        {
            return;
        }

        switch (state)
        {
            case State.ThreeDimension:
                break;
            case State.Fat_Sugar:
                FatSugarToThreeDimension();
                break;
            case State.Fat_Protein:
                FatProteinToThreeDimention();
                break;
            case State.Protein_Sugar:
                ProteinSugarToThreeDimension();
                break;
            default:
                break;
        }


        switch (stateStr)
        {
            case "ThreeDimension":
                state = State.ThreeDimension;   
                break;
            case "Fat_Sugar":
                state = State.Fat_Sugar;

                break;
            case "Fat_Protein":
                state = State.Fat_Protein;
                break;
            case "Protein_Sugar":
                state = State.Protein_Sugar;
                break;
            default:
                state = State.ThreeDimension;
                break;
        }

        Invoke(functionArray[(int)state], 3);

    }

    public void Translatete(string state)
    {

        ProteinSugarToThreeDimension();
    }

    private void ThreeDimentionToFatProtein()
    {
        for (int i = 0; i < dataPointFolder.childCount-1; i++)
        {
            Vector3 dataPointLocalPos = dataPointFolder.GetChild(i).localPosition;
            Vector3 newPos = new Vector3(dataPointLocalPos.x, dataPointLocalPos.y, 0.5f);

            StartCoroutine(MoveToPosition(newPos, translateDuration, i));

        }
    }


    private void FatProteinToThreeDimention()
    {
        for (int i = 0; i < dataPointFolder.childCount-1; i++)
        {
            Vector3 dataPointOriginalPos = dataPointOriginalPosition[i];
            Vector3 newPos = new Vector3(dataPointOriginalPos.x, dataPointOriginalPos.y, dataPointOriginalPos.z);

            StartCoroutine(MoveToPosition(newPos, translateDuration, i));

        }
    }


    private void ThreeDimentionToFatSugar()
    {
        StartCoroutine(RotateToRotation(new Vector3(0, -90, 0), rotationDuration));

        for (int i = 0; i < dataPointFolder.childCount-1; i++)
        {
            Vector3 dataPointLocalPos = dataPointFolder.GetChild(i).localPosition;
            Vector3 newPos = new Vector3(0.5f, dataPointLocalPos.y, dataPointLocalPos.z);

            StartCoroutine(MoveToPosition(newPos, translateDuration, i));

        }

    }

    private void FatSugarToThreeDimension()
    {
        StartCoroutine(RotateToRotation(new Vector3(0, 90, 0), rotationDuration));
        for (int i = 0; i < dataPointFolder.childCount-1; i++)
        {
            Vector3 dataPointOriginalPos = dataPointOriginalPosition[i];
            Vector3 newPos = new Vector3(dataPointOriginalPos.x, dataPointOriginalPos.y, dataPointOriginalPos.z);

            StartCoroutine(MoveToPosition(newPos, translateDuration, i));

        }
    }


    private void ThreeDimentionToProteinSugar()
    {
        StartCoroutine(RotateToRotation(new Vector3(0, -90, 90), rotationDuration));
        for (int i = 0; i < dataPointFolder.childCount-1; i++)
        {
            Vector3 dataPointLocalPos = dataPointFolder.GetChild(i).localPosition;
            Vector3 newPos = new Vector3(dataPointLocalPos.x, -0.5f, dataPointLocalPos.z);

            StartCoroutine(MoveToPosition(newPos, translateDuration, i));

        }
    }

    private void ProteinSugarToThreeDimension()
    {
        StartCoroutine(RotateToRotation(new Vector3(0, 90, -90), rotationDuration));
        for (int i = 0; i < dataPointFolder.childCount-1; i++)
        {
            Vector3 dataPointOriginalPos = dataPointOriginalPosition[i];
            Vector3 newPos = new Vector3(dataPointOriginalPos.x, dataPointOriginalPos.y, dataPointOriginalPos.z);

            StartCoroutine(MoveToPosition(newPos, translateDuration, i));

        }
    }

    private IEnumerator MoveToPosition(Vector3 newPosition , float time,int i)
    {
        float elapsedTime  = 0;
        Vector3 startingPos   = dataPointFolder.GetChild(i).localPosition;
        while (elapsedTime < time)
        {
            dataPointFolder.GetChild(i).localPosition = Vector3.Lerp(startingPos, newPosition, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator RotateToRotation(Vector3 RotationAngle, float time)
    {
        float elapsedTime = 0;
        Quaternion startingRotation = dataPointFolder.localRotation;
        Quaternion endRotation = Quaternion.Euler(startingRotation.eulerAngles+RotationAngle);
        while (elapsedTime < time+Time.deltaTime)
        {

            dataPointFolder.localRotation = Quaternion.Lerp(startingRotation, endRotation, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

}
