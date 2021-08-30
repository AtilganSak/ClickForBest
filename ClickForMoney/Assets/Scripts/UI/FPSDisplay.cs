using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour
{
	float deltaTime = 0.0f;

	void Update()
	{
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
	}
	GUIStyle style = new GUIStyle();
	void OnGUI()
	{
		Rect rect = new Rect(Screen.width / 2 - 150, Screen.height / 2, Screen.width, Screen.height * 2 / 100);
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = Screen.height * 2 / 100;
		style.normal.textColor = Color.white;
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		GUI.Label(rect, text, style);
	}
}