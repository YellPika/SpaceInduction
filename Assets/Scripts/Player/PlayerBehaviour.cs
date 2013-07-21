using UnityEngine;

[RequireComponent(typeof(PowerProperty))]
[RequireComponent(typeof(RespawnPointInventory))]
[RequireComponent(typeof(LevelInventory))]
public sealed class PlayerBehaviour : MonoBehaviour
{
    private PlayerWheelBehaviour wheel;
    private Transform body;
    private Vector3 offset;

    private PowerProperty power;
    private RespawnPointInventory respawnPoint;
    private LevelInventory level;

    private void Awake()
    {
        wheel = transform.parent.FindChild("Wheel").GetComponent<PlayerWheelBehaviour>();
        body = transform.parent.FindChild("Body");
        offset = transform.position - wheel.transform.position;

        power = GetComponent<PowerProperty>();
        respawnPoint = GetComponent<RespawnPointInventory>();
        level = GetComponent<LevelInventory>();
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, -wheel.TurnAmount, 0);
        transform.position = wheel.transform.position + offset;

        if (power.Value == 0 && level.Current != null)
        {
            level.Current.Restart();

            var wheelOffset = wheel.transform.position - transform.position;
            var bodyOffset = body.transform.position - transform.position;

            transform.position = respawnPoint.Current.transform.position;

            wheel.transform.position = transform.position + wheelOffset;
            wheel.rigidbody.velocity = Vector3.zero;

            body.transform.position = transform.position + bodyOffset;
            body.rigidbody.velocity = Vector3.zero;

            wheel.TurnAmount = respawnPoint.Current.transform.rotation.eulerAngles.y;
        }
    }
}
