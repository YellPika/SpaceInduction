using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public sealed class AlienPowerSource : PowerSource
{
    [SerializeField]
    private AudioClip drain;
    private AudioSource drainSource;

    [SerializeField]
    private float power = 0.75f;
    private HashSet<PowerProperty> targets = new HashSet<PowerProperty>();

    private void Awake()
    {
        drainSource = gameObject.AddComponent<AudioSource>();
        drainSource.clip = drain;
        drainSource.loop = true;
        drainSource.mute = true;
        drainSource.Play();
    }

    private void OnTriggerEnter(Collider collider)
    {
        var property = collider.GetComponent<PowerProperty>();
        if (property != null)
            targets.Add(property);
    }

    private void OnTriggerExit(Collider collider)
    {
        var property = collider.GetComponent<PowerProperty>();
        if (property != null)
            targets.Remove(property);
    }

    private void Update()
    {
        var mute = true;

        foreach (var target in targets)
        {
            if (target.GetComponent<PlayerBehaviour>() != null)
            {
                target.Apply(power * Time.deltaTime * -0.5f);
                mute = false;
            }
            else
                target.Apply(-power);
        }

        drainSource.mute = mute;
    }

    private void Restart()
    {
        targets.Clear();
        collider.enabled = false;
        
        // This is a hack, because for some stupid reason, iterator coroutines won't work.
        Invoke("Reenable", 0);
    }

    private void Reenable()
    {
        collider.enabled = true;
    }
}
