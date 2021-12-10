using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using Klak.Ndi;
using rAR;

public class CameraFrameToTexture : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] ARCameraManager cameraManager;
    [SerializeField] RenderTexture frameTargetTexture;
    [SerializeField] Material mat;
    [SerializeField] NdiSender sender;

    Matrix4x4 m_projection;
    Metadata MakeMetadata() => new Metadata
    {
        position = _camera.transform.position,
        rotation = _camera.transform.rotation,
        prjMatrix = m_projection
    };

    private void OnEnable()
    {
        cameraManager.frameReceived += OnCameraFrameReceived;
    }

    private void OnDisable()
    {
        cameraManager.frameReceived -= OnCameraFrameReceived;
    }

    void OnCameraFrameReceived(ARCameraFrameEventArgs args)
    {
        if (args.textures.Count < 1) return;

        for (var i = 0; i < args.textures.Count; i++)
        {
            var id = args.propertyNameIds[i];
            var tex = args.textures[i];
            mat.SetTexture(id, tex);
        }

        if (args.projectionMatrix.HasValue)
        {
            m_projection = args.projectionMatrix.Value;
            m_projection[1, 1] *= (16f / 9) / _camera.aspect;
        }

        var tex1 = args.textures[0];
        var texAspect = (float)tex1.width / tex1.height;
        var aspectFix = texAspect / (16f / 9);

        mat.SetFloat("_AspectFix", aspectFix);
        Graphics.Blit(null, frameTargetTexture, mat);
    }

    private void OnRenderObject()
    {
        sender.metadata = MakeMetadata().Serialize();
    }
}
