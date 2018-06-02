using UI.Managers;
using UnityEngine;

public class ShirtPreviewManager : MonoBehaviour 
{
    private MeshRenderer meshRenderer;
    private Material material;

    private void OnEnable()
    {
        CodeScreenManager.onNewCodeUsed += SetSkin;
    }

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.materials[0];
    }

    public void SetSkin(ItemModel item)
    {
        material.SetTexture("_MainTex", item.ItemTexture);
    }

    private void OnDisable()
    {
        CodeScreenManager.onNewCodeUsed -= SetSkin;
    }
}
