using UnityEngine;

public class DrawingSurface : MonoBehaviour {
    [SerializeField] private Shader m_drawingShader;
    [SerializeField] private Texture m_initialTexture;
    [SerializeField][Range(4, 13)] private int m_resolution = 4;
    private int m_propA1, m_propA2, m_propBrushTex;
    private int m_passCircle, m_passLine, m_passTexture, m_passClear;
    private RenderTexture m_front, m_back;
    private Material m_drawMaterial, m_targetMaterial;


    private void Start() {
        m_targetMaterial = GetComponent<MeshRenderer>().sharedMaterial;
        m_front = new RenderTexture(0x1 << m_resolution, 0x1 << m_resolution, 0, UnityEngine.Experimental.Rendering.GraphicsFormat.R16_SFloat);
        m_back = new RenderTexture(m_front);
        m_front.Create();
        m_back.Create();

        m_drawMaterial = new Material(m_drawingShader);
        m_propBrushTex = Shader.PropertyToID("_BrushTex");
        m_propA1 = Shader.PropertyToID("_Vectors");
        m_propA2 = Shader.PropertyToID("_RHS");
        m_passCircle = m_drawMaterial.FindPass("Circle");
        m_passTexture = m_drawMaterial.FindPass("Texture");
        m_passLine = m_drawMaterial.FindPass("Line");
        m_passClear = m_drawMaterial.FindPass("Clear");

        m_drawMaterial.SetTexture("_MainTex", m_back);
        m_drawMaterial.SetVector(m_propA1, Vector4.zero);
        m_drawMaterial.SetVector(m_propA2, Vector4.zero);

        m_targetMaterial.SetTexture("_HeightMap", m_front);

        Graphics.Blit(m_initialTexture, m_front, m_drawMaterial);
        Graphics.Blit(m_initialTexture, m_back, m_drawMaterial);
    }


    protected void OnDestroy() {
        m_front.Release();
        m_back.Release();
    }

    protected virtual void Reblit(int pass) {
        Graphics.Blit(m_front, m_back);
        Graphics.Blit(m_back, m_front, m_drawMaterial, pass);
    }

    public virtual void AddCircularMark(Vector2 pos, float radius, float harshness, float strength = 1) {
        m_drawMaterial.SetVector(m_propA1, new Vector4(
            pos.x, pos.y, 0, 0
        ));
        m_drawMaterial.SetVector(m_propA2, new Vector4(
            radius, harshness, strength, 0
        ));
        Reblit(m_passCircle);
    }
    public virtual void AddLineMark(Vector2 from, Vector2 to, float radius, float harshness, float strength = 1) {
        m_drawMaterial.SetVector(m_propA1, new Vector4(
            from.x, from.y, to.x, to.y
        ));
        m_drawMaterial.SetVector(m_propA2, new Vector4(
           radius, harshness, strength, 0
        ));
        Reblit(m_passLine);
    }
    public virtual void AddTextureMark(Texture tex, Vector2 pos, float rot, Vector2 scale, float strength = 1) {
        m_drawMaterial.SetTexture(m_propBrushTex, tex);
        m_drawMaterial.SetVector(m_propA1, new Vector4(
            pos.x, pos.y, scale.x, scale.y
        ));
        m_drawMaterial.SetVector(m_propA2, new Vector4(
           rot, 0, strength, 0
        ));
        Reblit(m_passTexture);
    }
    public virtual void Clear(float strength = 1) {
        m_drawMaterial.SetVector(m_propA2, new Vector4(
            0, 0, strength, 0
        ));
        Reblit(m_passClear);
    }
}
