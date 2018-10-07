#if UNITY_EDITOR
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class IconGenerator : MonoBehaviour {
	
	[Header("Prefabs")]
	public GameObject[] targets;
	public string[] fileNames;
	[Header("Custom settings")]
	[Tooltip("Custom folder used to save the icon. LEAVE EMPTY FOR DEFAULT!")]
	public string customFolder = ""; // OOPS! You need to create folder manually, before running the script!
	public bool usePrefabNames = false;

	[Header("Icons")]
	public Texture rawIcon;
	public Texture2D icon;

	void Start ()
	{
		int targetCount = 0;
		string iconName;

		if (targets == null) // Check if targets are specified
		{
			Debug.LogError ("You need to specify targets!");
			return;
		}

		foreach (GameObject target in targets)
		{
			rawIcon = AssetPreview.GetAssetPreview (target);
			icon = rawIcon as Texture2D;

			byte[] bytes = icon.EncodeToPNG ();

			if(usePrefabNames) // Check if usePrefabNames is enabled!
				iconName = target.name;
			else
				iconName = fileNames [targetCount];

			GameObject.Find("Canvas").GetComponent<IconGeneratorUIExample>().AddImage (icon,iconName); // Used for example, can be removed!

			if (customFolder == "") // Check if custom folder is specified!
			{
				File.WriteAllBytes (Application.dataPath + "/" + iconName + ".png", bytes);
				Debug.Log ("File saved in: " + Application.dataPath + "/" + iconName + ".png");
			}
			else
			{
				File.WriteAllBytes (Application.dataPath + "/" + customFolder + "/" + iconName + ".png", bytes);
				Debug.Log ("File saved in: " + Application.dataPath + "/" + customFolder + "/" + iconName + ".png");
			}

			targetCount++;
		}
	}
}

#endif