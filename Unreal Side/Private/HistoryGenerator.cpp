
#include "HistoryGenerator.h"
#include "PlayFabCloudScriptAPI.h"

UHistoryGenerator* UHistoryGenerator::GenerateHistory()
{
	UHistoryGenerator* HistoryGenerator = NewObject<UHistoryGenerator>();

	
	PlayFab::CloudScriptModels::FExecuteFunctionRequest Request;
	Request.FunctionName = FString("CreateRandomHistory");
	PlayFab::UPlayFabCloudScriptAPI::FExecuteFunctionDelegate OnSuccess;
	OnSuccess.BindUObject(HistoryGenerator, &UHistoryGenerator::GenerateHistorySuccess);
	PlayFab::FPlayFabErrorDelegate OnError;
	OnError.BindUObject(HistoryGenerator, &UHistoryGenerator::GenerateHistoryError);
	PlayFab::UPlayFabCloudScriptAPI().ExecuteFunction(Request, OnSuccess, OnError);
	
	return HistoryGenerator;
}

void UHistoryGenerator::GenerateHistorySuccess(const PlayFab::CloudScriptModels::FExecuteFunctionResult& Result)
{
	FString History;
	Result.FunctionResult.GetJsonValue().Get()->TryGetString(History);
	const int MinimumHistoryLength = 5;
	History.Len() > MinimumHistoryLength ?   OnResponse.Broadcast(History) : OnResponse.Broadcast(FString("This is a cursed button. You need to press it multiple times."));
}

void UHistoryGenerator::GenerateHistoryError(const PlayFab::FPlayFabCppError& Result)
{
	OnResponse.Broadcast(FString("This is a cursed button. You need to press it multiple times."));
}
