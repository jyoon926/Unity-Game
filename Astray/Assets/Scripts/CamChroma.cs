using UnityEngine;

[ExecuteInEditMode]

public class CamChroma : MonoBehaviour
{
    public Material paletteMaterial;

    private void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, paletteMaterial);
    }
}