using UnityEngine;

public sealed class DoorBehaviour : MonoBehaviour
{
    [SerializeField]
    private bool open = false;
    private bool initialState;

    private void Awake()
    {
        initialState = open;

        if (open)
            animation.Play("Door.Open");
    }

    private void Restart()
    {
        if (initialState)
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
        
        return true;
    }

    public bool Close()
    {
        if (!open)
            return false;

        animation.Play("Door.Close");
        open = false;

        return true;
    }
}
