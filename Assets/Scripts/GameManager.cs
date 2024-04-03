using UnityEngine;

/// <summary>
/// A singleton which persists through scene changes.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Singleton instance.
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    public float forwardMoveSpeed;
    public float sidewaysMoveSpeed;
    
    // Player score is present in more than one scene in the game. 
    public int PlayerScore { get; set; }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
        
        // Make sure this singleton doesn't get destroyed.
        DontDestroyOnLoad(gameObject);
    }
}
