using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        public static Action OnLeftMouseButtonDown;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (OnLeftMouseButtonDown != null)
                    OnLeftMouseButtonDown();
            }
        }
    }
}