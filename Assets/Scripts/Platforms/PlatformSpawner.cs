using UnityEngine;
using Random = UnityEngine.Random;

namespace Platforms
{
    /// <summary>
    /// Manages the creation and deletion of new platforms.
    /// </summary>
    public class PlatformSpawner : MonoBehaviour
    {
        [Tooltip("The selection of random platforms to choose from.")]
        [SerializeField] private GameObject[] platformPrefabs;
        
        [Tooltip("The amount of platforms you want to initially spawn.")]
        [SerializeField] private int initialPlatformCount;
        
        // The last spawned platform.
        private Platform _lastPlatform;

        private void OnEnable()
        {
            Platform.OnPlayerLeftPlatform += OnPlayerLeftPlatform;
        }

        private void OnDisable()
        {
            Platform.OnPlayerLeftPlatform -= OnPlayerLeftPlatform;
        }

        private void Start()
        {
            _lastPlatform = null;
            
           // Spawn the initial amount of platforms desired. 
            for (var i = 0; i < initialPlatformCount; ++i)
                SpawnPlatform();
        }

        // Spawns a platform at _lastPlatform's connector.
        private void SpawnPlatform()
        {
            // To randomly select isles to spawn.
            var randomIndex = Random.Range(0, platformPrefabs.Length);

            // Spawn a new platform at the previous connector.
            var newPlatform = Instantiate(platformPrefabs[randomIndex],
                // Last platform will be null at the very beginning.
                _lastPlatform == null ? Vector3.zero : _lastPlatform.ConnectorPos,
                Quaternion.identity);

            // Save the last platform created.
            _lastPlatform = newPlatform.GetComponent<Platform>();
        }

        private void OnPlayerLeftPlatform(Platform platform)
        {
            // Destroy the platform that we just left with a second delay.
            Destroy(platform.gameObject, 2f);
            
            // Spawn a new one at the last platform's connector.
            SpawnPlatform();
        }
    } 
}
