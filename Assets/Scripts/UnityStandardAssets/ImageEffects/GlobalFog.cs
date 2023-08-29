using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Rendering/Global Fog")]
	internal class GlobalFog : PostEffectsBase
	{
		[Tooltip("Apply distance-based fog?")]
		public bool distanceFog = true;

		[Tooltip("Distance fog is based on radial distance from camera when checked")]
		public bool useRadialDistance;

		[Tooltip("Apply height-based fog?")]
		public bool heightFog = true;

		[Tooltip("Fog top Y coordinate")]
		public float height = 1f;

		[Range(0.001f, 10f)]
		public float heightDensity = 2f;

		[Tooltip("Push fog away from the camera by this amount")]
		public float startDistance;

		public Shader fogShader;

		private Material fogMaterial;

		public override bool CheckResources()
		{
			CheckSupport(needDepth: true);
			fogMaterial = CheckShaderAndCreateMaterial(fogShader, fogMaterial);
			if (!isSupported)
			{
				ReportAutoDisable();
			}
			return isSupported;
		}

		[ImageEffectOpaque]
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (!CheckResources() || (!distanceFog && !heightFog))
			{
				Graphics.Blit(source, destination);
				return;
			}
			Camera component = GetComponent<Camera>();
			Transform transform = component.transform;
			float nearClipPlane = component.nearClipPlane;
			float farClipPlane = component.farClipPlane;
			float fieldOfView = component.fieldOfView;
			float aspect = component.aspect;
			Matrix4x4 identity = Matrix4x4.identity;
			float num = fieldOfView * 0.5f;
			Vector3 b = transform.right * nearClipPlane * Mathf.Tan(num * ((float)Math.PI / 180f)) * aspect;
			Vector3 b2 = transform.up * nearClipPlane * Mathf.Tan(num * ((float)Math.PI / 180f));
			Vector3 a = transform.forward * nearClipPlane - b + b2;
			float d = a.magnitude * farClipPlane / nearClipPlane;
			a.Normalize();
			a *= d;
			Vector3 a2 = transform.forward * nearClipPlane + b + b2;
			a2.Normalize();
			a2 *= d;
			Vector3 a3 = transform.forward * nearClipPlane + b - b2;
			a3.Normalize();
			a3 *= d;
			Vector3 a4 = transform.forward * nearClipPlane - b - b2;
			a4.Normalize();
			a4 *= d;
			identity.SetRow(0, a);
			identity.SetRow(1, a2);
			identity.SetRow(2, a3);
			identity.SetRow(3, a4);
			Vector3 position = transform.position;
			float num2 = position.y - height;
			float z = (!(num2 <= 0f)) ? 0f : 1f;
			fogMaterial.SetMatrix("_FrustumCornersWS", identity);
			fogMaterial.SetVector("_CameraWS", position);
			fogMaterial.SetVector("_HeightParams", new Vector4(height, num2, z, heightDensity * 0.5f));
			fogMaterial.SetVector("_DistanceParams", new Vector4(0f - Mathf.Max(startDistance, 0f), 0f, 0f, 0f));
			FogMode fogMode = RenderSettings.fogMode;
			float fogDensity = RenderSettings.fogDensity;
			float fogStartDistance = RenderSettings.fogStartDistance;
			float fogEndDistance = RenderSettings.fogEndDistance;
			bool flag = fogMode == FogMode.Linear;
			float num3 = (!flag) ? 0f : (fogEndDistance - fogStartDistance);
			float num4 = (!(Mathf.Abs(num3) > 0.0001f)) ? 0f : (1f / num3);
			Vector4 vector = default(Vector4);
			vector.x = fogDensity * 1.2011224f;
			vector.y = fogDensity * 1.442695f;
			vector.z = ((!flag) ? 0f : (0f - num4));
			vector.w = ((!flag) ? 0f : (fogEndDistance * num4));
			fogMaterial.SetVector("_SceneFogParams", vector);
			fogMaterial.SetVector("_SceneFogMode", new Vector4((float)fogMode, useRadialDistance ? 1 : 0, 0f, 0f));
			int num5 = 0;
			CustomGraphicsBlit(passNr: (!distanceFog || !heightFog) ? (distanceFog ? 1 : 2) : 0, source: source, dest: destination, fxMaterial: fogMaterial);
		}

		private static void CustomGraphicsBlit(RenderTexture source, RenderTexture dest, Material fxMaterial, int passNr)
		{
			RenderTexture.active = dest;
			fxMaterial.SetTexture("_MainTex", source);
			GL.PushMatrix();
			GL.LoadOrtho();
			fxMaterial.SetPass(passNr);
			GL.Begin(7);
			GL.MultiTexCoord2(0, 0f, 0f);
			GL.Vertex3(0f, 0f, 3f);
			GL.MultiTexCoord2(0, 1f, 0f);
			GL.Vertex3(1f, 0f, 2f);
			GL.MultiTexCoord2(0, 1f, 1f);
			GL.Vertex3(1f, 1f, 1f);
			GL.MultiTexCoord2(0, 0f, 1f);
			GL.Vertex3(0f, 1f, 0f);
			GL.End();
			GL.PopMatrix();
		}
	}
}
