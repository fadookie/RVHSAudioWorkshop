using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Slider _mainSlider;
    [SerializeField] private AudioMixer _mainMixer;

    // Update is called once per frame
    void Update()
    {
        const int MixerVolumeChangeDeltaDb = 10;
        const string MasterVolParam = "masterVol";
        
        if (Input.GetKeyDown("-"))
        {
            ModifyMixerVolume(MasterVolParam, -MixerVolumeChangeDeltaDb);
        }
        else if (Input.GetKeyDown("="))
        {
            ModifyMixerVolume(MasterVolParam, MixerVolumeChangeDeltaDb);
        }
        
        UpdateMixerUI(MasterVolParam, _mainSlider);
        
        return;
        
        void ModifyMixerVolume(string mixerParam, int delta)
        {
            _mainMixer.GetFloat(mixerParam, out var volume);
            var newVolume = volume + delta;
            newVolume = Math.Clamp(newVolume, -80, 20);
            if (Mathf.Approximately(newVolume, volume)) return;
            Debug.Log($"ModifyMixerVolume changing {mixerParam} to {newVolume} dB");
            _mainMixer.SetFloat(mixerParam, newVolume);
        }

        void UpdateMixerUI(string mixerParam, Slider slider)
        {
            _mainMixer.GetFloat(mixerParam, out var volume);
            var normalizedVolume = Mathf.InverseLerp(-80f, 20f, volume);
            slider.value = normalizedVolume;
        }
    }
}
