using Synchro;

using System.Collections.Generic;
using UnityEngine;

public class SlateAuthorViewRights : AuthorViewRights
{
    private VersioningNotifications vn;
    private Synchro.DeviceType device;
    public bool defaultPublic = false;

    private Material Action0;
    private Material Action1;
    private Material Action2;
    

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        vn = this.GetComponent<VersioningNotifications>();
        device = SynchroManager.Instance.IsDevice();        
    }

    protected void Start()
    {
        if (defaultPublic)
        {
            viewPermissions.SwitchPublic();
        }

        int idAction = SynchroManager.Instance.fullOwners.IndexOf(int.Parse(SynchroManager.Instance.ownerId));
        Action0 = Resources.Load("Action0") as Material;
        Action1 = Resources.Load("Action1") as Material;
        Action2 = Resources.Load("Action2") as Material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnDestroy()
    {
        SynchroManager.Instance.RemoveObject(transform.GetChild(0).name);
        SynchroManager.Instance.RemoveObject(gameObject.name);
    }    
    
    public void ChangePermission(AuthorViewRights savr)
    {
        Debug.Log(this.name + " " +     savr.viewPermissions.PermissionState().ToString());
        switch (savr.viewPermissions.PermissionState())
        {
            case PrivacyState.Foreign:
                MakeForeign();                
                break;
            case PrivacyState.Private:
                MakePrivate();
                break;
            case PrivacyState.Public:
                MakePublic();
                break;
            case PrivacyState.Shared:
                MakeShared(savr.viewPermissions.GetCollaborators());
                break;
        }
        
        changePermission.objectName = this.name;
        changePermission.owners = new List<int>(savr.viewPermissions.GetCollaborators());
        changePermission.permissionState = Permissions.PrivacyConverter(savr.viewPermissions.PermissionState());
        SynchroServer.Instance.SendCommand("H", changePermission);
    }
     
    public override void MakePrivate()
    {
        PrivacyState oldState = viewPermissions.PermissionState();

        SynchroManager.Instance.ToOwn(this.name, oldState);
        viewPermissions.SwitchPrivate();
        vn.SetStandardVisibility(true, false, false);
    }

    public override void MakePublic()
    {
        PrivacyState oldState = viewPermissions.PermissionState();

        SynchroManager.Instance.ToShared(this.name, oldState);
        viewPermissions.SwitchPublic();
        vn.SetWallVisibility();
    }

    public override void MakeShared(List<int> owners)
    {
        PrivacyState oldState = viewPermissions.PermissionState();

        SynchroManager.Instance.ToShared(this.name, oldState);
        viewPermissions.SwitchShared(owners);

        List<int> fullOwnersList = new List<int>(SynchroManager.Instance.fullOwners);
        List<bool> isIn = new List<bool>();
        for(int i = 0; i < fullOwnersList.Count; i++)
        {
            isIn.Add(owners.Contains(fullOwnersList[i]));
        }

        vn.SetStandardVisibility(isIn[0], isIn[1], isIn[2]);
    }

    public override void MakeForeign()
    {
        PrivacyState oldState = viewPermissions.PermissionState();

        SynchroManager.Instance.ToForeign(this.name, oldState);
        viewPermissions.SwitchForeign();        
    }

    public override void RemoteMakePublic()
    {
        PrivacyState oldState = viewPermissions.PermissionState();
        SynchroManager.Instance.ToShared(this.name, oldState);

        vn.Activate();
        vn.SetWallVisibility();
    }

    public override void RemoteMakePrivate()
    {
        PrivacyState oldState = viewPermissions.PermissionState();
        SynchroManager.Instance.ToOwn(this.name, oldState);

        if (device == Synchro.DeviceType.WALL)
        {
            
        } else if (device == Synchro.DeviceType.HOLOLENS)
        {

        } else if (device == Synchro.DeviceType.MASTER)
        {

        }
        vn.Activate();
        vn.SetStandardVisibility(true, false, false);
    }

    public override void RemoteMakeShared(List<int> owners)
    {
        PrivacyState oldState = viewPermissions.PermissionState();
        SynchroManager.Instance.ToShared(this.name, oldState);

        if (device == Synchro.DeviceType.WALL)
        {

        }
        else if (device == Synchro.DeviceType.HOLOLENS)
        {

        }
        else if(device == Synchro.DeviceType.MASTER)
        {

        }

        List<int> fullOwnersList = new List<int>(SynchroManager.Instance.fullOwners);
        List<bool> isIn = new List<bool>();

        for (int i = 0; i < fullOwnersList.Count; i++)
        {
            isIn.Add(owners.Contains(fullOwnersList[i]));
        }

        viewPermissions.SwitchShared(owners);
        vn.Activate();
        vn.SetStandardVisibility(isIn[0], isIn[1], isIn[2]);
    }

    public override void RemoteMakeForeign()
    {
        MakeForeign();
        vn.Deactivate();
    }    
}
