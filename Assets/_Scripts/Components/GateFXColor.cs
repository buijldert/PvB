using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RR.Components
{
    /// <summary>
    /// This class is responsible for saving performace when switching particle colors.
    /// </summary>
    public class GateFXColor : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] gateParticleSystems;

        /// <summary>
        /// Changes the color of the gate particlesystems.
        /// </summary>
        /// <param name="color"></param>
        public void ChangeParticleColor(Color color)
        {
            for (int i = 0; i < gateParticleSystems.Length; i++)
            {
                var main = gateParticleSystems[i].main;
                main.startColor = color;
            }
        }
    }
}