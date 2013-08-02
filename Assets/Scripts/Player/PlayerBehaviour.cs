using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PowerProperty))]
[RequireComponent(typeof(RespawnPointInventory))]
[RequireComponent(typeof(LevelInventory))]
[RequireComponent(typeof(BoxMover))]
public sealed class PlayerBehaviour : MonoBehaviour
{
    private PlayerWheelBehaviour wheel;
    private Transform body;
    private Vector3 offset;

    private PowerProperty power;
    private RespawnPointInventory respawnPoint;
    private LevelInventory level;
    private BoxMover mover;

    private void Awake()
    {
        wheel = transform.parent.FindChild("Wheel").GetComponent<PlayerWheelBehaviour>();
        body = transform.parent.FindChild("Body");
        offset = transform.position - wheel.transform.position;

        power = GetComponent<PowerProperty>();
        respawnPoint = GetComponent<RespawnPointInventory>();
        level = GetComponent<LevelInventory>();
        mover = GetComponent<BoxMover>();

        power.Changed += (sender, e) =>
            {
                if (e.Value != 0 || level.Current == null)
                    return;

                level.Current.Restart();

                // DO NOT do this before/in Restart(), otherwise the player could reset before
                // the rest of the level, resulting in some strange stuff.
                Teleport(respawnPoint.transform.position, respawnPoint.transform.rotation);
            };
    }

    private void Update()
    {
        transform.rigidbody.MoveRotation(Quaternion.Euler(0, -wheel.TurnAmount, 0));
        transform.rigidbody.MovePosition(wheel.transform.position + offset);

        mover.IsActivated = Input.GetKey(KeyCode.Space);
    }

    public void Teleport(Vector3 position, Quaternion rotation)
    {
        var wheelOffset = wheel.transform.position - transform.position;
        var bodyOffset = body.transform.position - transform.position;

        transform.position = position;

        body.transform.position = transform.position + bodyOffset;
        body.rigidbody.velocity = Vector3.zero;
        body.rigidbody.angularVelocity = Vector3.zero;

        wheel.transform.position = transform.position + wheelOffset;
        wheel.TurnAmount = -rotation.eulerAngles.y;
        wheel.rigidbody.velocity = Vector3.zero;
        wheel.rigidbody.angularVelocity = Vector3.zero;

        body.rigidbody.Sleep();
        wheel.rigidbody.Sleep();
    }
}
