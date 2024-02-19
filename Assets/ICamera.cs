using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ICamera
{
  
    IEnumerator ActivateCamera(Camera initialcam,Camera secondcam);
    IEnumerator DesactivateCamera(Camera initialcam, Camera secondcam);
    IEnumerator UpdateCamera(Camera activeCam);
}