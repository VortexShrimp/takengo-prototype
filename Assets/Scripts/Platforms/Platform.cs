using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace Platforms
{
    /// <summary>
    /// Represents a platform in the game.
    /// </summary>
    public class Platform : MonoBehaviour
    {
        public delegate void PlayerLeftPlatformHandler(Platform platform);
       
        // Called when a platform needs to be destroyed.
        public static event PlayerLeftPlatformHandler OnPlayerLeftPlatform;

        [SerializeField] private Transform connector;
        [SerializeField] private int laneCount;
        [SerializeField] private GameObject[] pickupPrefabs;
        [SerializeField] private GameObject[] obstaclePrefabs;

        // We need this to access the size of the platform.
        private Renderer _floorRenderer;
        private Rigidbody _floorRigidBody;

        // A reference to the pickup which this platform spawned.
        private GameObject _pickup;
        private GameObject _obstacle;

        // Expose the platform connector's position so that the spawner can access it.
        public Vector3 ConnectorPos => connector.position;

        private void Awake()
        {
            var floor = gameObject.transform.GetChild(0).gameObject;
            
            _floorRenderer = floor.GetComponent<Renderer>();
            _floorRigidBody = floor.GetComponent<Rigidbody>();
        }

        private void Start()
        {
            // Size and position of the floor geometry in the world.
            Vector3 floorSize = _floorRenderer.bounds.size;
            Vector3 floorPos = _floorRigidBody.position;
            
            // The width of a single lane.
            float laneWidth = floorSize.x / (laneCount + 1);
            float laneStart = floorPos.x - (floorSize.x / 2f);
            
            int randomLane = Random.Range(1, laneCount + 1);
            float spawnX = laneStart + (laneWidth * randomLane);
            var spawnPos = new Vector3(spawnX, floorPos.y + 2f, floorPos.z);

            int randomPickup = Random.Range(0, pickupPrefabs.Length);
            _pickup = Instantiate(pickupPrefabs[randomPickup], spawnPos, Quaternion.identity);

            if (laneCount >= 2)
            {
                // Generate a new random lane but make sure its
                // not the same as the previous.
                int newRandomLane = Random.Range(1, laneCount + 1);
                while (newRandomLane == randomLane)
                    newRandomLane = Random.Range(1, laneCount + 1);

                spawnX = laneStart + (laneWidth * newRandomLane);
                spawnPos = new Vector3(spawnX, floorPos.y + 2f, floorPos.z);

                randomPickup = Random.Range(0, obstaclePrefabs.Length);
                _obstacle = Instantiate(obstaclePrefabs[randomPickup], spawnPos, Quaternion.identity); 
            }
        }

        private void Update()
        {
            // Move the platform towards the player.
           transform.Translate(new Vector3(0, 0, -(GameManager.Instance.forwardMoveSpeed * Time.deltaTime)));
        }

        private void OnTriggerExit(Collider other)
        {
            // Make sure the player has left the trigger.
            if (other.CompareTag("Player") == false)
                return;
            
            Debug.Log("Player has exited a platform.");
           
            // Fire an event to notify everyone.
            OnPlayerLeftPlatform?.Invoke(this);
            
            // Pickups are only destroyed when the player hits them.
            // By this point, if _pickup is not equal to null, the player missed it.
            if (_pickup != null)
                Destroy(_pickup);
            
            Destroy(_obstacle);
        }
    }
}
