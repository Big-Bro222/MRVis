using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class windowSizeSetUp : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider XSlider;
    public Slider YSlider;
    public GameObject MapBackground;
    [SerializeField] VisualizationController visualizationController;
    private Vector3 OriginalScale;

    void Start()
    {
        OriginalScale = MapBackground.transform.localScale;
    }

    public void UpdateSlider()
    {
        MapBackground.transform.localScale = new Vector3(OriginalScale.x * XSlider.value, OriginalScale.y*YSlider.value, OriginalScale.z);
    }

    public void SetScale()
    {
        Debug.Log(XSlider.value);
        visualizationController.xscale = XSlider.value/2;
        visualizationController.yscale = YSlider.value/2;
        object[] datas = new object[] {XSlider.value/2,YSlider.value/2};
        PhotonNetwork.RaiseEvent(Global.RESCALE_WINDOW, datas, RaiseEventOptions.Default, SendOptions.SendReliable);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
