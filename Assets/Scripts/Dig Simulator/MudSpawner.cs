using System.Collections;
using UnityEngine;

namespace Dig_Simulator
{
    public class MudSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject mudPrefab;
        [SerializeField] private Vector3 cubeCenter = Vector3.zero;
        [SerializeField] private float prefabSize = 0.5f;
        [SerializeField] private float prefabHeight = 0.1f;
        [SerializeField] private int cubeSize = 100;
        [SerializeField] private int cubeDepth = 5;
    
        private void Start()
        {
            SpawnMudCube();
        }
    
        private void SpawnMudCube()
        {
            Vector3 startPos = cubeCenter - new Vector3(cubeSize / 2f, cubeSize / 2f, cubeSize / 2f) * prefabSize;
            
            for (int x = 0; x < cubeSize; x++)
            {
                for (int y = 0; y < cubeDepth; y++)
                {
                    for (int z = 0; z < cubeSize; z++)
                    {
                        Vector3 spawnPos = startPos + new Vector3(x * prefabSize, y * prefabHeight, z * prefabSize);
                        Instantiate(mudPrefab, spawnPos, Quaternion.identity, this.transform);
                    }
                }
            }
        }
    }
}