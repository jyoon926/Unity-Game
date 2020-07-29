using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessing : MonoBehaviour
{
    public FollowerController bird;
    public float sorrow;
    private Volume volume;
    private ColorAdjustments colorAdjustments;
    private FilmGrain filmGrain;
    private ChromaticAberration chromaticAberration;
    private Vignette vignette;
    private WhiteBalance whiteBalance;
    public Color vignetteColor;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Rendering.VolumeProfile volumeProfile = GetComponent<UnityEngine.Rendering.Volume>()?.profile;
        if(!volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));
        
        if(!volumeProfile.TryGet(out colorAdjustments)) throw new System.NullReferenceException(nameof(colorAdjustments));
        if(!volumeProfile.TryGet(out filmGrain)) throw new System.NullReferenceException(nameof(filmGrain));
        if(!volumeProfile.TryGet(out chromaticAberration)) throw new System.NullReferenceException(nameof(chromaticAberration));
        if(!volumeProfile.TryGet(out vignette)) throw new System.NullReferenceException(nameof(vignette));
        if(!volumeProfile.TryGet(out whiteBalance)) throw new System.NullReferenceException(nameof(whiteBalance));

        //InvokeRepeating("UpdatePostProcessing", 0.1f, 1f);
    }

    private void Update() {
        sorrow = bird.sorrow;
        chromaticAberration.intensity.Override(sorrow * 0.25f + 0.075f);
        vignette.intensity.Override(sorrow * .1f + 0.3f);
        //vignette.color.Override(Color.Lerp(vignetteColor, Color.black, sorrow - 0.2f));
        vignette.smoothness.Override(sorrow * 0.25f + 0.2f);
        filmGrain.intensity.Override(sorrow * 0.5f + 0.25f);
        colorAdjustments.contrast.Override(40f + sorrow * -40f);
        colorAdjustments.saturation.Override(sorrow * -60f - 10f);
        colorAdjustments.postExposure.Override(sorrow * 0.4f + 0.8f);
        whiteBalance.temperature.Override(sorrow * 90f - 10f);
    }

    void UpdatePostProcessing()
    {
    }
}
