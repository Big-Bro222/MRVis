using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSetUpscale : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] BoxCollider ClipCollider;
    [SerializeField] Transform background;
    [SerializeField] RectTransform dragable;
    private Vector3 originalScaleClip;
    private Vector3 originalScaleBackground;
    private Vector2 originalScaledragable;

    private void Awake()
    {
        originalScaleClip = ClipCollider.size;
        originalScaleBackground = background.transform.localScale;
        originalScaledragable = dragable.sizeDelta;
    }


    public void scalesetup(float x,float y)
    {
        if (!ClipCollider && !background&&!dragable)
        {
            Debug.LogError("there is nothing for rescale for visualization!");
            return;
        }
        ClipCollider.size = new Vector3(originalScaleClip.x * x*0.5f, originalScaleClip.y* y*0.5f, originalScaleClip.z);

        background.transform.localScale = new Vector3(originalScaleBackground.x * x * 0.5f, originalScaleBackground.y * y * 0.5f, originalScaleBackground.z);

        dragable.sizeDelta=new Vector2(originalScaledragable.x * x * 0.5f, originalScaledragable.y*y * 0.5f);
    }


}
