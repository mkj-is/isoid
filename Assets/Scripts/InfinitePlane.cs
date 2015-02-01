using UnityEngine;
using System.Collections;

public class InfinitePlane : MonoBehaviour {
    public GameObject PlayerObject;
    public GameObject PrefabPlane;

    private GameObject[,] _planeGrid = new GameObject[3, 3];
    private float scale;

    void Start()
    {
        scale = gameObject.transform.localScale.x;

        _planeGrid[0, 0] = (GameObject)Instantiate(PrefabPlane);
        _planeGrid[0, 1] = (GameObject)Instantiate(PrefabPlane);
        _planeGrid[0, 2] = (GameObject)Instantiate(PrefabPlane);
        _planeGrid[1, 0] = (GameObject)Instantiate(PrefabPlane);
        _planeGrid[1, 1] = this.gameObject;
        _planeGrid[1, 2] = (GameObject)Instantiate(PrefabPlane);
        _planeGrid[2, 0] = (GameObject)Instantiate(PrefabPlane);
        _planeGrid[2, 1] = (GameObject)Instantiate(PrefabPlane);
        _planeGrid[2, 2] = (GameObject)Instantiate(PrefabPlane);

        UpdatePlanePositions();
    }

    private void UpdatePlanePositions()
    {
        _planeGrid[0, 0].transform.position = new Vector3(_planeGrid[1, 1].transform.position.x - 10 * scale, _planeGrid[1, 1].transform.position.y, _planeGrid[1, 1].transform.position.z + 10 * scale);
        _planeGrid[0, 1].transform.position = new Vector3(_planeGrid[1, 1].transform.position.x - 10 * scale, _planeGrid[1, 1].transform.position.y, _planeGrid[1, 1].transform.position.z);
        _planeGrid[0, 2].transform.position = new Vector3(_planeGrid[1, 1].transform.position.x - 10 * scale, _planeGrid[1, 1].transform.position.y, _planeGrid[1, 1].transform.position.z - 10 * scale);

        _planeGrid[1, 0].transform.position = new Vector3(_planeGrid[1, 1].transform.position.x, _planeGrid[1, 1].transform.position.y, _planeGrid[1, 1].transform.position.z + 10 * scale);
        _planeGrid[1, 2].transform.position = new Vector3(_planeGrid[1, 1].transform.position.x, _planeGrid[1, 1].transform.position.y, _planeGrid[1, 1].transform.position.z - 10 * scale);

        _planeGrid[2, 0].transform.position = new Vector3(_planeGrid[1, 1].transform.position.x + 10 * scale, _planeGrid[1, 1].transform.position.y, _planeGrid[1, 1].transform.position.z + 10 * scale);
        _planeGrid[2, 1].transform.position = new Vector3(_planeGrid[1, 1].transform.position.x + 10 * scale, _planeGrid[1, 1].transform.position.y, _planeGrid[1, 1].transform.position.z);
        _planeGrid[2, 2].transform.position = new Vector3(_planeGrid[1, 1].transform.position.x + 10 * scale, _planeGrid[1, 1].transform.position.y, _planeGrid[1, 1].transform.position.z - 10 * scale);
    }

    void Update()
    {
        Vector3 playerPosition = new Vector3(PlayerObject.transform.position.x, PlayerObject.transform.position.y, PlayerObject.transform.position.z);
        GameObject playerPlane = null;

        int xOffset = 0;
        int yOffset = 0;
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if ((playerPosition.x >= _planeGrid[x, y].transform.position.x - ((10 * scale) / 2)) &&
                    (playerPosition.x <= (_planeGrid[x, y].transform.position.x + ((10 * scale) / 2))) &&
                    (playerPosition.z >= _planeGrid[x, y].transform.position.z - ((10 * scale) / 2)) &&
                    (playerPosition.z <= (_planeGrid[x, y].transform.position.z + ((10 * scale) / 2))))
                {
                    playerPlane = _planeGrid[x, y];
                    xOffset = 1 - x;
                    yOffset = 1 - y;
                    break;
                }
            }
            if (playerPlane != null)
                break;
        }

        if (playerPlane != _planeGrid[1, 1])
        {
            GameObject[,] newPlaneGrid = new GameObject[3, 3];

            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                {
                    int newX = x + xOffset;
                    if (newX < 0)
                        newX = 2;
                    else if (newX > 2)
                        newX = 0;
                    int newY = y + yOffset;
                    if (newY < 0)
                        newY = 2;
                    else if (newY > 2)
                        newY = 0;
                    newPlaneGrid[newX, newY] = _planeGrid[x, y];
                }

            _planeGrid = newPlaneGrid;
            UpdatePlanePositions();
        }
    }
}
