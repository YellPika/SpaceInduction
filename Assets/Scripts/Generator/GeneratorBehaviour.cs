using System.Linq;
using UnityEngine;

public sealed class GeneratorBehaviour : MonoBehaviour
{
    [SerializeField]
    private PowerSource[] targets;

    private void Awake()
    {
        GetComponentInChildren<GeneratorTriggerBehaviour>().Triggered +=
            (sender, e) =>
            {

                animation.Play("Generator.Start");
                gameObject.AddComponent<SelfPowerSource>();

                var setSource = gameObject.AddComponent<SetPowerSource>();
                foreach (var target in targets)
                {
                    target.enabled = true;
                    setSource.Targets.Add(target.GetComponent<PowerProperty>());
                }
            };
    }

    private void Spin()
    {
        animation.Play("Generator.Spin");
    }

    private void Restart()
    {
        animation.Stop();

        var setSource = GetComponent<SetPowerSource>();
        if (setSource != null)
            Destroy(setSource);

        foreach (var target in targets)
            target.enabled = false;
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
