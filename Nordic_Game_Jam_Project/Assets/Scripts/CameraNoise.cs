using UnityEngine;
using System.Collections;

public class CameraNoise : MonoBehaviour {

    public Material material;

    void OnRenderImage(RenderTexture src, RenderTexture dest) {
        Graphics.Blit(src, dest, material);
    }
}
