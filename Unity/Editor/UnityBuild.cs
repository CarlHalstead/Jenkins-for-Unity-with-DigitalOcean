using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;

public class UnityBuild
{
	private static readonly string GameName = PlayerSettings.productName;

	private static string[] EnabledScenePaths => EditorBuildSettings.scenes
		.Where((scene) => scene.enabled == true)
		.Select((scene) => scene.path)
		.ToArray();

	public static void BuildPlatforms()
	{
		List<string> arguments = Environment.GetCommandLineArgs().ToList();

		string buildLocation = string.Empty;

		if (arguments.Contains("-buildPath"))
			buildLocation = arguments[arguments.IndexOf("-buildPath") + 1];
		else
			throw new ArgumentException("Project must be launched with a -buildPath argument! Where would we build the project too otherwise?");

		if (arguments.Contains("-windows32"))
			BuildWindows32(buildLocation);

		if (arguments.Contains("-windows64"))
			BuildWindows64(buildLocation);

		if (arguments.Contains("-linux64"))
			BuildLinux64(buildLocation);

		if (arguments.Contains("-macos"))
			BuildMacOS(buildLocation);

		if (arguments.Contains("-android"))
			BuildAndroid(buildLocation);

		if (arguments.Contains("-ios"))
			BuildiOS(buildLocation);

		if (arguments.Contains("-webgl"))
			BuildWebGL(buildLocation);
	}

	private static void BuildWindows32(string location)
	{
		BuildPlayerOptions playerOptions = new BuildPlayerOptions()
		{
			scenes = EnabledScenePaths,
			locationPathName = Path.Combine(location, "Windows32", $"{GameName}.exe"),
			target = BuildTarget.StandaloneWindows
		};

		BuildPipeline.BuildPlayer(playerOptions);
	}

	private static void BuildWindows64(string location)
	{
		BuildPlayerOptions playerOptions = new BuildPlayerOptions()
		{
			scenes = EnabledScenePaths,
			locationPathName = Path.Combine(location, "Windows64", $"{GameName}.exe"),
			target = BuildTarget.StandaloneWindows64
		};

		BuildPipeline.BuildPlayer(playerOptions);
	}

	private static void BuildLinux64(string location)
	{
		BuildPlayerOptions playerOptions = new BuildPlayerOptions()
		{
			scenes = EnabledScenePaths,
			locationPathName = Path.Combine(location, "Linux64", $"{GameName}.x86_64"),
			target = BuildTarget.StandaloneLinux64
		};

		BuildPipeline.BuildPlayer(playerOptions);
	}

	private static void BuildMacOS(string location)
	{
		BuildPlayerOptions playerOptions = new BuildPlayerOptions()
		{
			scenes = EnabledScenePaths,
			locationPathName = Path.Combine(location, "MacOS", $"{GameName}"),
			target = BuildTarget.StandaloneOSX
		};

		BuildPipeline.BuildPlayer(playerOptions);
	}

	private static void BuildAndroid(string location)
	{
		BuildPlayerOptions playerOptions = new BuildPlayerOptions()
		{
			scenes = EnabledScenePaths,
			locationPathName = Path.Combine(location, "Android", $"{GameName}.apk"),
			target = BuildTarget.Android
		};

		BuildPipeline.BuildPlayer(playerOptions);
	}

	private static void BuildiOS(string location)
	{
		BuildPlayerOptions playerOptions = new BuildPlayerOptions()
		{
			scenes = EnabledScenePaths,
			locationPathName = Path.Combine(location, "iOS", $"{GameName}"),
			target = BuildTarget.iOS
		};

		BuildPipeline.BuildPlayer(playerOptions);
	}

	private static void BuildWebGL(string location)
	{
		BuildPlayerOptions playerOptions = new BuildPlayerOptions()
		{
			scenes = EnabledScenePaths,
			locationPathName = Path.Combine(location, "WebGL", $"{GameName}"),
			target = BuildTarget.WebGL
		};

		BuildPipeline.BuildPlayer(playerOptions);
	}
}
