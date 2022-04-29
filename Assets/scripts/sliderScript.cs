using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class sliderScript : MonoBehaviour
{
    MeshFilter buttonMeshFilter;
    MeshFilter baseMeshFilter;

    private float yMax, yMin, yRest;
    private float passiveZ, activeZ;
    public bool isActive;
    public GameObject mesh;
    public LibPdInstance pd;
    public TextMeshPro valueField;
    Mouse mouse;
    public Material activeMaterial, passiveMaterial;
    MeshRenderer meshRenderer;
    public GameObject sliderBase;
    public Collider topCollider, bottomCollider;
    TreeGenerator treeGenerator;

    void OnValidate()
    {
        treeGenerator = GameObject.Find("Trees").GetComponent<TreeGenerator>();
        pd = GameObject.Find("synth new").GetComponent<LibPdInstance>();
        passiveZ = 1.6f;
        activeZ = .1f;
        isActive = false;
        mesh = this.transform.GetChild(0).gameObject;
        mouse = GameObject.Find("mouseController").GetComponent<Mouse>();
        meshRenderer = mesh.GetComponent<MeshRenderer>();
        sliderBase = this.transform.GetChild(2).gameObject;
        topCollider = sliderBase.transform.GetChild(1).GetComponent<Collider>();
        bottomCollider = sliderBase.transform.GetChild(2).GetComponent<Collider>();
        // setting the rest position of the slider
        yRest = mesh.transform.position.y;
        yMin = yRest - 2f;
        yMax = yRest + 2f;
        buttonMeshFilter = transform.GetChild(0).GetComponent<MeshFilter>();
        baseMeshFilter = transform.GetChild(2).GetComponent<MeshFilter>();
        // buttonMeshFilter.mesh = Resources.Load("Assets/prefabs/Models/sliderButton.prefab");
    }

    void OnDrawGizmos()
    {
        // MIN
        Gizmos.DrawSphere(
            new Vector3(
                mesh.transform.position.x,
                yMin,
                mesh.transform.position.z
            ),
            .1f
        );

        // REST
        Gizmos.DrawSphere(
            new Vector3(
                mesh.transform.position.x,
                yRest,
                mesh.transform.position.z
            ),
            .1f
        );


        // MAX
        Gizmos.DrawSphere(
            new Vector3(
                mesh.transform.position.x,
                yMax,
                mesh.transform.position.z
            ),
            .1f
        );
    }

    void Update()
    {
        if (this.isActive)
        {
            moveSlider();
        }

        float sliderYValue = this.transform.GetChild(0).transform.localPosition.y;

        // TODO this should be rounded to one or two decimals.
        // valueField.SetText(mesh.transform.localPosition.y.ToString());


        // this script is attached to all sliders, so check which one, we are grabbing.
        // **************************************** PAD ****************************************
        // TODO properly map these to correct values
        if (this.name == "PadVolume")
        {
            pd.SendFloat("chordVolume", sliderYValue);
            treeGenerator.updateTrees(Mathf.RoundToInt(sliderYValue) * 10);
        }
        else if (this.name == "PadFilter")
        {
            pd.SendFloat("chordCutoff", sliderYValue);
        }

        // ******************** CHORUS ********************
        else if (this.name == "ChorusMix")
        {
            pd.SendFloat("chorusMix", sliderYValue);
        }
        else if (this.name == "ChorusRate")
        {
            pd.SendFloat("chorusRate", sliderYValue);
        }
        else if (this.name == "ChorusDepth")
        {
            pd.SendFloat("chorusDepth", sliderYValue);
        }

        // TODO waveforms
        // **************************************** BASS ****************************************
        else if (this.name == "BassVolume")
        {
            pd.SendFloat("bassVolume", sliderYValue);
        }
        else if (this.name == "BassFilter")
        {
            pd.SendFloat("bassFilter", sliderYValue);
        }
        // TODO waveforms
        // TODO Rythm On/Off

        // **************************************** DRUMS ****************************************
        else if (this.name == "DrumVolume")
        {
            pd.SendFloat("drumVolume", sliderYValue);
        }
        else if (this.name == "DrumFilter")
        {
            pd.SendFloat("drumFilter", sliderYValue);
        }
        else if (this.name == "TempoSlider")
        {
            // TODO this one does not work yet.
            pd.SendFloat("tempo", sliderYValue * 1000);
        }
        // **************************************** ARP ****************************************
        else if (this.name == "ArpVolume")
        {
            pd.SendFloat("arpVolume", sliderYValue);
        }

        else if (this.name == "ArpFilter")
        {
            pd.SendFloat("arpCutoff", sliderYValue);
        }
        else if (this.name == "ArpReverbMix")
        {
            pd.SendFloat("arpVerbSize", sliderYValue);
        }
        // TODO waveform
    }

    /// <summary>
    /// Sets a slider to passive, including the position and the ability to move on the y-axis
    /// </summary>
    public void setPassive()
    {
        this.isActive = false;
        mesh.transform.position = new Vector3(
            mesh.transform.position.x,
            mesh.transform.position.y,
            mesh.transform.position.z
        );
        print("set slider to passive");
        meshRenderer.material = passiveMaterial;
    }
    public void setActive()
    {
        this.isActive = true;

        print("set slider to active");

        meshRenderer.material = activeMaterial;
    }

    void moveSlider()
    {
        if (mesh.transform.position.y > yMax)
        {
            mesh.transform.position = new Vector3(
                mesh.transform.position.x,
                yMax,
                mesh.transform.position.z
            );
        }
        else if (mesh.transform.position.y < yMin)
        {
            mesh.transform.position = new Vector3(
                mesh.transform.position.x,
                yMin,
                mesh.transform.position.z
            );
        }
        else
        {
            mesh.transform.position = new Vector3(
                mesh.transform.position.x,
                mouse.worldPosition.y,
                mesh.transform.position.z
            );
        }
    }
}
