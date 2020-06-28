using Cinemachine;
using UnityEngine;

namespace BustaGames.Climber
{
    [ExecuteInEditMode]
    [SaveDuringPlay]
    [AddComponentMenu("")] // Hide in menu
    public class CinemachineLockCameraX : CinemachineExtension
    {
        [Tooltip("Lock the camera's X position to this value")]
        public float xPosition;

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (enabled && stage == CinemachineCore.Stage.Body)
            {
                var pos = state.RawPosition;
                pos.x = xPosition;
                state.RawPosition = pos;
            }
        }
    }
}