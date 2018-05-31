using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
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
        public void SwitchOutfit(Material outfitMaterial)
        {
            for (int i = 0; i < playerMeshRenderers.Length; i++)
            {
                Material[] rendererMaterials = playerMeshRenderers[i].materials;
                rendererMaterials[1] = outfitMaterial;
                playerMeshRenderers[i].materials = rendererMaterials;
            }
        }
    }
}