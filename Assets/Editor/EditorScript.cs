using UnityEditor;
using UnityEngine;
using System.Collections;

public class EditorScript : MonoBehaviour {
	
	//BuildOptions.ShowBuiltPlayer
	//BuildOptions.AutoRunPlayer
	
	public static void PerformBuild() {
		BuildStandaloneWin();
		BuildWebGL();
		// BuildAndroid();
	}
	
	public static string GetName () {
		return "ANiceGameAboutDinosaurs";
	}
	
	public static string[] GetScenes () {
		return new string[] {"Assets/ANGAD_main.unity"};
	}
	
    public static void BuildStandaloneWin()
    {      
        BuildPipeline.BuildPlayer(GetScenes(), "Builds/PC/" + GetName() + "StandaloneWin.exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
	}
	
	public static void BuildWebGL()
    {          
        BuildPipeline.BuildPlayer(GetScenes(), "Builds/WebGL", BuildTarget.WebGL, BuildOptions.None);
	}
	
	public static void BuildAndroid()
    {          
        BuildPipeline.BuildPlayer(GetScenes(), "Builds/Android/" + GetName() + ".apk", BuildTarget.Android, BuildOptions.None);
	}
}
