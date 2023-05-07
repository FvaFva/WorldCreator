using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class BlockViewer : MonoBehaviour
{
    [SerializeField] private int _flightAltitude;
    [SerializeField] private int _fallSpeed;

    private MeshRenderer _render;
    private BlockPreset _preset;
    private Vector3 _realPosition;
    private Vector3 _flyPosition;
    private Coroutine _faller;

    public void DrawPreset(BlockPreset preset)
    {
        if(preset == null)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        _preset = preset;
        _render.material = _preset.Skin;
        StartVisualization();
    }

    private void Awake()
    {
        gameObject.SetActive(false);
        TryGetComponent<MeshRenderer>(out _render);
        _realPosition = transform.position;
        _flyPosition = new Vector3(_realPosition.x, _realPosition.y + _flightAltitude, _realPosition.z);
    }

    private void StartVisualization()
    {
        StopVisualization();
        transform.position = _flyPosition;
        _faller = StartCoroutine(FallDown());
    }

    private void StopVisualization()
    {
        if(_faller != null)
            StopCoroutine(_faller);

        transform.position = _realPosition;
    }

    private IEnumerator FallDown()
    {
        WaitForSeconds delay = new WaitForSeconds(0.01f);

        while (transform.position.y > _realPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, _realPosition, _fallSpeed * Time.deltaTime);
            yield return delay;
        }
    }
}
