using System;
using UnityEngine;
using RR.Components.Player;
using RR.Controllers;
using RR.Components;
using System.Collections;
using RR.Audio;

namespace RR.Handlers
{
    /// <summary>
    /// This class is responsible for handling the collision between the player and the gates.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class CollisionHandler : MonoBehaviour
    {
        [Header("GameObjects")]
        [SerializeField] private GameObject fadeParticleGameObject;
        [SerializeField] private GameObject deathParticleGameObject;

        [Header("Colors")]
        [SerializeField] private Color pinkColor;
        [SerializeField] private Color blueColor;

        [Header("AudioClips")]
        [SerializeField]
        private AudioClip deathSound;
        [SerializeField] private AudioClip fadeThroughSound;

        private const string WHITE_OBSTACLE_TAG = "WhiteObstacle";
        private const string BLACK_OBSTACLE_TAG = "BlackObstacle";

        public static Action<int> OnFadeThroughCollision;
        public static Action OnDeadlyCollision;

        private PlayerMovement playerMovement;

        private void Start()
        {
            playerMovement = GetComponent<PlayerMovement>();
        }

        /// <summary>
        /// Handles the collision of the player with the different obstacles.
        /// </summary>
        /// <param name="_collision">The collider the player is colliding with.</param>
        private void OnTriggerEnter(Collider _collision)
        {
            if ((_collision.gameObject.tag == WHITE_OBSTACLE_TAG && playerMovement.GetPlayerColor() == PlayerColor.Pink) || 
                (_collision.gameObject.tag == BLACK_OBSTACLE_TAG && playerMovement.GetPlayerColor() == PlayerColor.Blue))
            {
                if (OnDeadlyCollision != null)
                {
                    SFXManager.instance.PlaySound(deathSound);
                    deathParticleGameObject.SetActive(true);
                    StartCoroutine(DeathParticleDelay());
                    OnDeadlyCollision();

                    //TODO: put this somewhere else..
                    if(SettingsController.GetVibrationState())
                    {
                        Handheld.Vibrate();
                    }
                }
                    
            }
            else if(_collision.gameObject.tag == WHITE_OBSTACLE_TAG || _collision.gameObject.tag == BLACK_OBSTACLE_TAG)
            {
                if (OnFadeThroughCollision != null)
                {
                    SFXManager.instance.PlaySound(fadeThroughSound);
                    GameObject fadeParticleClone = ObjectPool.instance.GetObjectForType(fadeParticleGameObject.name, false);
                    fadeParticleClone.transform.position = _collision.transform.position;
                    GateFXColor gateFXColor = fadeParticleClone.GetComponent<GateFXColor>();
                    if (_collision.gameObject.tag == WHITE_OBSTACLE_TAG)
                    {
                        gateFXColor.ChangeParticleColor(blueColor);
                    }
                    else
                    {
                        gateFXColor.ChangeParticleColor(pinkColor);
                    }
                    
                    OnFadeThroughCollision(10);
                }
            }
        }

        private IEnumerator DeathParticleDelay()
        {
            yield return new WaitForSeconds(2f);
            deathParticleGameObject.SetActive(false);
        }

        private void OnDisable()
        {
            deathParticleGameObject.SetActive(false);
        }
    }
}