using System;
using UnityEngine;

// Specialized logic for operating the door tile.
public sealed class DoorBehaviour : MonoBehaviour
{
    [SerializeField]
    private bool open = false;

    // For resetting purposes.
    private bool initiallyOpen;

    public bool IsOpen { get { return open; } }

    public event EventHandler Opened;
    public event EventHandler Closed;

    private void Awake()
    {
        initiallyOpen = open;

        if (open)
            animation.Play("Door.Open");
    }

    private void Restart()
    {
        if (initiallyOpen)
            Open();
        else
            Close();
    }

    public bool Open()
    {
        if (open)
            return false;
        
        animation.Play("Door.Open");
        open = true;

        if (Opened != null)
            Opened(this, EventArgs.Empty);
        
        return true;
    }

    public bool Close()
    {
        if (!open)
            return false;

        animation.Play("Door.Close");
        open = false;

        if (Closed != null)
            Closed(this, EventArgs.Empty);

        return true;
    }
}
