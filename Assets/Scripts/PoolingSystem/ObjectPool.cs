using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    // Prefab to be pooled
    private readonly T prefab;

    // Queue to store the pooled objects
    private readonly Queue<T> objects = new Queue<T>();

    // Optional parent transform to organize pooled objects
    private readonly Transform parent;

    // Constructor to initialize the object pool with a prefab and initial size
    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;

        // Instantiate the initial set of objects and add them to the pool
        for (int i = 0; i < initialSize; i++)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.SetActive(false);
            objects.Enqueue(obj);
        }
    }

    // Method to get an object from the pool
    public T Get()
    {
        // If the pool is empty, expand it by creating one additional object
        if (objects.Count == 0)
        {
            ExpandPool(1);
        }

        // Dequeue an object from the pool, activate it, and return it
        T obj = objects.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }

    // Method to release an object back into the pool
    public void Release(T obj)
    {
        // Deactivate the object and enqueue it back into the pool
        obj.gameObject.SetActive(false);
        objects.Enqueue(obj);
    }

    // Method to expand the pool by creating additional objects
    public void ExpandPool(int amount)
    {
        // Instantiate the specified number of objects and add them to the pool
        for (int i = 0; i < amount; i++)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.SetActive(false);
            objects.Enqueue(obj);
        }
    }
}
