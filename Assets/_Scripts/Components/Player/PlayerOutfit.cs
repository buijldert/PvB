﻿using RR.Models;
using RR.UI.Managers;
using UnityEngine;

namespace RR.Components.Player
{
    /// <summary>
    /// This class is responible for switching the players outfit to the selected one.
    /// </summary>
    public class PlayerOutfit : MonoBehaviour
    {
        public static PlayerOutfit instance;

        public delegate void ChangeOutfit(ItemModel _itemModel);
        public static ChangeOutfit OnChangeOutfit;

        [SerializeField] private MeshRenderer[] playerMeshRenderers;

        /// <summary>
        /// Awake() is called before OnEnable() and Start().
        /// </summary>
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            instance = this;

        }

        /// <summary>
        /// OnEnable() is called before Start() and after Awake().
        /// </summary>
        private void OnEnable()
        {
            OnChangeOutfit += SwitchOutfit;
        }

        /// <summary>
        /// OnDisable() is called before the object is disabled.
        /// </summary>
        private void OnDisable()
        {
            OnChangeOutfit -= SwitchOutfit;
        }

        /// <summary>
        /// Switches the players outfit material to the given material.
        /// </summary>
        /// <param name="outfitMaterial">The given outfit material.</param>
        public void SwitchOutfit(ItemModel _itemmodel)
        {
            for (int i = 0; i < playerMeshRenderers.Length; i++)
            {
                Material[] rendererMaterials = playerMeshRenderers[i].materials;
                rendererMaterials[1].mainTexture = _itemmodel.ItemTexture;
                playerMeshRenderers[i].materials = rendererMaterials;
            }
        }
    }
}