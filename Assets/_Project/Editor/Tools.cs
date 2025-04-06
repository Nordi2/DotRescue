using UnityEditor;
using UnityEngine;

namespace _Project.Editor
{
    public class Tools
    {
        [MenuItem("Tools/Clear Prefs")]
        public static void CleatPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}
