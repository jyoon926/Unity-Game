using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;
	public AudioMixer mixer;


	public Sound[] sounds;
	public int musicVolume;
	public int soundVolume;
	public bool fullscreen = true;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = s.mixerGroup;
		}
		musicVolume = 10;
		soundVolume = 10;
		fullscreen = true;
	}

	private void Update() {
		if (musicVolume > 0) {
			mixer.SetFloat("MusicVolume", (musicVolume * 3f) - 31f);
		}
		else {
			mixer.SetFloat("MusicVolume", -80f);
		}
		if (soundVolume > 0) {
			mixer.SetFloat("MasterVolume", (soundVolume * 3f) - 31f);
		}
		else {
			mixer.SetFloat("MasterVolume", -80f);
		}
        if (fullscreen) {
			Screen.fullScreen = true;
			Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else {
			Screen.fullScreen = false;
			Screen.fullScreenMode = FullScreenMode.Windowed;
        }
	}

	private void Start() {
		if (SceneManager.GetActiveScene().buildIndex == 0)
			Play("Theme 1");
	}

	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
		s.source.outputAudioMixerGroup = s.mixerGroup;
		s.source.Play();
	}
	public bool IsPlaying(string sound) {
		Sound s = Array.Find(sounds, item => item.name == sound);
		
		return s.source.isPlaying;
			
	}
}
