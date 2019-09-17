using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public void Start()
    {
        // Get all interactable components
        GameObject[] interactables = GameObject.FindGameObjectsWithTag("interactable");
        // sort by position
        interactables = interactables.OrderBy(item => Vector3.Distance(transform.position, item.transform.position)).ToArray();
        // assign id to tag
        for (int i = 0; i < interactables.Length; i++)
        {
            int ID = GetID(interactables[i].GetComponent<IInteractable>());
            interactables[i].GetComponent<IInteractable>().ID = ID;
            VDebug.Instance.Log(i + " = " + interactables[i].gameObject.name + " : " + ID);
        }
    }

    public int GetID(IInteractable interactable)
    {
        int id = lastID++;
        storage.Add(id, interactable);
        return id;
    }

    public IInteractable GetInteractable(int ID)
    {
        if (storage[ID] == null)
        {
            Debug.LogWarning("GetInteractable : " + ID + " could not be found..");
        }
        return storage[ID];
    }
}
