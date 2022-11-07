using UnityEngine;

[ RequireComponent(typeof(AudioSource))]
public class Music : MonoBehaviour
{

    public AudioClip begin;
    public AudioClip loop;
    public AudioSource audioSource;
    /// <summary>
    /// ������ �� � ������� ���� private �������� ���������, 
    /// �� �������������� unity � �� �� ����� ��������
    /// ���������� ����� ��������� ����� �� ����
    /// </summary>


    private void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
    }
    public void Background(float volume)
    {
        audioSource.volume = volume;
        audioSource.clip = begin;
        audioSource.playOnAwake = true;
        audioSource.Play();
        Invoke(nameof(loopMusic), begin.length-0.2f);

    }
    public void loopMusic()
    {
        audioSource.clip = loop;
        audioSource.Play();
        audioSource.loop = true;
    }
    public void StopMusic()
    {
        if (audioSource != null)
            audioSource.Stop();
    }
}
