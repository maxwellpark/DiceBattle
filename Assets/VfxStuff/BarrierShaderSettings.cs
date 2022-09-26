using UnityEngine;

public class BarrierShaderSettings
{
    public float EnabledDistortion { get; set; }
    public float DistortionSpeed { get; }
    public float GlobalOpacity { get; set; }
    public Color MainColor { get; set; }

    public BarrierShaderSettings(float enabledDistortion, float distortionSpeed, float globalOpacity, Color mainColor)
    {
        EnabledDistortion = enabledDistortion;
        DistortionSpeed = distortionSpeed;
        GlobalOpacity = globalOpacity;
        MainColor = mainColor;
    }
}