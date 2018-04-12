using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitcher : MonoBehaviour {

    private MeshRenderer _mr;

    private void Start()
    {
        _mr = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (gameObject.layer == 11)
            {
                gameObject.layer = 10;
                _mr.material.DOColor(Color.white, 0.5f);
            }
            else
            {
                gameObject.layer = 11;
                _mr.material.DOColor(Color.black, 0.5f);
            }
        }
    }
}
