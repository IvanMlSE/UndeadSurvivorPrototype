using System.Collections;
using UnityEngine;

public class GameAmbient : MonoBehaviour
{
    [SerializeField] private float _volumeChangeRate;
    [SerializeField] private float _maxVolume;
    [SerializeField] private AudioClip _startingGameSound;
    [SerializeField] private AudioClip _endingGameSound;
    [SerializeField] private AudioClip[] _ambientSounds;

    private bool _isPlayingAudioSource01;
    private AudioSource _àudioSource01;
    private AudioSource _àudioSource02;
    private Coroutine _coroutine01;
    private Coroutine _coroutine02;

    private const float MinVolume = 0;

    private void Awake()
    {
        _isPlayingAudioSource01 = true;

        _àudioSource01 = gameObject.AddComponent<AudioSource>();

        _àudioSource01.volume = MinVolume;
        _àudioSource01.loop = true;
        _àudioSource01.playOnAwake = false;

        _àudioSource02 = gameObject.AddComponent<AudioSource>();

        _àudioSource02.volume = MinVolume;
        _àudioSource02.loop = true;
        _àudioSource02.playOnAwake = false;
    }

    public void PlaySound(SoundType soundType)
    {
        MakeSmoothSoundTransition(soundType);
    }

    private void MakeSmoothSoundTransition(SoundType soundType)
    {
        if (_isPlayingAudioSource01)
        {
            SetAudioClip(_àudioSource02, soundType);
            ChangeVolumeSmoothly(_àudioSource02, ref _coroutine02, _maxVolume);

            ChangeVolumeSmoothly(_àudioSource01, ref _coroutine01, MinVolume);
        }
        else
        {
            SetAudioClip(_àudioSource01, soundType);
            ChangeVolumeSmoothly(_àudioSource01, ref _coroutine01, _maxVolume);

            ChangeVolumeSmoothly(_àudioSource02, ref _coroutine02, MinVolume);
        }

        _isPlayingAudioSource01 = !_isPlayingAudioSource01;
    }

    private void SetAudioClip(AudioSource audioSource, SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.StartingGame:
                audioSource.clip = _startingGameSound;
                break;

            case SoundType.EndingGame:
                audioSource.clip = _endingGameSound;
                break;

            case SoundType.Ambient:
                audioSource.clip = _ambientSounds[Random.Range(0, _ambientSounds.Length)];
                break;
        }
    }

    private void ChangeVolumeSmoothly(AudioSource audioSource, ref Coroutine coroutine, float targetVolume)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(ChangeVolumeSmoothlyCoroutine(audioSource, targetVolume));
    }

    private IEnumerator ChangeVolumeSmoothlyCoroutine(AudioSource audioSource, float targetVolume)
    {
        if (targetVolume == _maxVolume)
        {
            audioSource.Play();
        }

        while (audioSource.volume != targetVolume)
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, targetVolume, _volumeChangeRate);

            if (audioSource.volume == MinVolume)
            {
                audioSource.Stop();
            }

            yield return null;
        }
    }

    public enum SoundType
    {
        StartingGame,
        EndingGame,
        Ambient
    }
}