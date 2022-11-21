// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "PlayFabCloudScriptDataModels.h"
#include "PlayFabError.h"
#include "Kismet/BlueprintAsyncActionBase.h"
#include "HistoryGenerator.generated.h"

/**
 * 
 */
UCLASS(BlueprintType)
class PLAYFABCLICKER_API UHistoryGenerator : public UBlueprintAsyncActionBase
{
	GENERATED_BODY()


public:
	DECLARE_DYNAMIC_MULTICAST_DELEGATE_OneParam(FOnGenerateHistoryCompleted, FString, History);
	UPROPERTY(BlueprintAssignable)
	FOnGenerateHistoryCompleted OnResponse;

	UFUNCTION(BlueprintCallable, meta=(BlueprintInternalUseOnly = "true"), Category="Clicker|History")
	static UHistoryGenerator* GenerateHistory();

private:
	void GenerateHistorySuccess(const PlayFab::CloudScriptModels::FExecuteFunctionResult& Result);
	void GenerateHistoryError(const PlayFab::FPlayFabCppError& Result);
	
};
