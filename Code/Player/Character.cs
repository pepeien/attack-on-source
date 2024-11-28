using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Player
{
	public sealed class Character : Component
	{
		[Property]
		public PlayerController Controller { get; set; }

		private float FieldOfView = -1.0f;

		private const float ZOOM_COEFFICIENT = 0.35f;
		private const float ZOOMED_IN_FOV = 45.0f;
		private const float ZOOMED_OUT_FOV = 79.0f;

		protected override void OnPreRender()
		{
			base.OnPreRender();

			CameraComponent camera = Controller.Scene.Camera;

			if (Input.Down("attack2"))
			{
				ZoomCameraIn(camera);

				return;
			}

			ZoomCameraOut(camera);
		}

		private void ZoomCameraIn(CameraComponent camera)
		{
			if (camera == null)
			{
				return;
			}

			if (this.FieldOfView < 0.0f)
			{
				this.FieldOfView = camera.FieldOfView;
			}

			if (this.FieldOfView > ZOOMED_IN_FOV)
			{
				this.FieldOfView -= ZOOM_COEFFICIENT;
			}
			
			camera.FieldOfView = this.FieldOfView;
			camera.LocalTransform = camera.LocalTransform.Add(Vector3.Left * -2.0f, false);
		}

		private void ZoomCameraOut(CameraComponent camera)
		{
			if (this.FieldOfView < 0.0f)
			{
				this.FieldOfView = camera.FieldOfView;
			}

			if (this.FieldOfView < ZOOMED_OUT_FOV)
			{
				this.FieldOfView += ZOOM_COEFFICIENT;
			}

			camera.FieldOfView = this.FieldOfView;
			camera.LocalTransform = camera.LocalTransform.Add(Vector3.Left * 2.0f, false);
		}
	}
}
