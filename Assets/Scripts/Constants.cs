public static class Constants
{
	public static class Input
	{
		public const string AxisName_Mouse_X = "Mouse X";
		public const string AxisName_Mouse_Y = "Mouse Y";
		public const string AxisName_Move_Horizontal = "Horizontal";
		public const string AxisName_Move_Vertical = "Vertical";
	}

	public static class Physics
	{
		public static readonly float GroundCheckDistance = 0.9f;
		public static readonly float GravityAcceleration = 0.26f;
	}

	public static class Layer
	{
		public static readonly int DEFAULT = 0;
		public static readonly int MAP = UnityEngine.LayerMask.NameToLayer("Map");
	}
}
