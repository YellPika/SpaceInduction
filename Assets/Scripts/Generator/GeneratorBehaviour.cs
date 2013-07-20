using System.Linq;
using UnityEngine;

public sealed class GeneratorBehaviour : MonoBehaviour
{
    [SerializeField]
    private PowerSource[] targets;

    // For resetting purposes.
    private bool[] initialValues;

    private void Awake()
    {
        initialValues = targets
            .Select(n => n.enabled)
            .ToArray();

        GetComponentInChildren<GeneratorTriggerBehaviour>().Triggered +=
            (sender, e) =>
            {

                animation.Play("Generator.Start");
                gameObject.AddComponent<SelfPowerSource>();

                var setSource = gameObject.AddComponent<SetPowerSource>();
                setSource.Targets.AddRange(targets.Select(n => n.GetComponent<PowerProperty>()));

                foreach (var target in targets)
                    target.enabled = true;
            };
    }

    private void Restart()
    {
        animation.Stop();

        var setSource = GetComponent<SetPowerSource>();
        if (setSource != null)
            Destroy(setSource);

        for (int i = 0; i < targets.Length; i++)
            targets[i].enabled = initialValues[i];
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
