using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public sealed class AlienPowerSource : PowerSource
{
    [SerializeField]
    private AudioClip drain;
    private AudioSource drainSource;

    private float drainVolume;
    private float targetDrainVolume;

    [SerializeField]
    private float power = 0.75f;
    private HashSet<PowerProperty> targets = new HashSet<PowerProperty>();

    private void Awake()
    {
        drainSource = gameObject.AddComponent<AudioSource>();
        drainSource.clip = drain;
        drainSource.loop = true;
        drainSource.volume = 0;
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
        targetDrainVolume = 0;

        foreach (var target in targets)
        {
            if (target.GetComponent<PlayerBehaviour>() != null)
            {
                target.Apply(power * Time.deltaTime * -0.5f);
                targetDrainVolume = 1;
            }
            else
                target.Apply(-power);
        }

        drainVolume = Mathf.Clamp01(drainVolume + Mathf.Sign(targetDrainVolume - drainVolume) * 4 * Time.deltaTime);
        drainSource.volume = Mathf.Pow(drainVolume, 4);
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
