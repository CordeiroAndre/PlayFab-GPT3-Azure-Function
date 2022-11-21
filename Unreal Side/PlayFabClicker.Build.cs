// Copyright Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;

public class PlayFabClicker : ModuleRules
{
	public PlayFabClicker(ReadOnlyTargetRules Target) : base(Target)
	{
		PCHUsage = ModuleRules.PCHUsageMode.UseExplicitOrSharedPCHs;
		PublicDependencyModuleNames.AddRange(
			new string[]
			{
				"Core",
			}
			);
		PrivateDependencyModuleNames.AddRange(
			new string[]
			{
				"CoreUObject",
				"Engine",
				"Slate",
				"SlateCore",
				"UMG", 
				"PlayFabCommon", 
				"PlayFabCpp"
			}
			);
	}
}
