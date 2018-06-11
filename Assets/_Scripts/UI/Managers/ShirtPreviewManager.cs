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

        /// <summary>
        /// OnEnable() is called before Start() and after Awake().
        /// </summary>
        private void OnEnable()
        {
            CodeScreenManager.onNewCodeUsed += SetSkin;
        }

        /// <summary>
        /// Start() is called after Awake() and OnEnable().
        /// </summary>
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

        /// <summary>
        /// OnDisable is called before the object is disabled.
        /// </summary>
        private void OnDisable()
        {
            CodeScreenManager.onNewCodeUsed -= SetSkin;
        }
    }
}