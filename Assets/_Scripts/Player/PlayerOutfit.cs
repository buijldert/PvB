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
        [SerializeField] private MeshRenderer blackPlayerMeshRenderer;
        [SerializeField] private MeshRenderer whitePlayerMeshRenderer;

        /// <summary>
        /// Switches the players outfit material to the given material.
        /// </summary>
        /// <param name="outfitMaterial">The given outfit material.</param>
        private void SwitchOutfit(Material outfitMaterial)
        {
            blackPlayerMeshRenderer.sharedMaterials[1] = outfitMaterial;
            whitePlayerMeshRenderer.sharedMaterials[1] = outfitMaterial;
        }
    }
}