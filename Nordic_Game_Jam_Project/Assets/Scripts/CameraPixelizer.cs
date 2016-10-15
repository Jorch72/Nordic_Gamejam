using UnityEngine;
using System.Collections;


//Attach this to a camera
public class CameraPixelizer : MonoBehaviour {

    public Material material;
    public Color color1;
    public Color color2;
    public Color color3;
    public Color color4;

    private Matrix4x4 colorMatrix;

    void OnRenderImage(RenderTexture src, RenderTexture dest) {
        material.SetMatrix("_ColorMatrix", ColorMatrix);
        Graphics.Blit(src, dest, material);
    }

    Matrix4x4 ColorMatrix {
        get {
            Matrix4x4 matrix = new Matrix4x4();
            matrix.SetRow(0, ColorToVector(color1));
            matrix.SetRow(1, ColorToVector(color2));
            matrix.SetRow(2, ColorToVector(color3));
            matrix.SetRow(3, ColorToVector(color4));
            return matrix;
        }
    }

    private Vector4 ColorToVector(Color color) {
        return new Vector4(color.r, color.g, color.b, color.a);
    }
}