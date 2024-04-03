using TMPro;
using UnityEngine;

namespace UserInterface
{
    /// <summary>
    /// Controls the UI in the "Game Over" screen.
    /// </summary>
    public class GameOverController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreTextMesh;

        private void Start()
        {
            scoreTextMesh.text = $"You died. Final score: {GameManager.Instance.PlayerScore}";
        }
    } 
}

