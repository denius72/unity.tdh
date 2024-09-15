using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]

public class PostProcessEffect : MonoBehaviour {
    public Material postProcessMaterial;

    private void OnRenderImage(RenderTexture src, RenderTexture dest) {
        if (postProcessMaterial == null) {
            Graphics.Blit(src, dest);
			return;
        } else {
            Graphics.Blit(src, dest, postProcessMaterial);
        }
    }
}
