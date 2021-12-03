using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Antilatency.DeviceNetwork;
using Antilatency.Integration;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionConfigurator : MonoBehaviour
{
    public DeviceNetwork Network;

    public List<AltTrackingDirect> trackedAlts;

    public Button startButton;
    public Button stopButton;

    private void Start()
    {
        //UpdateTrackingConfiguration();
        foreach (var exporter in GameObject.FindObjectsOfType(typeof(Exporter)) as Exporter[])
        {
            Debug.Log(exporter.gameObject.name);
            startButton.onClick.AddListener(() => (exporter.gameObject)?.GetComponent<Exporter>().StartWriting());
            stopButton.onClick.AddListener(() => (exporter.gameObject)?.GetComponent<Exporter>().StopWriting());
        }
    }

    protected INetwork GetNativeNetwork() {
        if (Network == null) {
            Debug.LogError("Network is null");
            return null;
        }

        if (Network.NativeNetwork == null) {
            Debug.LogError("Native network is null");
            return null;
        }

        return Network.NativeNetwork;
    }
    
    protected Antilatency.Alt.Tracking.ILibrary _trackingLibrary;
    
    public void UpdateTrackingConfiguration()
    {
        var nativeNetwork = GetNativeNetwork();
        var nodes = new NodeHandle[]{};
        if (nativeNetwork == null)
        {
            nodes = new NodeHandle[0];
        }
        _trackingLibrary = Antilatency.Alt.Tracking.Library.load();
        using (var cotaskConstructor = _trackingLibrary.createTrackingCotaskConstructor()) {
            nodes = cotaskConstructor.findSupportedNodes(nativeNetwork).Where(v =>
                nativeNetwork.nodeGetStatus(v) == NodeStatus.Idle
            ).ToArray();
        }
    }
}
