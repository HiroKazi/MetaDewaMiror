using System.Collections;
using UnityEngine;

namespace MetaverseIndonesia.Utility
{
	public class PanelHMDFollower : MonoBehaviour
	{
		private const float TOTAL_DURATION = 2.0f;
		
		[SerializeField] private float _maxDistance = 0.3f;
		[SerializeField] private float _minDistance = 0.05f;

		[SerializeField] private OVRCameraRig _cameraRig;
		
		private Coroutine _coroutine = null;
		private Vector3 _prevPos = Vector3.zero;
		
		private void Update()
		{
			if(_cameraRig != null)
			{
				var centerEyeAnchorPos = _cameraRig.centerEyeAnchor.position;
				var panelPos = transform.position;
				
				float distanceSqr = Vector3.SqrMagnitude(centerEyeAnchorPos - panelPos);
				
				if (((distanceSqr > _maxDistance) || (_minDistance > distanceSqr)) && _coroutine == null)
				{
					if (_coroutine == null)
						_coroutine = StartCoroutine(LerpToHMD());
				}

			}
			else
			{
				Debug.LogError("No Camera OVRRig");
			}
		}

		private IEnumerator LerpToHMD()
		{
			Vector3 cameraForward = _cameraRig.centerEyeAnchor.forward;
			cameraForward.y = 0;

			Vector3 panelOffsett = cameraForward.normalized * 0.5f;
			panelOffsett.y -= 0.25f;

			Vector3 newPanelPosition = _cameraRig.centerEyeAnchor.position + panelOffsett;
			Quaternion newPanelRotation = Quaternion.LookRotation(newPanelPosition - _cameraRig.centerEyeAnchor.position);
			
			float startTime = Time.time;
			float endTime = Time.time + TOTAL_DURATION;

			while (Time.time < endTime)
			{
				transform.position =
				  Vector3.Slerp(transform.position, newPanelPosition, (Time.time - startTime) / TOTAL_DURATION);

				transform.rotation = 
				  Quaternion.Slerp(transform.rotation, newPanelRotation, (Time.time - startTime) / TOTAL_DURATION);
				yield return null;
			}

			transform.position = newPanelPosition;
			_coroutine = null;
		}
	}
}
