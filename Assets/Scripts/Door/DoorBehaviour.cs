using System;
using UnityEngine;

// Specialized logic for operating the door tile.
public sealed class DoorBehaviour : MonoBehaviour
{
    // For resetting purposes.
    private bool initiallyOpen;

    [SerializeField]
    private AudioClip slide;
    private AudioSource slideSource;

    [SerializeField]
    private bool open = false;
    public bool IsOpen { get { return open; } }

    public event EventHandler Opened;
    public event EventHandler Closed;

    private void Awake()
    {
        initiallyOpen = open;

        slideSource = gameObject.AddComponent<AudioSource>();
        slideSource.clip = slide;
    }

    private void Start()
    {
        gameObject.SampleAnimation(animation.GetClip("Door." + (open ? "Close" : "Open")), 0);
    }

    private void Update()
    {
        audio.pitch = Time.timeScale;
    }

    private void Restart()
    {
        animation.Stop();

        open = initiallyOpen;
        Start();
    }

    public bool Open()
    {
        if (open)
            return false;

        animation.Play("Door.Open");
        slideSource.Play();

        open = true;

        return true;
    }

    public bool Close()
    {
        if (!open)
            return false;

        animation.Play("Door.Close");
        slideSource.Play();

        open = false;

        return true;
    }

    private void FireOpened()
    {
        if (Opened != null)
            Opened(this, EventArgs.Empty);
    }

    private void FireClosed()
    {
        if (Closed != null)
            Closed(this, EventArgs.Empty);
    }
}
