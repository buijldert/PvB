using UnityEngine;

public class ShirtPreviewManager : MonoBehaviour 
{
    public static ShirtPreviewManager instance;

    private MeshRenderer meshR;
    private Material m;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }

    private void Start()
    {
        meshR = GetComponent<MeshRenderer>();
        m = meshR.materials[0];
    }

    public void SetSkin(Texture texture)
    {
        m.SetTexture("_MainTex", texture);
    }
}
