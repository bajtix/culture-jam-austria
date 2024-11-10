using UnityEngine;

public class LODSnow : MonoBehaviour, IDrawingSurface {
    public Shader categorizeShader, drawingShader;

    [Range(4, 12)] public int resolution = 4;
    public Texture initialTexture;

    public Vector2 rangeMD = new Vector2(0.05f, 0.1f);

    [Header("Mesh Config")]
    public MeshRenderer[] meshesHD;
    public float maxDistanceHD = 10;
    public MeshRenderer[] meshesMD;
    public float maxDistanceMD = 50;
    public MeshRenderer[] meshesLD;

    private RenderTexture m_heightmap, m_downsample, m_back;
    private Material m_catmat, m_targetMaterial, m_drawMaterial;
    private float[] m_precisionResults = new float[] {
        0,0,0,0
    };


    private void Start() {
        m_heightmap = new RenderTexture(0x1 << resolution, 0x1 << resolution, 0, UnityEngine.Experimental.Rendering.GraphicsFormat.R16_SFloat);
        m_back = new RenderTexture(m_heightmap);
        m_downsample = new RenderTexture(m_heightmap);
        m_heightmap.Create();
        m_back.Create();
        m_downsample.Create();

        m_catmat = new Material(categorizeShader);
        m_catmat.SetInt("_Factor", 2);

        m_drawMaterial = new Material(drawingShader);
        m_drawMaterial.SetTexture("_MainTex", m_back);
        m_drawMaterial.SetVector("_Brush", Vector4.zero);

        m_targetMaterial = GetComponent<MeshRenderer>().material;
        m_targetMaterial.SetTexture("_HeightMap", m_heightmap);

        Graphics.Blit(initialTexture, m_heightmap, m_drawMaterial);
        Graphics.Blit(initialTexture, m_back, m_drawMaterial);

        foreach (var mr in meshesLD) {
            mr.sharedMaterial = m_targetMaterial;
            mr.GetComponent<MeshFilter>().sharedMesh.bounds = new Bounds(mr.GetComponent<MeshFilter>().sharedMesh.bounds.center, mr.GetComponent<MeshFilter>().sharedMesh.bounds.size * 2);
            mr.enabled = false;
        }

        foreach (var mr in meshesMD) {
            mr.sharedMaterial = m_targetMaterial;
            mr.GetComponent<MeshFilter>().sharedMesh.bounds = new Bounds(mr.GetComponent<MeshFilter>().sharedMesh.bounds.center, mr.GetComponent<MeshFilter>().sharedMesh.bounds.size * 2);
            mr.enabled = false;
        }

        foreach (var mr in meshesHD) {
            mr.sharedMaterial = m_targetMaterial;
            mr.GetComponent<MeshFilter>().sharedMesh.bounds = new Bounds(mr.GetComponent<MeshFilter>().sharedMesh.bounds.center, mr.GetComponent<MeshFilter>().sharedMesh.bounds.size * 2);
            mr.enabled = false;
        }

        InvokeRepeating("Recalculate", 0, 2);
    }

    private void Recalculate() {
        m_precisionResults = CalculateRequiredHeightMapPrecision();
        //Debug.Log($"PRECISION {results[0]} {results[1]} {results[2]} {results[3]}");
    }

    public void SetLODs(params int[] lods) {
        if (lods.Length != 4) throw new System.Exception("4 LODs required");
        //Debug.Log($"LOD {lods[0]} {lods[1]} {lods[2]} {lods[3]}");
        for (int i = 0; i < 4; i++) {
            meshesHD[i].enabled = lods[i] == 0;
            meshesMD[i].enabled = lods[i] == 1;
            meshesLD[i].enabled = lods[i] >= 2;
        }
    }

    private int GetLODFromPrecision(float precision) {
        return precision >= rangeMD.x ? (precision <= rangeMD.y ? 1 : 0) : 2;
    }

    private int GetLODFromDistance(float sqrDistance) {
        return sqrDistance >= (maxDistanceHD * maxDistanceHD) ? (sqrDistance < (maxDistanceMD * maxDistanceMD) ? 1 : 2) : 0;
    }

    private void FixedUpdate() {
        var camPos = Camera.main.transform.position;

        float[] distances = new float[4];
        int[] lods = new int[4];
        for (int i = 0; i < 4; i++) {
            distances[i] = (meshesLD[i].transform.position - camPos).sqrMagnitude;
        }

        if (Mathf.Min(distances) > maxDistanceMD * maxDistanceMD) {
            SetLODs(2, 2, 2, 2);
            //Debug.Log("Too far!");
            return;
        }


        for (int i = 0; i < 4; i++) {
            lods[i] = Mathf.Max(GetLODFromPrecision(m_precisionResults[i]), GetLODFromDistance(distances[i]));
        }

        SetLODs(lods);
    }



    private float[] CalculateRequiredHeightMapPrecision() {
        m_catmat.SetFloat("_TexelSize", 1f / (0x1 << resolution));
        Graphics.Blit(m_heightmap, m_downsample, m_catmat, 1); // derivative pass

        for (int power = resolution; power >= 2; power--) {
            m_catmat.SetFloat("_TexelSize", 1f / (0x1 << power));
            Graphics.Blit(m_downsample, m_back, m_catmat, 0);
            Graphics.Blit(m_back, m_downsample);
        }

        var tmp = RenderTexture.GetTemporary(2, 2, 0, UnityEngine.Experimental.Rendering.GraphicsFormat.R16_SFloat);
        Graphics.Blit(m_downsample, tmp);
        RenderTexture.active = tmp;
        Texture2D readable = new Texture2D(2, 2);
        readable.ReadPixels(new Rect(0, 0, 2, 2), 0, 0);
        readable.Apply();
        RenderTexture.ReleaseTemporary(tmp);


        return new float[] {
            readable.GetPixel(0,0).r,
            readable.GetPixel(0,1).r,
            readable.GetPixel(1,0).r,
            readable.GetPixel(1,1).r
        };
    }

    public void Splat(Vector2 pos, float radius, float strength) {
        m_drawMaterial.SetVector("_Brush", new Vector4(pos.x, pos.y, radius, strength));
        Graphics.Blit(m_heightmap, m_back);
        Graphics.Blit(m_back, m_heightmap, m_drawMaterial);
    }

    protected void OnDestroy() {
        m_heightmap.Release();
        m_back.Release();
    }
}
