using System.Collections;
using System.Collections.Generic;
using System.IO;
using NatSuite.Recorders;
using NatSuite.Recorders.Clocks;
using NatSuite.Recorders.Inputs;
using UnityEngine;

public class CameraRecorder : MonoBehaviour
{
  async  void Start()
    {
        var recorder = new MP4Recorder(1280, 720, 30);
        var clock = new RealtimeClock();
        var cameraInput = new CameraInput(recorder, clock, Camera.main);
        StartCoroutine(delay());
        cameraInput.Dispose();
        var path = await recorder.FinishWriting();
        print(path);
        if(!Directory.Exists(path))
            Directory.CreateDirectory(path);

    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(5);
    }


}
