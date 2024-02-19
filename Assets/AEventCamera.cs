using System.Collections;
using UnityEngine;


    public abstract class AEventCamera : MonoBehaviour,ICamera
    {
        private Camera ActiveCamera;
        
        public IEnumerator ActivateCamera(Camera initialcam, Camera secondcam)
        {
            initialcam.enabled = false;
            secondcam.enabled = true;

            ActiveCamera = secondcam;
            
            yield break;
        }

        public IEnumerator DesactivateCamera(Camera initialcam, Camera secondcam)
        {
            initialcam.enabled = false;
            secondcam.enabled = true;
            
            ActiveCamera = secondcam;
            
            yield break;
        }

        public IEnumerator UpdateCamera(Camera activeCam)
        {
            throw new System.NotImplementedException();
        }
    }
