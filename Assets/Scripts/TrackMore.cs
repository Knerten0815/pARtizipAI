using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TrackMore : MonoBehaviour
{
    private void Awake()
    {
        VuforiaConfiguration.Instance.Vuforia.MaxSimultaneousImageTargets = 10;
    }
}
