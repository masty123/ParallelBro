using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableFactory : MonoBehaviour
{
    public static InteractableFactory Instance;
    private Dictionary<int, IInteractable> storage;
    private int lastID;

    void Awake()
    {
        if (InteractableFactory.Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Debug.Log("Started");
        Instance = this;
        storage = new Dictionary<int, IInteractable>();
        lastID = 1000;
    }

    public int GetID()
    {
        Debug.Log(lastID);
        return lastID++;
    }

    public IInteractable GetInteractable(int ID)
    {
        if(storage[ID] == null)
        {
            Debug.LogWarning("GetInteractable : " + ID + " could not be found..");
        }
        return storage[ID];
    }
}
