using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class OnirimDebuggerEditorWindow : EditorWindow
{
	OnirimGameController controller;
	OnirimGameModel model;

	[MenuItem("Window/Onirim/Debugger")]
	public static void ShowWindow()
	{
		GetWindow<OnirimDebuggerEditorWindow>(false, "Onirim Debugger", true);
	}



	private void OnGUI()
	{

	}
}
