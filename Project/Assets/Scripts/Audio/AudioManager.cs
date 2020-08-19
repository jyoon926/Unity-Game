using UnityEngine.Audio;
using System;
using System.Collections;
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
	private bool musicIsPlaying;
	public string currentSong;
	public AudioSource currentSource;
	public bool fullscreen = true;

	void Awake() {
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
		if (SceneManager.GetActiveScene().buildIndex == 0) {
			currentSong = null;
			currentSource = null;
			PlayMusic("Theme 1");
		}
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

	public void PlayMusic(string sound) {
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		currentSource = s.source;
		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
		s.source.outputAudioMixerGroup = s.mixerGroup;
		s.source.Play();
		currentSong = sound;
	}
	public void PlayMusicWithFade(string sound, float transitionTime = 2.0f) {
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		StartCoroutine(UpdateMusicWithFade(currentSong, s, transitionTime));
		currentSong = sound;
		currentSource = s.source;
	}
	private IEnumerator UpdateMusicWithFade(string activeString, Sound newSound, float transitionTime) {
		Sound active = Array.Find(sounds, item => item.name == activeString);
		
		for (float t = 0.0f; t < transitionTime; t += Time.deltaTime) {
			active.source.volume = (1f - (t / transitionTime)) * active.volume;
			yield return null;
		}
		active.source.Stop();
		newSound.source.volume = newSound.volume * (1f + UnityEngine.Random.Range(-newSound.volumeVariance / 2f, newSound.volumeVariance / 2f));
		newSound.source.pitch = newSound.pitch * (1f + UnityEngine.Random.Range(-newSound.pitchVariance / 2f, newSound.pitchVariance / 2f));
		newSound.source.outputAudioMixerGroup = newSound.mixerGroup;
		newSound.source.Play();
		for (float t = 0.0f; t < transitionTime; t += Time.deltaTime) {
		 	newSound.source.volume = t / transitionTime;
		 	yield return null;
		}
	}
	public void FadeMusic(string sound, float transitionTime = 2f) {
		StartCoroutine(FadeOutMusic(currentSong, transitionTime));
	}
	private IEnumerator FadeOutMusic(string activeString, float transitionTime) {
		Sound active = Array.Find(sounds, item => item.name == activeString);
		for (float t = 0.0f; t < transitionTime; t += Time.deltaTime) {
			active.source.volume = (1f - (t / transitionTime)) * active.volume;
			yield return null;
		}
		active.source.Stop();
	}
	public bool IsPlaying(string sound) {
		Sound s = Array.Find(sounds, item => item.name == sound);
		
		return s.source.isPlaying;
	}
}