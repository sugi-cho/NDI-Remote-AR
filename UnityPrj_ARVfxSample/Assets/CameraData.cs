using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using rAR;
using Klak.Ndi;

public class CameraData : MonoBehaviour
{
    [SerializeField] NdiReceiver ndiReceiver;
    [SerializeField] Camera _camera;
    [SerializeField] Metadata metadata;


    // Update is called once per frame
    void Update()
    {
        var xml = ndiReceiver.metadata;
        if (string.IsNullOrEmpty(xml)) return;
        metadata = Metadata.Deserialize(xml);

        _camera.transform.position = metadata.position;
        _camera.transform.rotation = metadata.rotation;
        _camera.projectionMatrix = metadata.prjMatrix;
    }
}
