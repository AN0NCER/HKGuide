using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

namespace HKGuide.ModTools
{
    public class Resources : MonoBehaviour
    {
        private static Resources? _instance;

        //Resources Images
        public Dictionary<string, Texture2D> Images = new Dictionary<string, Texture2D>();

        //Resources Levels
        public Dictionary<string, string> Levels = new Dictionary<string, string>();

        //Resources Fonts
        public static class Fonts
        {
            public static Font? trajanBold;
            public static Font? trajanNormal;
            public static Font? arial;
        }

        public void Load()
        {
            LoadFonts();
            LoadImages();
            LoadLevels();
        }

        /// <summary>
        /// Loading Fonts to Resources (Instance)
        /// </summary>
        private void LoadFonts()
        {
            foreach (Font f in UnityEngine.Resources.FindObjectsOfTypeAll<Font>())
            {
                if (f != null && f.name == "TrajanPro-Bold")
                {
                    Fonts.trajanBold = f;
                }

                if (f != null && f.name == "TrajanPro-Regular")
                {
                    Fonts.trajanNormal = f;
                }

                //Just in case for some reason the computer doesn't have arial
                if (f != null && f.name == "Perpetua")
                {
                    Fonts.arial = f;
                }

                foreach (string font in Font.GetOSInstalledFontNames())
                {
                    if (font.ToLower().Contains("arial"))
                    {
                        Fonts.arial = Font.CreateDynamicFontFromOSFont(font, 13);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Loading Images to Resources (Instance)
        /// </summary>
        private void LoadImages()
        {
            string[] resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            foreach (string res in resourceNames)
            {
                if (res.StartsWith("HKGuide.ModResources."))
                {
                    try
                    {
                        Stream imageStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(res);
                        byte[] buffer = new byte[imageStream.Length];
                        imageStream.Read(buffer, 0, buffer.Length);

                        Texture2D tex = new Texture2D(1, 1);
                        tex.LoadImage(buffer.ToArray());

                        string internalName = Path.GetFileNameWithoutExtension(res);
                        Images.Add(internalName, tex);
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Loading Levels to Resources (Instance)
        /// </summary>
        private void LoadLevels()
        {
            string[] resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            foreach (string res in resourceNames)
            {
                if (res.StartsWith("HKGuide.ModLevels."))
                {
                    try
                    {
                        string[] split = SplitIntoSentences(res);
                        string internalName = split[2];
                        Levels.Add(internalName, res);
                    }
                    catch { }
                }
            }
        }

        private string[] SplitIntoSentences(string res)
        {
            // Define template for sentence separator
            string pattern = @"[^.]+";
            // Split text into sentences using a regular expression
            MatchCollection matches = Regex.Matches(res, pattern);
            string[] sentences = matches.Cast<Match>().Select(m => m.Value).ToArray();
            return sentences;
        }

        public static Resources Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<Resources>();
                    if (_instance == null)
                    {
                        GameObject GUIObj = new GameObject();
                        _instance = GUIObj.AddComponent<Resources>();
                        DontDestroyOnLoad(GUIObj);
                    }
                }
                return _instance;
            }
        }
    }
}

