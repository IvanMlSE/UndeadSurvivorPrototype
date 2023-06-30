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
    private AudioSource _�udioSource01;
    private AudioSource _�udioSource02;
    private Coroutine _coroutine01;
    private Coroutine _coroutine02;

    private const float MinVolume = 0;

    private void Awake()
    {
        _isPlayingAudioSource01 = true;

        _�udioSource01 = gameObject.AddComponent<AudioSource>();

        _�udioSource01.volume = MinVolume;
        _�udioSource01.loop = true;
        _�udioSource01.playOnAwake = false;

        _�udioSource02 = gameObject.AddComponent<AudioSource>();

        _�udioSource02.volume = MinVolume;
        _�udioSource02.loop = true;
        _�udioSource02.playOnAwake = false;
    }

    public void PlaySound(SoundType soundType)
    {
        MakeSmoothSoundTransition(soundType);
    }

    private void MakeSmoothSoundTransition(SoundType soundType)
    {
        if (_isPlayingAudioSource01)
        {
            SetAudioClip(_�udioSource02, soundType);
            ChangeVolumeSmoothly(_�udioSource02, ref _coroutine02, _maxVolume);

            ChangeVolumeSmoothly(_�udioSource01, ref _coroutine01, MinVolume);
        }
        else
        {
            SetAudioClip(_�udioSource01, soundType);
            ChangeVolumeSmoothly(_�udioSource01, ref _coroutine01, _maxVolume);

            ChangeVolumeSmoothly(_�udioSource02, ref _coroutine02, MinVolume);
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