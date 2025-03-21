using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    const string MasterVolParam = "masterVol";
    
    [SerializeField] private Slider _mainSlider;

    private readonly Dictionary<string, float> _mockMixer = new() // TODO: Replace with a real mixer!
    {
        [MasterVolParam] = 0f,
    };

    // Update is called once per frame
    void Update()
    {
        const int MixerVolumeChangeDeltaDb = 10;
        
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
            var volume = _mockMixer[mixerParam];
            var newVolume = volume + delta;
            newVolume = Math.Clamp(newVolume, -80, 20);
            if (Mathf.Approximately(newVolume, volume)) return;
            Debug.Log($"ModifyMixerVolume changing {mixerParam} to {newVolume} dB");
            _mockMixer[mixerParam] = newVolume;
        }

        void UpdateMixerUI(string mixerParam, Slider slider)
        {
            var volume = _mockMixer[mixerParam];
            var normalizedVolume = Mathf.InverseLerp(-80f, 20f, volume);
            slider.value = normalizedVolume;
        }
    }
}
