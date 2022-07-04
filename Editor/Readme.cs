using System;
using UnityEngine;

namespace NFTPort.Editor
{
	public class Readme : ScriptableObject {
		public Texture2D icon;
		public float iconMaxWidth = 128f;
		public string title;
		public Section[] sections;
		public bool loadedLayout;
		public bool notFirstload;
	
		[Serializable]
		public class Section {
			public string heading, text, linkText, url;
		}
	}
}
