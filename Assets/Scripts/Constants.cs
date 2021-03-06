﻿public static class Constants
{
	public static class Input
	{
		public const string AxisName_Mouse_X = "Mouse X";
		public const string AxisName_Mouse_Y = "Mouse Y";
		public const string AxisName_Move_Horizontal = "Horizontal";
		public const string AxisName_Move_Vertical = "Vertical";
	}

	public static class DefaultStatus
	{
		public static class Player
		{
			public static readonly float Move_Speed = 3f;
			public static readonly float Zoom_Move_Speed_Multiplier = 0.6f;
			public static readonly float Jump_Power = 0.06f;
		}
	}

	public static class Physics
	{
		public static readonly float Ground_Check_Distance = 0.5f;
		public static readonly float Gravity_Acceleration = 0.2f;
		public static readonly float Max_Gravity_Acceleration = 0.08f;
		public static readonly float Speed_Multiplier_On_Air = 0.9f;
	}

	public static class GameEnv
	{
		public static readonly float Camera_X_Speed = 3f;
		public static readonly float Camera_Y_Speed = 3f;
		public static readonly float Camera_X_Limit_Min = -85f;
		public static readonly float Camera_X_Limit_Max = 85f;
	}

	public static class Layer
	{
		public const string DEFAULT = "Default";
		public const string MAP = "Map";
		public const string PLAYER = "Player";
		public const string ENEMY = "Enemy";

		public static class Mask
		{
			public static readonly int DEFAULT = UnityEngine.LayerMask.GetMask(Layer.DEFAULT);
			public static readonly int MAP = UnityEngine.LayerMask.GetMask(Layer.MAP);
			public static readonly int PLAYER = UnityEngine.LayerMask.GetMask(Layer.PLAYER);
			public static readonly int ENEMY = UnityEngine.LayerMask.GetMask(Layer.ENEMY);
		}
	}
}
