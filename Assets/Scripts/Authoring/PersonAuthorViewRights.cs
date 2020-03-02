using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Synchro;
using UnityEngine;

public class PersonAuthorViewRights : AuthorViewRights
{
    public int selfColor;
    private Color[] colorPalette = new Color[] { Color.green, Color.red, Color.yellow };

    public override void MakeForeign()
    {
        throw new System.NotImplementedException();
    }

    public override void MakePrivate()
    {
        viewPermissions.SwitchPrivate();
    }

    public override void MakePublic()
    {
        throw new System.NotImplementedException();
    }

    public override void MakeShared(List<int> owners)
    {
        viewPermissions.SwitchShared(owners);
    }

    public override void RemoteMakeForeign()
    {
        throw new System.NotImplementedException();
    }

    public override void RemoteMakePrivate()
    {
        throw new System.NotImplementedException();
    }

    public override void RemoteMakePublic()
    {
        throw new System.NotImplementedException();
    }

    public override void RemoteMakeShared(List<int> owners)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeShared(new List<int>() { int.Parse(SynchroManager.Instance.ownerId) });

        if (!isGenerated)
            selfColor = SynchroManager.Instance.fullOwners.IndexOf(int.Parse(SynchroManager.Instance.ownerId));

        /* for debug on pc */
        if (selfColor == -1)
            selfColor = 0;

        this.GetComponent<Renderer>().material.SetColor("_WireColor", colorPalette[selfColor]);
    }

    // Update is called once per frame
    void Update()
    {
    }

    [Button]
    public void DistancePoint()
    {
        Debug.Log(Vector3.Distance(this.transform.position, Vector3.zero).ToString());
    }

    [Button]
    public void ReCalibrate()
    {
        SynchroManager.Instance.OrderCalibration(this.name);
    }

    [Button]
    public void SelfRecalibrate()
    {
        SynchroManager.Instance.ReCalibrate();
    }
}
