using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class AudioManager : MonoBehaviour
{
    public AudioMixer gameAudioMixer; // Reference to the AudioMixer for the game
    public Slider musicVolumeSlider; // Reference to the Slider for adjusting music volume
    public Slider sfxVolumeSlider; // Reference to the Slider for adjusting SFX volume
    public TextMeshProUGUI musicVolumeText; // Reference to the TextMeshProUGUI for displaying music volume
    public TextMeshProUGUI sfxVolumeText; // Reference to the TextMeshProUGUI for displaying SFX volume

    void Start()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f); // Load the music volume from player preferences, or use 0.5f if not set
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f); // Load the SFX volume from player preferences, or use 0.5f if not set

        musicVolumeSlider.value = musicVolume; // Set the initial value of the music volume slider
        sfxVolumeSlider.value = sfxVolume; // Set the initial value of the SFX volume slider

        SetMusicVolume(musicVolume); // Apply and display the loaded music volume
        SetSFXVolume(sfxVolume); // Apply and display the loaded SFX volume
    }

    public void SetMusicVolume(float volume)
    {
        Debug.Log($"Setting music volume to: {volume}"); // Log the new music volume
        gameAudioMixer.SetFloat("MusicVol", Mathf.Log10(volume) * 20); // Set the "MusicVol" exposed parameter on the AudioMixer
        PlayerPrefs.SetFloat("MusicVolume", volume); // Save the music volume to player preferences
        PlayerPrefs.Save(); // Save the player preferences
        musicVolumeText.text = $"{(int)(volume * 100)}%"; // Update the music volume text display
    }

    public void SetSFXVolume(float volume)
    {
        gameAudioMixer.SetFloat("SFXVol", Mathf.Log10(volume) * 20); // Set the "SFXVol" exposed parameter on the AudioMixer
        PlayerPrefs.SetFloat("SFXVolume", volume); // Save the SFX volume to player preferences
        PlayerPrefs.Save(); // Save the player preferences
        sfxVolumeText.text = $"{(int)(volume * 100)}%"; // Update the SFX volume text display
    }
}