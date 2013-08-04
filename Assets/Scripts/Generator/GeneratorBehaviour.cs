using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public sealed class GeneratorBehaviour : MonoBehaviour
{
    [SerializeField]
    private AudioClip running;
    private AudioSource runningSource;

    [SerializeField]
    private PowerSource[] targets;

    public event EventHandler Started;

    private void Awake()
    {
        runningSource = gameObject.AddComponent<AudioSource>();
        runningSource.clip = running;
        runningSource.pitch = 0;
        runningSource.loop = true;
        runningSource.Play();

        var trigger = GetComponentInChildren<GeneratorTrigger>();
        trigger.Triggered += (sender, e) =>
        {
            // Only do this when all the rods are inserted.
            if (trigger.InsertionCount != trigger.RodCount)
                return;

            animation.Play();

            var setSource = gameObject.AddComponent<SetPowerSource>();
            foreach (var target in targets)
            {
                var power = target.GetComponent<PowerProperty>();
                if (power != null)
                    setSource.Targets.Add(power);

                target.enabled = true;
            }

            if (Started != null)
                Started(this, EventArgs.Empty);

            StartCoroutine(IncreasePitch());
        };
    }

    private IEnumerator IncreasePitch()
    {
        while (audio.pitch < 3)
        {
            runningSource.pitch += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Restart()
    {
        runningSource.pitch = 0;

        animation.Stop();
        gameObject.SampleAnimation(animation.GetClip("Generator.Start"), 0);

        var setSource = GetComponent<SetPowerSource>();
        if (setSource != null)
            Destroy(setSource);

        foreach (var target in targets)
            target.enabled = false;
    }

    private void Spin()
    {
        animation.Play("Generator.Spin");
    }

#if UNITY_EDITOR
    private void Reset()
    {
        if (transform.parent == null)
            return;

        targets = FindSceneObjectsOfType(typeof(PowerSource))
            .Cast<PowerSource>()
            .Where(n => !n.enabled)
            .ToArray();
    }
#endif
}
