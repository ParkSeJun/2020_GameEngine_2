using UnityEngine;

public class BgmPlayer : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1.0f)] float volume;

    AudioSource music;
    float curVolume;

    private void Awake()
    {
        music = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        music.loop = true;
        curVolume = volume;
        music.volume = volume;
        music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (volume != curVolume)
        {
            music.volume = volume;
            curVolume = volume;
        }
    }
}
