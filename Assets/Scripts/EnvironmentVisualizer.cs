using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Antilatency.Integration;
using UnityEngine;

public class EnvironmentVisualizer : AltEnvironmentMarkersDrawer
{
    protected new void Start() 
    {
        base.Start();
        _markerMaterial.color = Color.green;
        StartCoroutine(DelayedAction(() =>
        {
            foreach (var markerTransform in from marker in base._markers
                where marker.transform.position.y > 0.01
                select marker.transform)
            {
                markerTransform.Rotate(new Vector3(0,0,1), 180f);
            }
        }, .1f));
    }

    private IEnumerator DelayedAction(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
}
