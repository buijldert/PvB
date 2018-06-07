using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.PostProcessing;

namespace RR.Managers
{
    /// <summary>
    /// This class manages the postprocessing profiles for differing platforms.
    /// </summary>
    [RequireComponent(typeof(PostProcessingBehaviour))]
    [ExecuteInEditMode]
    public class PostProcessingManager : MonoBehaviour
    {
        [Header("Post Processing Profiles")]
        [SerializeField] private PostProcessingProfile androidProfile;
        [SerializeField] private PostProcessingProfile iphoneProfile;
        [SerializeField] private PostProcessingProfile webglProfile;

        private PostProcessingBehaviour postProcessingBehaviour;
        
        private void Awake()
        {
            postProcessingBehaviour = GetComponent<PostProcessingBehaviour>();
            SelectProfile();
        }

        private void SelectProfile()
        {
#if UNITY_EDITOR
            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.Android:
                    postProcessingBehaviour.profile = androidProfile;
                    break;
                case BuildTarget.iOS:
                    postProcessingBehaviour.profile = iphoneProfile;
                    break;
                case BuildTarget.WebGL:
                    postProcessingBehaviour.profile = webglProfile;
                    break;
            }
#else
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    postProcessingBehaviour.profile = androidProfile;
                    break;
                case RuntimePlatform.IPhonePlayer:
                    postProcessingBehaviour.profile = iphoneProfile;
                    break;
                case RuntimePlatform.WebGLPlayer:
                    postProcessingBehaviour.profile = webglProfile;
                    break;
            }
#endif
        }
    }
}