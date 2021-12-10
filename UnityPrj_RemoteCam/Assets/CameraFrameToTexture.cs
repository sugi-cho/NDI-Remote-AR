using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;

public class CameraFrameToTexture : MonoBehaviour
{
    [SerializeField] ARCameraManager cameraManager;
    [SerializeField] RenderTexture frameTargetTexture;

    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        cameraManager.frameReceived -= OnCameraFrameReceived;
    }

    void OnCameraFrameReceived(ARCameraFrameEventArgs args)
    {
        if (args.textures.Count < 1) return;

        for(var i = 0; i < args.textures.Count; i++)
        {
            var t = args.textures[i];
            Debug.Log($"{t.name}: {t.width}x{t.height}");
        }

        var tex = args.textures[0];
        Graphics.Blit(tex, frameTargetTexture);
    }
}
