using System;
using System.Linq;
using UnityEngine;

public sealed class GeneratorBehaviour : MonoBehaviour
{
    [SerializeField]
    private PowerSource[] targets;

    public event EventHandler Started;

    private void Awake()
    {
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
        };
    }

    private void Restart()
    {
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
