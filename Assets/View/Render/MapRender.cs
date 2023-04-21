using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRender : MonoBehaviour
{
    private BlockViewer[,,] _map;
    private List<Coroutine> _rendering = new List<Coroutine>();

    [SerializeField] private int _blocksRowsPreload;
    [SerializeField] private BlockViewer _block;

    public int HarvestedSizeX { get; private set; }
    public int HarvestedSizeY { get; private set; }

    public void RenderMap(BlockPreset[,,] map)
    {
        ClearMap();
        HarvestedSizeX = map.GetLength(0);
        HarvestedSizeY = map.GetLength(1);

        for (int i = 0; i < HarvestedSizeX; i++)
            _rendering.Add(StartCoroutine(Render(map, i)));
    }

    private void Awake()
    {
        if(_block == null || _blocksRowsPreload == 0)
            gameObject.SetActive(false);

        _map = new BlockViewer[_blocksRowsPreload, _blocksRowsPreload, MainSettings.LinkSize];

        PreloadMap();
    }

    private void PreloadMap()
    {
        for (int i = 0; i < _blocksRowsPreload; i++)        
            for (int j = 0; j < _blocksRowsPreload; j++)
                for (int k = 0; k < MainSettings.LinkSize; k++)
                    _map[i, j, k] = Instantiate(_block, new Vector3(i, k, j),new Quaternion(), transform);                    
    }

    private void ClearMap()
    {
        foreach (Coroutine coroutine in _rendering)
            if (coroutine != null)
                StopCoroutine(coroutine);

        _rendering.Clear();

        for (int i = 0; i < HarvestedSizeX; i++)
            for (int j = 0; j < HarvestedSizeY; j++)
                for (int k = 0; k < MainSettings.LinkSize; k++)
                    _map[i, j, k].DrowPreset(null);
    }

    private IEnumerator Render(BlockPreset[,,] map, int x)
    {
        WaitForSeconds delay = new WaitForSeconds(MainSettings.OptimizationSecondsDelay);

        //RandomizDirection(out int xStart, out int xFinish, out int xSpeed, 0, HarvestedSizeX);
        RandomizDirection(out int yStart, out int yFinish, out int ySpeed, 0, HarvestedSizeY);
        
        for (int z = 0; z < MainSettings.LinkSize; z++)
        {
            for (int y = yStart; y != yFinish; y += ySpeed)
            {
                BlockPreset currenPreset = map[x, y, z];
                _map[x, y, z].DrowPreset(currenPreset);

                if (currenPreset != null)
                    yield return delay;
                else
                    yield return null;
            }
        }
    }

    private void RandomizDirection(out int start, out int finish, out int speed, int min, int max)
    {
        if(Random.Range(0,2) == 1)
        {
            start = min;
            finish = max;
            speed = 1;
        }
        else
        {
            start = max - 1;
            finish = min - 1;
            speed = -1;
        }
    }
}