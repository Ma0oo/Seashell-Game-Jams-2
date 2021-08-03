using Plugins.HabObject.DIContainer;
using Services.Cameras;
using UnityEngine;

namespace Factorys
{
    public class CameraServicesFactory : MonoBehaviour
    {
        [SerializeField] private CameraFollowPlayer _cameraTemplate;
        public void Create()
        {
            Destroy(Camera.main.gameObject);
            DiServices.MainContainer.CreatePrefab(_cameraTemplate);
        }
    }
}