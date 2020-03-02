using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Synchro;
using NaughtyAttributes;

public class VersioningNotifications : MonoBehaviour
{
    [BoxGroup("Textures")]
    public Material basic;
    [BoxGroup("Textures")]
    public Material notifNew;
    [BoxGroup("Textures")]
    public Material notifMissing;
    [BoxGroup("Textures")]
    public Material invisible;

    [BoxGroup("ObjectStatus")]
    public Transform plane;
    [BoxGroup("ObjectStatus")]
    public bool isAttracted;
    [BoxGroup("ObjectStatus")]
    public bool isOnWall;

    public bool ToDestroy { get; private set; } = false;

    public bool ShowId = false;

    private bool isNotified = false;
    private bool hasDisappeared = false;

    // Start is called before the first frame update
    void Awake()
    {
        plane = transform.GetChild(0);
    }

    void Start()
    {       
        if (!ToDestroy && isAttracted && (SynchroManager.Instance.IsDevice() == Synchro.DeviceType.HOLOLENS))
            plane.GetComponent<Renderer>().material = invisible;

        if (isOnWall)
            SetWallVisibility();
        else 
            SetStandardVisibility(true, true, true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetStandardVisibility(bool c, bool b, bool a)
    {
        isOnWall = false;

        if (SynchroManager.Instance.IsDevice() == Synchro.DeviceType.HOLOLENS)
        {
            SetOwnSelf();
        }
        else if(SynchroManager.Instance.IsDevice() == Synchro.DeviceType.WALL)
        {
            SetPublicSelf();
        }
    }

    public void SetWallVisibility()
    {
        isOnWall = true;

        if (SynchroManager.Instance.IsDevice() == Synchro.DeviceType.HOLOLENS)
            SetPublicSelf();
        else if (SynchroManager.Instance.IsDevice() == Synchro.DeviceType.WALL)
            SetOwnSelf();
    }

    public void Deactivate()
    {
        Debug.Log("isDeactivated");        
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void SendMissing()
    {
        Debug.Log("Send Take");
        SynchroManager.Instance.SharedToOwn(this.name);
        SynchroManager.Instance.SharedToOwn(this.GetComponent<BoundingBox>().Target.name);
    }

    public void SendNew()
    {
        Debug.Log("Send Public");
        SynchroManager.Instance.OwnToShared(this.name);
        SynchroManager.Instance.OwnToShared(this.GetComponent<BoundingBox>().Target.name);
    }

    public void Remove()
    {
        plane.GetComponent<Renderer>().enabled = false;
        foreach (Transform child in plane)
        {
            child.gameObject.SetActive(false);
        }
        hasDisappeared = true;
    }

    public void Add()
    {
        plane.GetComponent<Renderer>().enabled = true;
        plane.GetComponent<Renderer>().material = basic;
        foreach (Transform child in plane)
        {
            child.gameObject.SetActive(true);
        }
        hasDisappeared = false;
    }    

    #region PrivacySetters

    public void SetOwn()
    {
        gameObject.SetActive(true);
        isNotified = false;
        plane.GetComponent<Renderer>().material = basic;
        plane.GetComponent<Renderer>().enabled = true;
        foreach (Transform child in plane)
        {
            child.gameObject.SetActive(true);
        }
        hasDisappeared = false;

        if (SynchroManager.Instance.IsDevice() == Synchro.DeviceType.HOLOLENS)
        {
            this.GetComponent<BoundingBox>().enabled = true;
            this.GetComponent<InteractionScript>().enabled = true;
        }
    }

    public void SetOwnSelf()
    {
        gameObject.SetActive(true);
        isNotified = false;
        plane.GetComponent<Renderer>().material = basic;
        plane.GetComponent<Renderer>().enabled = true;
        foreach (Transform child in plane)
        {
            child.gameObject.SetActive(true);
        }

        hasDisappeared = false;        

        if (SynchroManager.Instance.IsDevice() == Synchro.DeviceType.HOLOLENS)
        {
            this.GetComponent<BoundingBox>().enabled = true;
            this.GetComponent<InteractionScript>().enabled = true;
        }
    }

    public void SetPublic(string owner)
    {
        gameObject.SetActive(true);
        isNotified = true;
        plane.GetComponent<Renderer>().material = notifNew;
        foreach (Transform child in plane)
        {
            child.gameObject.SetActive(false);
        }
        hasDisappeared = false;

        if (SynchroManager.Instance.IsDevice() == Synchro.DeviceType.HOLOLENS)
        {
            this.GetComponent<BoundingBox>().enabled = true;
            this.GetComponent<InteractionScript>().enabled = true;
        }
    }

    public void SetPublicSelf()
    {
        gameObject.SetActive(true);
        isNotified = true;        
        foreach (Transform child in plane)
        {
            child.gameObject.SetActive(false);
        }

        hasDisappeared = false;

        if (SynchroManager.Instance.IsDevice() == Synchro.DeviceType.HOLOLENS)
        {
            this.GetComponent<BoundingBox>().enabled = true;
            this.GetComponent<InteractionScript>().enabled = true;
        }

        plane.GetComponent<Renderer>().material = invisible;
    }

    public void SetForeign(string owner)
    {
        GameObject Shadow = Instantiate(gameObject);        
        Shadow.transform.position = this.transform.position;
        Shadow.transform.parent = SynchroManager.Instance.transform;
        Shadow.transform.localRotation = Quaternion.identity;
        Shadow.GetComponent<VersioningNotifications>().SetForeignSelf(owner);        
        gameObject.SetActive(false);
    }

    public void SetForeignSelf(string owner)
    {
        gameObject.SetActive(true);
        isNotified = true;
        ToDestroy = true;

        plane.GetComponent<Renderer>().material = notifMissing;
        plane.GetComponent<Renderer>().enabled = true;
        foreach (Transform child in plane)
        {
            child.gameObject.SetActive(false);
        }
        hasDisappeared = true;

        if (SynchroManager.Instance.IsDevice() == Synchro.DeviceType.HOLOLENS)
        {
            this.GetComponent<BoundingBox>().enabled = false;
            this.GetComponent<InteractionScript>().enabled = false;
        }                
    }

    #endregion

    void BackToBase()
    {
        if (isNotified)
        {
            plane.GetComponent<Renderer>().enabled = false;

            if (hasDisappeared)
            {
                hasDisappeared = false;
                gameObject.SetActive(false);
            }

            if (ToDestroy)
            {
                Destroy(gameObject);
            }
        }
    }
}
