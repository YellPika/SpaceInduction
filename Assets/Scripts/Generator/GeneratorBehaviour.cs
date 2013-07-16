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
                gameObject.AddComponent<SelfPowerSource>();

                var setSource = gameObject.AddComponent<SetPowerSource>();
                foreach (var target in targets)
                {
                    target.enabled = true;
                    setSource.Targets.Add(target.GetComponent<PowerProperty>());
                }

                animation.Play();
            };
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
