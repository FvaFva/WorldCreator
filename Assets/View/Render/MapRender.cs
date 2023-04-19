using System.Collections;
using UnityEngine;

public class MapRender : MonoBehaviour
{
    private BlockViewer[,,] _map;
    private Coroutine _rendering;

    [SerializeField] private int _blocksRowsPreload;
    [SerializeField] private BlockViewer _block;

    public int HarvestedSizeX { get; private set; }
    public int HarvestedSizeY { get; private set; }

    public void RenderMap(BlockPreset[,,] map)
    {
        ClearMap();
        HarvestedSizeX = map.GetLength(0);
        HarvestedSizeY = map.GetLength(1);
        _rendering = StartCoroutine(Render(map));
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
        if(_rendering != null)
            StopCoroutine(_rendering);

        for (int i = 0; i < HarvestedSizeX; i++)
            for (int j = 0; j < HarvestedSizeY; j++)
                for (int k = 0; k < MainSettings.LinkSize; k++)
                    _map[i, j, k].DrowPreset(null);
    }

    private IEnumerator Render(BlockPreset[,,] map)
    {
        WaitForSeconds delay = new WaitForSeconds(MainSettings.OptimizationSecondsDelay);

        for (int i = 0; i < HarvestedSizeX; i++)
        {
            for (int j = 0; j < HarvestedSizeY; j++)
            {
                for (int k = 0; k < MainSettings.LinkSize; k++)
                {
                    BlockPreset currenPreset = map[i, j, k];
                    _map[i, j, k].DrowPreset(currenPreset);

                    if(currenPreset != null)
                        yield return delay;
                    else
                        yield return null;
                }
            }
        }
    }
}
