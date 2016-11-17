using UnityEngine;
using System.Collections;


//Attach this to a camera
public class CameraPixelizer : MonoBehaviour {

    public Material material;
    void Start() {
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest) {
        Graphics.Blit(src, dest, material);
    }

}