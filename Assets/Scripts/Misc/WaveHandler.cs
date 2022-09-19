using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHandler : MonoBehaviour
{
    [SerializeField] private float _disableTime = 0.5f;

    private Transform _player;
    private Material _mat;
    private MeshRenderer _renderer;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>().transform;
        _renderer = GetComponent<MeshRenderer>();
        _mat = _renderer.material;
    }

    private void OnEnable()
    {
        if (_mat != null)
        {
            _mat.SetVector("_FocalPoint", new Vector4(Remap(_player.position.x, -18, 18, 0, 1), Remap(_player.position.y, -10, 10, 0, 1), 0f, 0f));
            Invoke("DisableWave", _disableTime);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void DisableWave()
    {
        gameObject.SetActive(false);
    }

    public float Remap(float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        var fromAbs = from - fromMin;
        var fromMaxAbs = fromMax - fromMin;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + toMin;

        return to;
    }

}
