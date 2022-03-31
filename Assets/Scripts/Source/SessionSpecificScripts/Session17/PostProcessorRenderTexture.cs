using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class PostProcessorRenderTexture : MonoBehaviour
{
    [Range(0.1f, 4.0f)] [SerializeField] private float renderScale;
    [SerializeField] private RawImage image;

    private Camera cam;

    [SerializeField]private RenderTexture rTex;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        RenderTextureDescriptor descr = new RenderTextureDescriptor(
            (int)(cam.pixelWidth * renderScale),
            (int)(cam.pixelHeight * renderScale),
            SystemInfo.GetGraphicsFormat(DefaultFormat.HDR), 32, 8
        );
        
        rTex = RenderTexture.GetTemporary(descr);
        rTex.name = "PostProcessing Texture";
        cam.targetTexture = rTex;
        image.texture = rTex;
    }
}
