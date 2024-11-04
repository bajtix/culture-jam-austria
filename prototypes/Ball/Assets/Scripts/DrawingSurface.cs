using UnityEngine;

public class DrawingSurface : MonoBehaviour {
    public Shader drawingShader;

    [Range(4, 12)] public int resolution = 4;

    private int m_brushProperty;
    private RenderTexture m_front, m_back;
    private Material m_targetMaterial, m_drawMaterial;


    private void Start() {
        m_targetMaterial = GetComponent<MeshRenderer>().material;
        m_front = new RenderTexture(0x1 << resolution, 0x1 << resolution, 0, UnityEngine.Experimental.Rendering.GraphicsFormat.R16_SFloat);
        m_back = new RenderTexture(m_front);
        m_front.Create();
        m_back.Create();

        m_drawMaterial = new Material(drawingShader);
        m_brushProperty = Shader.PropertyToID("_Brush");

        m_drawMaterial.SetTexture("_MainTex", m_back);
        m_drawMaterial.SetVector(m_brushProperty, Vector4.zero);
        m_targetMaterial.SetTexture("_MainTex", m_front);
        m_targetMaterial.SetTexture("_BaseMap", m_front);

        Graphics.Blit(null, m_front, m_drawMaterial);
        Graphics.Blit(null, m_back, m_drawMaterial);
    }

    public void Splat(Vector2 pos, float radius, float strength) {
        m_drawMaterial.SetVector(m_brushProperty, new Vector4(pos.x, pos.y, radius, strength));
        Graphics.Blit(m_back, m_front, m_drawMaterial);
        Graphics.Blit(m_front, m_back);
    }

    private void OnDestroy() {
        m_front.Release();
        m_back.Release();
    }
}
