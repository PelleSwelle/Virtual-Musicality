using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonState : MonoBehaviour
{
    public Material activeMaterial, passiveMaterial;
    MeshRenderer meshRenderer;

    public bool isPressed;

    void OnValidate()
    {
        meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
        meshRenderer.material = passiveMaterial;
    }


    void Start()
    {

        this.isPressed = false;
    }
}
