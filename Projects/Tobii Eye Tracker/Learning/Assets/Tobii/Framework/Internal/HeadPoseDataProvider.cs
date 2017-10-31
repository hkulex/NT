﻿//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN

using Tobii.GameIntegration;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Tobii.Gaming.Internal
{
	/// <summary>
	/// Provider of head pose data. When the provider has been started it
	/// will continuously update the Last property with the latest gaze point 
	/// value received from Tobii Engine.
	/// </summary>
	internal class HeadPoseDataProvider : DataProviderBase<HeadPose>
	{
		/// <summary>
		/// Creates a new instance.
		/// Note: don't create instances of this class directly. Use the <see cref="TobiiHost.GetGazePointDataProvider"/> method instead.
		/// </summary>
		/// <param name="eyeTrackingHost">Eye Tracking Host.</param>
		public HeadPoseDataProvider()
		{
			Last = HeadPose.Invalid;
		}

		protected override void OnStreamingStarted()
		{
			Interop.SubscribeToStream(TobiiSubscription.TobiiSubscriptionHeadTracking);
		}

		protected override void OnStreamingStopped()
		{
			Interop.UnsubscribeFromStream(TobiiSubscription.TobiiSubscriptionHeadTracking);
		}

		internal override string Id
		{
			get { return "HeadPoseDataStream"; }
		}

		internal void Update()
		{
			var headPoses = Interop.GetNewHeadPoses();
			foreach (var headPose in headPoses)
			{
				OnHeadPose(headPose);
			}

			Cleanup();
		}

		private void OnHeadPose(GameIntegration.HeadPose headPose)
		{
			long eyetrackerCurrentUs = headPose.TimeStampMicroSeconds; // TODO awaiting new API from tgi

			float timeStampUnityUnscaled = Time.unscaledTime - ((eyetrackerCurrentUs - headPose.TimeStampMicroSeconds) / 1000000f);
			var rotation = Quaternion.Euler(-headPose.Rotation.Pitch * Mathf.Rad2Deg,
				headPose.Rotation.Yaw * Mathf.Rad2Deg,
				-headPose.Rotation.Roll * Mathf.Rad2Deg);
			Last = new HeadPose(
				new Vector3(headPose.Position.X, headPose.Position.Y, headPose.Position.Z),
				rotation,
				timeStampUnityUnscaled, headPose.TimeStampMicroSeconds);
		}
	}
}
#endif
