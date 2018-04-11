using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    [RequireComponent(typeof(MeshRenderer))]
    public class RaceTrack : MonoBehaviour
    {
        private MeshRenderer _trackRenderer;

        private Vector2 _textureOffset;
        private float _speed = 0.5f;

        void Start()
        {
            _trackRenderer = GetComponent<MeshRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            _textureOffset = new Vector2(0, Time.time * _speed);

            _trackRenderer.material.mainTextureOffset = _textureOffset;
        }
    }
}