using System.Collections.Generic;
using UnityEngine;

public class BarrierManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _barrierPrefab;
    [SerializeField]
    private float _xOffset;
    [SerializeField]
    private float _zOffset;

    private Grid _playerGrid;
    private EnemyGrid _enemyGrid;
    private List<GameObject> _playerBarriers = new List<GameObject>();
    private List<GameObject> _enemyBarriers = new List<GameObject>();

    private void Start()
    {
        var playerGridObj = GameObject.FindWithTag("PlayerGrid");
        if (playerGridObj == null)
            throw new System.Exception("Player grid not found");

        var enemyGridObj = GameObject.FindWithTag("EnemyGrid");
        if (enemyGridObj == null)
            throw new System.Exception("Enemy grid not found");

        _playerGrid = playerGridObj.GetComponent<Grid>();
        _enemyGrid = enemyGridObj.GetComponent<EnemyGrid>();

        SetupBarriers(2, 3);
    }

    public void SetupBarriers(int playerBarrierCount, int enemyBarrierCount)
    {
        SetupBarriers(playerBarrierCount, _zOffset * -1, _playerGrid, ref _playerBarriers);
        SetupBarriers(enemyBarrierCount, _zOffset, _enemyGrid, ref _enemyBarriers);
    }

    public void SetupBarriers(int count, float zOffset, Grid grid, ref List<GameObject> barriers)
    {
        barriers.Clear();
        var possibleCoords = GetPossibleCellCoords();

        for (int i = 0; i < count; i++)
        {
            var index = Random.Range(0, possibleCoords.Count);
            var coords = possibleCoords[index];
            var cell = grid.GetCellByCoords(coords);

            if (cell == null)
            {
                Debug.LogError("Cell could not be found at " + coords);
                continue;
            }
            var barrier = CreateBarrier(cell.transform.position, zOffset);
            barriers.Add(barrier);
            possibleCoords.RemoveAt(index);
        }
    }

    private GameObject CreateBarrier(Vector3 cellPos, float zOffset)
    {
        var barrier = Instantiate(_barrierPrefab);
        barrier.transform.position = new Vector3(cellPos.x, cellPos.y, cellPos.z + zOffset);
        return barrier;
    }

    private List<(int, int)> GetPossibleCellCoords()
    {
        return new List<(int, int)>
        {
            (0,0), (0,1), (0,2), (1,0), (1,1), (1,2), (2,0), (2,1), (2,2)
        };
    }
}
