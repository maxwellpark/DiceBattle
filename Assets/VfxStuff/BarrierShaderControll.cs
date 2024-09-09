using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[ImageEffectAllowedInSceneView]
public class BarrierShaderControll : MonoBehaviour
{
    public Material mat;

    private MeshRenderer _myRenderer;
    public int barrierState;
    private Barrier _myBarrier;
    private Dictionary<int, BarrierShaderSettings> _shaderSettings;

    private void Start()
    {
        _myRenderer = GetComponent<MeshRenderer>();
        _myBarrier = GetComponentInParent<Barrier>();
        _myBarrier.onDamageTaken += OnDamageTakenHandler;

        _shaderSettings = new Dictionary<int, BarrierShaderSettings>
        {
            { 0, new BarrierShaderSettings(0f, -1f, 0.8f, Color.blue) },
            { 1, new BarrierShaderSettings(0.2f ,0.5f,  0.7f, Color.cyan) },
            { 2, new BarrierShaderSettings(0.4f, 1f, 0.6f, Color.red) },
            { 3, new BarrierShaderSettings(0.8f, 2f, 0.5f, Color.red) }
        };
        SetShaderSettings();
    }

    private void SetShaderSettings()
    {
        if (!_shaderSettings.ContainsKey(barrierState))
            throw new System.Exception("Key does not exist in shader settings dictionary: " + barrierState);

        var settings = _shaderSettings[barrierState];
        // Adult Link settings: 
        //var currentEnabledDistortion = mat.GetFloat("_Enabledistortion");

        //_myRenderer.material.SetFloat("_Enabledistortion", settings.EnabledDistortion - currentEnabledDistortion);
        //_myRenderer.material.SetFloat("_Globalopacity", settings.GlobalOpacity);
        //_myRenderer.material.SetColor("_Maincolor", settings.MainColor);

        //if (settings.DistortionSpeed >= 0)
        //    _myRenderer.material.SetFloat("_Distortionspeed", settings.DistortionSpeed);

        _myRenderer.material.SetColor("_Color", settings.BaseColor);
    }

    private void OnDamageTakenHandler()
    {
        ChangeState();
        SetShaderSettings();
    }

    private void ChangeState()
    {
        barrierState = Mathf.Min(barrierState + 1, 3);
        barrierState = Mathf.Max(barrierState, 0);
    }

    private void OnDestroy()
    {
        if (_myBarrier != null)
            _myBarrier.onDamageTaken -= ChangeState;
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateHandler();

    }

    private void UpdateHandler()
    {
        //one hit
        if (barrierState == 0)
        {

            _myRenderer.material.SetFloat("_Enabledistortion", 0f);
            _myRenderer.material.SetFloat("_Globalopacity", 0.8f);
            _myRenderer.material.SetColor("_Maincolor", Color.cyan);



        }




        //one hit
        if (barrierState == 1)
        {
            _myRenderer.material.SetFloat("_Enabledistortion", 0.2f - mat.GetFloat("_Enabledistortion"));
            _myRenderer.material.SetFloat("_Globalopacity", 0.7f);
            _myRenderer.material.SetFloat("_Distortionspeed", 0.5f);

        }

        //one hit
        if (barrierState == 2)
        {
            _myRenderer.material.SetFloat("_Enabledistortion", 0.4f - mat.GetFloat("_Enabledistortion"));
            _myRenderer.material.SetFloat("_Globalopacity", 0.6f);
            _myRenderer.material.SetColor("_Maincolor", Color.red);
            _myRenderer.material.SetFloat("_Distortionspeed", 1f);

        }

        //almost destroyed
        if (barrierState == 3)
        {
            _myRenderer.material.SetFloat("_Enabledistortion", 0.8f - mat.GetFloat("_Enabledistortion"));
            _myRenderer.material.SetFloat("_Globalopacity", 0.5f);
            _myRenderer.material.SetFloat("_Distortionspeed", 2f);
        }
    }
}
