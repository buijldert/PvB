using RR.Models;
using UnityEngine;

namespace RR.UI.Managers
{
    /// <summary>
    /// This class is responsible for showing the shirt unlock previews.
    /// </summary>
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

        /// <summary>
        /// Sets the texture of the used material
        /// </summary>
        /// <param name="item">The item model which holds the texture.</param>
        public void SetSkin(ItemModel item)
        {
            material.SetTexture("_MainTex", item.ItemTexture);
        }

        private void OnDisable()
        {
            CodeScreenManager.onNewCodeUsed -= SetSkin;
        }
    }
}