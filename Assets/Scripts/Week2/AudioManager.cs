using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private AudioSource bgmSource;
    [SerializeField][Range(0f, 1f)] private float bgmVolume = 0.3f;

    protected override void Awake()
    {
        base.Awake();

        bgmSource = new GameObject { name = "BGM" }.AddComponent<AudioSource>();
        bgmSource.transform.SetParent(transform);
    }

    private void Start()
    {
        PlayBgm();
    }

    private void Update()
    {
        VolumeUpdate(bgmVolume);
    }

    private void VolumeUpdate(float value)
    {
        if (bgmSource.volume != value)
            bgmSource.volume = value;
    }

    public void SetVolume(float value)
    {
        VolumeUpdate(value);
    }

    public void PlayBgm()
    {
        AudioClip bgm = Resources.Load<AudioClip>("Sounds/bgm");
        if (bgm == null)
        {
            Debug.LogError("BGM is not found");
            return;
        }

        bgmSource.clip = bgm;
        bgmSource.loop = true;
        bgmSource.Play();
    }
}
