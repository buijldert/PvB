using UnityEngine;

namespace RR.Components.Player
{
    /// <summary>
    /// This class is responible for switching the players outfit to the selected one.
    /// </summary>
    public class PlayerOutfit : MonoBehaviour
    {
        [SerializeField] private MeshRenderer[] playerMeshRenderers;

        /// <summary>
        /// Switches the players outfit material to the given material.
        /// </summary>
        /// <param name="outfitMaterial">The given outfit material.</param>
        public void SwitchOutfit(Material _outfitMaterial)
        {
            for (int i = 0; i < playerMeshRenderers.Length; i++)
            {
                Material[] rendererMaterials = playerMeshRenderers[i].materials;
                rendererMaterials[1] = _outfitMaterial;
                playerMeshRenderers[i].materials = rendererMaterials;
            }
        }
    }
}