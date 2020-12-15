using UnityEngine;

public static class Helper
{
	public static void DrawRect(Rect rect, float y, Color color)
	{
		Debug.DrawLine(new Vector3(rect.xMin, y, rect.yMin), new Vector3(rect.xMin, y, rect.yMax), color);
		Debug.DrawLine(new Vector3(rect.xMin, y, rect.yMin), new Vector3(rect.xMax, y, rect.yMin), color);
		Debug.DrawLine(new Vector3(rect.xMax, y, rect.yMax), new Vector3(rect.xMin, y, rect.yMax), color);
		Debug.DrawLine(new Vector3(rect.xMax, y, rect.yMax), new Vector3(rect.xMax, y, rect.yMin), color);
	}
}
