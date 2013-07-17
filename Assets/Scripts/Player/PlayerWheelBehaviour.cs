using UnityEngine;

public sealed class PlayerWheelBehaviour : MonoBehaviour
{
    public float MoveSpeed = 10;
    public float TurnSpeed = 135;
    public float TurnAmount = 0;

    private void Update()
    {
        var joint = GetComponent<ConfigurableJoint>();
        joint.targetAngularVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow))
            joint.targetAngularVelocity += Vector3.right * MoveSpeed;
        if (Input.GetKey(KeyCode.DownArrow))
            joint.targetAngularVelocity += Vector3.left * MoveSpeed;

        if (Input.GetKey(KeyCode.LeftArrow))
            TurnAmount += TurnSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.RightArrow))
            TurnAmount -= TurnSpeed * Time.deltaTime;

        var radians = TurnAmount * Mathf.Deg2Rad;
        joint.axis = new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians));
    }
}
