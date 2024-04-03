using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Platforms;

namespace UserInterface
{
    /// <summary>
    /// Controls the UI in the "Main Game" scene.
    /// </summary>
    public class MainGameController : MonoBehaviour
    {
        [Tooltip("The TextMesh object you want to hold the score.")]
        [SerializeField] private TextMeshProUGUI scoreTextMesh;
    
        private void OnEnable()
        {
            Pickup.OnPlayerEnteredPickup += OnPlayerEnteredPickup;
            ObstacleNormalCollider.OnPlayerEnteredCollider += OnPlayerEnteredObstacleNormalCollider;
            ObstacleExtendedCollider.OnPlayerEnteredCollider += OnPlayerEnteredObstacleExtendedCollider;
        }

        private void OnDisable()
        {
            Pickup.OnPlayerEnteredPickup -= OnPlayerEnteredPickup;
            ObstacleNormalCollider.OnPlayerEnteredCollider -= OnPlayerEnteredObstacleNormalCollider;
            ObstacleExtendedCollider.OnPlayerEnteredCollider -= OnPlayerEnteredObstacleExtendedCollider;
        }

        private void Start()
        {
            // Initialize score to zero.
            GameManager.Instance.PlayerScore = 0;
            SetScoreText(GameManager.Instance.PlayerScore);
        }

        private void OnPlayerEnteredObstacleNormalCollider()
        {
            // Player hit an obstacle, show them the game over scene.
            SceneManager.LoadSceneAsync("Game Over", LoadSceneMode.Single);
        }

        private void OnPlayerEnteredObstacleExtendedCollider()
        {
            // Player performed a near miss.
            GameManager.Instance.PlayerScore += 10;
            SetScoreText(GameManager.Instance.PlayerScore);
        }
    
        private void OnPlayerEnteredPickup()
        {
            // Add some score for pickups.
            GameManager.Instance.PlayerScore += 5;
            SetScoreText(GameManager.Instance.PlayerScore);
        }

        // Helper function for setting the player's score.
        private void SetScoreText(int score)
        {
            scoreTextMesh.text = $"Score: {score}"; 
        }
    }
 
}
