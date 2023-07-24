using System;
using UnityEngine;
using UnityEngine.UI;
using Resources = HKGuide.ModTools.Resources;

namespace HKGuide.ModObjects
{
	public class TextModObject
	{
        public GameObject Object;
        public RectTransform Transform;
        public CanvasGroup Group;
        public Color Color;
        public Text Text;

        public TextModObject(GameObject parent, string text, float opacity = 1f)
		{
            Object = new GameObject();
            Object.AddComponent<CanvasRenderer>();
            Transform = Object.AddComponent<RectTransform>();
            Group = Object.AddComponent<CanvasGroup>();
            Group.interactable = false;
            Group.blocksRaycasts = false;
            Color = new Color(255f, 255f, 255f, opacity);

            Text = Object.AddComponent<Text>();
            Text.text = text;
            Text.font = Resources.Instance.Fonts.arial;
            Text.fontSize = 18;
            Text.fontStyle = FontStyle.Normal;
            Text.alignment = TextAnchor.MiddleLeft;
            Text.color = Color;

            ContentSizeFitter contentSizeFitter = Object.AddComponent<ContentSizeFitter>();
            contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            Object.transform.SetParent(parent.transform, false);

            UnityEngine.Object.DontDestroyOnLoad(Object);
        }
	}
}

