using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BlurScript : MonoBehaviour
{
    public void UpdateBlur()
    {
        PostProcessVolume _ppVolume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
        _ppVolume.enabled = !_ppVolume.enabled;
    }
}
