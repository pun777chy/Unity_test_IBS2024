using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    // The size of the zone within which the object can move
    public Vector3 zoneSize = new Vector3(10, 10, 10);

    // The target position to move towards
    private Vector3 targetPosition;

    // Called when the script instance is being loaded
    private void Start()
    {
        // Set an initial random target position within the zone
        SetRandomTargetPosition();
    }

    // Called once per frame
    private void Update()
    {
        // Move the object towards the target position
        MoveTowardsTarget();
    }

    // Sets a new random target position within the defined zone
    private void SetRandomTargetPosition()
    {
        targetPosition = new Vector3(
            Random.Range(-zoneSize.x / 2, zoneSize.x / 2),
            Random.Range(-zoneSize.y / 2, zoneSize.y / 2),
            Random.Range(-zoneSize.z / 2, zoneSize.z / 2)
        );
    }

    // Moves the object towards the target position
    private void MoveTowardsTarget()
    {
        // Move the object towards the target position at a constant speed
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime);

        // If the object is close to the target position, set a new random target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }
}
