using NaughtyAttributes;
using Synchro;
using System.Collections.Generic;
using UnityEngine;


public abstract class AuthorViewRights : MonoBehaviour
{
    public Permissions viewPermissions;
    protected ChangePermission changePermission;
    public bool isGenerated = false;

    protected virtual void Awake()
    {
        viewPermissions = new Permissions();
        changePermission = new ChangePermission();
        changePermission.owner = SynchroManager.Instance.ownerId;
    }

    public virtual void AnticipateStart() {}

    public virtual void GetAuthoring()
    {
        SynchroManager.Instance.Lock(this.name);
    }

    public virtual void LetAuthoring()
    {
        SynchroManager.Instance.Unlock(this.name);
    }

    public abstract void MakePublic();
    public abstract void MakePrivate();    
    public abstract void MakeShared(List<int> owners);
    public abstract void MakeForeign();

    public abstract void RemoteMakePublic();
    public abstract void RemoteMakePrivate();
    public abstract void RemoteMakeShared(List<int> owners);
    public abstract void RemoteMakeForeign();

    [Button]
    public void ViewPermissions()
    {
        if (viewPermissions.PermissionState() == PrivacyState.Shared) {
            string s = "";
            foreach (int i in viewPermissions.GetCollaborators())
                s += i.ToString() + " - ";
            Debug.Log(viewPermissions.PermissionState() + " " + s);
        }
        else
            Debug.Log(viewPermissions.PermissionState());
    }
}
