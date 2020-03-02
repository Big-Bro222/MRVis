using Synchro;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum PrivacyState
{
    Foreign,
    Private,
    Public,
    Shared
};

public class Permissions
{
    // -1 : foreign
    // 0 : private
    // 1 : shared with collaborators
    // 2 : public

    private int permission;
    private List<int> collaborators;

    public Permissions()
    {
        this.permission = -1;
        collaborators = new List<int>();
    }

    public PrivacyState PermissionState()
    {
        return (permission == -1) ? PrivacyState.Foreign : (permission == 0) ? PrivacyState.Private : (permission == 1) ? PrivacyState.Shared : PrivacyState.Public;
    }

    public int GetPermission()
    {
        return permission;
    }

    public void SetCollaborators(List<int> collaboratorsIDs)
    {
        if (collaboratorsIDs.Count == 0)
        {
            permission = 0;
            collaborators.Clear();
        }
        else
        {
            permission = 1;
            collaborators.Clear();
            collaborators = new List<int>(collaboratorsIDs);
        }
    }

    public void SetCollaborator(int collaboratorID)
    {
        permission = 1;
        collaborators.Clear();
        collaborators.Add(collaboratorID);        
    }

    public List<int> GetCollaborators()
    {
        return collaborators;
    }

    public void SwitchShared(List<int> collaborators)
    {
        permission = 1;
        this.collaborators = new List<int>(collaborators);
    }
    public void SwitchPublic()
    {
        SetCollaborators(SynchroManager.Instance.fullOwners);
        permission = 2;
    }
    public void SwitchPrivate()
    {
        permission = 0;
        collaborators.Clear();
    }
    public void SwitchForeign()
    {        
        permission = -1;
        collaborators.Clear();
    }

    public static PrivacyState PrivacyConverter(int p)
    {
        return (p == -1) ? PrivacyState.Foreign : (p == 0) ? PrivacyState.Private : (p == 1) ? PrivacyState.Shared : (p == 2) ? PrivacyState.Public : throw new Exception("Invalid permission value " + p );
    }

    public static int PrivacyConverter(PrivacyState p)
    {
        return (p == PrivacyState.Foreign) ? -1  : (p == PrivacyState.Private) ? 0 : (p == PrivacyState.Shared) ? 1 : (p == PrivacyState.Public) ? 2 : throw new Exception("Invalid PrivacyState value " + p);
    }
}