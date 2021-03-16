using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SplitScreen : ScreenPostEffectBase
{
    public bool _SpiltScreenMode = true;
    public Camera _SecondCamera;


    private void Update()
    {
        
        //Configure(RenderTexture sourceTexture, RenderTexture destTexture);
        //Configure();
    }

    public void Blit(RenderTexture sourceTexture, RenderTexture destTexture)
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

            //Debug.Log(_SpiltScreenMode ? 1 : 0);

            Graphics.Blit(sourceTexture, destTexture, _Material);
        }
        else
        {
            //Graphics.Blit(sourceTexture, destTexture);
        }
    }
}