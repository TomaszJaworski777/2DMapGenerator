using UnityEngine;

namespace Dungeons.Map
{
    public class MapGenerator : MonoBehaviour
    {
        private FastNoise noise;

        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private Vector2Int mapSize;
        [SerializeField] private float tileSize;
        [SerializeField] private float scale = 3f;
        [SerializeField] private float functionScale = 0.2f;
        [SerializeField] private FastNoise.NoiseType type;

        private void GenerateMap(Vector2Int mapSize)
        {
            InitalizeNoise();
            ClearObject();

            for (int x = 0; x < mapSize.x; x++)
            {
                for (int y = 0; y < mapSize.y; y++)
                {
                    SpawnTileFromNoiseValue(x, y);
                }
            }     
        }

        private FastNoise InitalizeNoise()
        {
            noise = new FastNoise(Random.Range(-77777, 77777));
            noise.SetNoiseType(type);
        }

        private void ClearObject()
        {
            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
            }
        }

        private void SpawnTileFromNoiseValue(int x, int y)
        {
            float tileValue = GetNoiseValue();

            if (tileValue > 0.5f)
                SpawnTile(x, y);
        }

        private float GetNoiseValue()
        {
            return noise.GetNoise(x * scale, y * scale) - GradientFunction(y) + 1f;
        }

        private float GradientFunction(float y)
        {
            return y * functionScale;
        }

        private void SpawnTile(int x, int y)
        {
            Instantiate(tilePrefab, (Vector2)transform.position + new Vector2(x, y) * tileSize, Quaternion.identity, transform);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube((Vector2)transform.position + (mapSize/2), (Vector2)mapSize * tileSize);
        }
    }
}
