using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreen : ScreenPostEffectBase
{
    public bool _SpiltScreenMode = true;
    public Camera _SecondCamera;

    public virtual void Configure(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        if (_Material != null)
        {
            if (_SecondCamera.targetTexture == null)
            {
                ////Create a texture of the same width and height according to the width and height of the main camera, and record the image of the auxiliary camera
                _SecondCamera.targetTexture = new RenderTexture(_SecondCamera.pixelWidth, _SecondCamera.pixelHeight, 0);
            }
            _Material.SetTexture("_SecondCameraTexture", _SecondCamera.targetTexture);
            _Material.SetInt("_SpiltScreenMode", _SpiltScreenMode ? 1 : 0);

            Debug.Log(_SpiltScreenMode ? 1 : 0);

            Graphics.Blit(sourceTexture, destTexture, _Material);
        }
        else
        {
            Debug.Log("Please say it's called");
            Graphics.Blit(sourceTexture, destTexture);
        }
    }
}