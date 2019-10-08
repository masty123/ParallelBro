using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    private static UserData Instance;

    [SerializeField]
    private string username = "test";
    [SerializeField]
    private string userId = "101";
    // image
    [SerializeField]
    private int characterIndex = 0;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public static UserData GetInstance()
    {
        if (Instance == null)
        {
            GameObject GO = new GameObject();
            GO.name = "UserData";
            GO.AddComponent(typeof(UserData));
            UserData.Instance = GO.GetComponent<UserData>();
        }
        return UserData.Instance;
    }

    public void SetUsername(string username)
    {
        this.username = username;
    }

    public string GetUsername()
    {
        return username;
    }

    public void SetUserId(string userId)
    {
        this.userId = userId;
    }

    public string GetUserId()
    {
        return userId;
    }

    public void SetCharacterIndex(int characterIndex)
    {
        this.characterIndex = characterIndex;
    }

    public int GetCharacterIndex()
    {
        return characterIndex;
    }
}
