using UnityEngine;

public class DynamicDraw : MonoBehaviour {
    public Shader drawingShader;
    public Material heightTarget;
    public float brushSize = 0.02f;
    public int resolution = 256;
    private RenderTexture m_tex, m_swap;
    private Material m_drawingMaterial;

    private void Start() {
        m_tex = new RenderTexture(resolution, resolution, 0, RenderTextureFormat.RFloat);
        m_swap = new RenderTexture(m_tex);
        m_tex.Create();
        m_swap.Create();
        m_drawingMaterial = new Material(drawingShader);
        m_drawingMaterial.SetFloat("_BrushRadius", brushSize);
        m_drawingMaterial.SetVector("_BrushPosition", Vector2.one * 4);

        Graphics.Blit(null, m_tex, m_drawingMaterial);
        m_drawingMaterial.SetTexture("_MainTex", m_tex);
        heightTarget.SetTexture("_HeightMap", m_tex);
    }

    public void AddSplat(Vector2 uv) {
        Graphics.Blit(m_tex, m_swap, m_drawingMaterial);
        m_drawingMaterial.SetVector("_BrushPosition", uv);
        Graphics.Blit(m_swap, m_tex);
    }


}
