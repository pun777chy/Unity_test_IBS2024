using UnityEngine; 
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class PrefabSpawner : MonoBehaviour
{
    // The prefab to be spawned
    public Transform prefab;

    // Initial amount of objects to be spawned
    public int initialAmount = 10;

    // Parent transform to hold spawned objects
    public Transform parent;

    // UI Text to display the count of spawned objects
    public TextMeshProUGUI spawnCountText;

    // Input field to get the number of objects to spawn or despawn
    public TMP_InputField inputField;

    // Key to trigger spawning
    public KeyCode spawnKey = KeyCode.S;

    // Key to trigger despawning
    public KeyCode despawnKey = KeyCode.D;

    // The zone within which objects will be spawned
    public Vector3 spawnZone = new Vector3(10, 10, 10);

    // The object pool for managing prefabs
    private ObjectPool<Transform> pool;

    // Called when the script instance is being loaded
    private void Start()
    {
        // Initialize the object pool with the specified prefab and initial amount
        pool = new ObjectPool<Transform>(prefab, initialAmount, parent);

        // Update the UI to show the initial spawn count
        UpdateUI();
    }

    // Called once per frame
    private void Update()
    {
        // Check if the spawn key is pressed
        if (Input.GetKeyDown(spawnKey))
        {
            // Parse the input field to get the number of objects to spawn
            int amount = int.Parse(inputField.text);
            if (amount <= 0 )
            {
                Debug.LogWarning("Please enter a postive integer in the inputfield");
            }
            // Spawn the specified number of objects
            for (int i = 0; i < amount; i++)
            {
                Transform obj = pool.Get();
                obj.position = GetRandomPositionInZone();
            }

            // Update the UI to reflect the new spawn count
            UpdateUI();
        }

        // Check if the despawn key is pressed
        if (Input.GetKeyDown(despawnKey))
        {
            // Parse the input field to get the number of objects to despawn
            int amount = int.Parse(inputField.text);

            // Despawn the specified number of objects
            List<Transform> children = GetActiveObjects();
            for (int i = 0; i < amount; i++)
            {
                if (children.Count > 0 && children.Count >= amount)
                {
                    Transform obj = children[i];
                    pool.Release(obj);
                }
                else
                {
                    if (children.Count < amount)
                    {
                        Debug.LogWarning("Please make sure the entered value is equal or less then the total number of active objects");
                    }

                }
            }

            // Update the UI to reflect the new spawn count
            UpdateUI();
        }
    }

    // Returns a random position within the spawn zone
    private Vector3 GetRandomPositionInZone()
    {
        return new Vector3(
            Random.Range(-spawnZone.x / 2, spawnZone.x / 2),
            Random.Range(-spawnZone.y / 2, spawnZone.y / 2),
            Random.Range(-spawnZone.z / 2, spawnZone.z / 2)
        );
    }

    // Updates the UI to show the current count of spawned objects
    private void UpdateUI()
    {
        spawnCountText.text = "Spawned: " + GetActiveObjects().Count;
    }
    private List<Transform> GetActiveObjects()
    {
        List<Transform> children = parent.GetComponentsInChildren<Transform>(false).ToList<Transform>();
        children.RemoveAt(0); // remove the parent Object from the List because GetComponentsInChildren includes the parent
        return children;
    }
}
