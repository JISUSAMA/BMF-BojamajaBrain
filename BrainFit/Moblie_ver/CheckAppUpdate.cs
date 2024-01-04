using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//AppUpdateManager 인앱 업데이트 지원 
using Google.Play.AppUpdate;
using Google.Play.Common;



/// <summary>
/// https://developer.android.com/reference/unity/class/Google/Play/AppUpdate/AppUpdateInfo#clientversionstalenessdays
/// https://developer.android.com/reference/unity/namespace/Google/Play/AppUpdate
/// </summary>
public class CheckAppUpdate : MonoBehaviour
{
    public TMP_Text updateStatusLog;
    AppUpdateManager appUpdateManager = null;
    //유니티 플러그 인앱과 Play API 간 통신을 처리하는 AppUpdateManager 클래스

    //앱 업데이트 작업

    //
    private void Awake()
    {
        //업데이트 하고 얼마나 지났는지 경과 일수 확인 하는 방법
        // var stalenessDays = appUpdateInfoOperation.GetResult();
        StartCoroutine(CheckForUpdate());
    }
    /// <summary>
    /// Google.Play.AppUpdate 클래스 
    /// AppUpdateInfo : 앱의 업데이트 가용성 및 설치 진행률에 대한 정보
    /// AppUpdateManager : 앱 내에 업데이트를 요청하고 시작하는 작업
    /// AppUpdateOptions : AppUpdateType을 포함하여 앱 내 업데이트를 구성하는 데 사용되는 옵션입니다.
    /// AppUpdateRequest : 진행 중인 앱 내 업데이트를 모니터링하는 데 사용되는 사용자 지정 수율 명령입니다.(?)
    /// </summary>
    /// <returns></returns>
    //업데이트 사용 가능 여부를 확인 
    IEnumerator CheckForUpdate()
    {
        appUpdateManager = new AppUpdateManager();
        PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfoOperation;
        appUpdateInfoOperation = appUpdateManager.GetAppUpdateInfo();
        // Wait until the asynchronous operation completes.
        //비동기 작업이 완료될 때까지 기다립니다.
        yield return appUpdateInfoOperation;

        // var appUpdateOptions = AppUpdateOptions.FlexibleAppUpdateOptions();

        if (appUpdateInfoOperation.IsSuccessful)
        {
            // Check AppUpdateInfo's UpdateAvailability, UpdatePriority,
            // IsUpdateTypeAllowed(), etc. and decide whether to ask the user
            // to start an in-app update.

            /*AppUpdateInfo의 UpdateAvailability, UpdatePriority,
            IsUpdateTypeAllowed() 등 및 사용자에게 요청할지 여부를 결정
            앱 내 업데이트를 시작합니다.*/

            var appUpdateInfoResult = appUpdateInfoOperation.GetResult();
            //  AppUpdateManager.StartUpdate() 업데이트를 요청 
            //  업데이트를 요청하기 전에 최신 AppUpdataeInfo가 있는지 확인해야함
            //  UpdateAvailability ,앱 내 업데이트에 대한 가용성 정보입니다.
            if (appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateAvailable)
            {
                Debug.Log("start updateAble");
                //업데이트 사용 가능
                /// AppUpdateOptions : AppUpdateType을 포함하여 앱 내 업데이트를 구성하는 데 사용되는 옵션입니다.
                var appUpdateOptions = AppUpdateOptions.FlexibleAppUpdateOptions(); //유연한 업데이트 처리 AppUpdateType =0
                                                                                    //var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions(); //즉시 업데이트 처리 AppUpdateType =1

                //지정된 업데이트 유형에 대해 앱 내 업데이트 시작.

                var startUpdateRequest = appUpdateManager.StartUpdate(
                    //appUpdateInfoOperation.GetResult()의 결과를 가져옴
                    appUpdateInfoResult,
                    //업데이트 처리 방식을 가져옴
                    appUpdateOptions);
           
                //startUpdateRequest.IsDone은 업데이트가 완료가 되면 True, 아닐 경우 Fales
                while (!startUpdateRequest.IsDone)
                {
                    Debug.Log("startUpdateRequest.IsDone");
                    if (startUpdateRequest.Status == AppUpdateStatus.Pending)
                    {
                        //업데이트 다운로드가 보류중 곧 처리 될 예정
                        updateStatusLog.gameObject.SetActive(true);
                    }
                    else if (startUpdateRequest.Status == AppUpdateStatus.Downloading)
                    {
                        //업데이트 다운로드가 진행 중 일 경우, 
                        Debug.Log(" AppUpdateStatus.Downloading");
                        updateStatusLog.text = $"Downloading ... {Mathf.Floor(startUpdateRequest.DownloadProgress * 100) }%";
                        //downloadProgressbar.value = startUpdateRequest.DownloadProgress;
                    }
                    else if (startUpdateRequest.Status == AppUpdateStatus.Downloaded)
                    {
                        //업데이트가 완전히 끝난 경우,
                        Debug.Log("AppUpdateStatus.Downloaded");
                        updateStatusLog.gameObject.SetActive(false);
                    }
                    yield return null;
                }
                //StartUpdate를 통해 시작된 유연한 입앱 업데이트 흐름을 완료하도록 비동기적으로 요청함, 
                //AppUpdateRequest가 완료되고 호출 해야함
                var result = appUpdateManager.CompleteUpdate();

                //업데이트가 완전히 끝난게 아니면
                while (!result.IsDone)
                {
                    yield return new WaitForEndOfFrame();
                    Debug.Log("CompleteUpdate.Status 2: " + startUpdateRequest.Status);
                    updateStatusLog.text = $"{startUpdateRequest.Status}.";
                }

                updateStatusLog.text = $"{startUpdateRequest.Status}";
                // 앱 내 업데이트 api 상태
                //https://developer.android.com/reference/unity/namespace/Google/Play/AppUpdate#appupdatestatus
                //0 : Unknown 1:Pending 2:Downloading 3:Downloaded 4:Installing 5:Installed 6:Failed 7:Canceled
                yield return (int)startUpdateRequest.Status;
            }
            else if (appUpdateInfoResult.UpdateAvailability == UpdateAvailability.DeveloperTriggeredUpdateInProgress)
            {
                //개발자 트리거 업데이트 진행 중
                updateStatusLog.text = "Update In Progress..";
                //  downloadProgressbar.gameObject.SetActive(true);

                var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions(); //즉시 업데이트 처리 
                var startUpdateRequest = appUpdateManager.StartUpdate(appUpdateInfoResult, appUpdateOptions);
                //업데이트가 완료 되지 않은 상황이면, 
                while (!startUpdateRequest.IsDone)
                {
                    yield return new WaitForEndOfFrame();
                    Debug.Log(appUpdateInfoResult.ToString());

                    //다운로드 중 일 때,
                    if (startUpdateRequest.Status == AppUpdateStatus.Downloading)
                    {
                        updateStatusLog.text = $"Downloading ... {Mathf.Floor(startUpdateRequest.DownloadProgress * 100) }%";
                        //    downloadProgressbar.value = startUpdateRequest.DownloadProgress;
                    }
                    //다운로드가 끝났으면
                    else if (startUpdateRequest.Status == AppUpdateStatus.Downloaded)
                    {
                        //  downloadProgressbar.gameObject.SetActive(false);
                    }
                }
                var result = appUpdateManager.CompleteUpdate();
                while (!result.IsDone)
                {
                    yield return new WaitForEndOfFrame();
                    updateStatusLog.text = $"{startUpdateRequest.Status}";
                }
                updateStatusLog.text = $"{startUpdateRequest.Status}";
                // 앱 내 업데이트 api 상태
                //https://developer.android.com/reference/unity/namespace/Google/Play/AppUpdate#appupdatestatus
                //0 : Unknown 1:Pending 2:Downloading 3:Downloaded 4:Installing 5:Installed 6:Failed 7:Canceled
                yield return (int)startUpdateRequest.Status;
            }
            //업데이트를 사용할 수 없음
            else if (appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateNotAvailable)
            {
                yield return (int)UpdateAvailability.UpdateNotAvailable;  // 1
            }
            else
            {
                yield return (int)UpdateAvailability.Unknown; // 0
            }


            //   var startUpdateRequest = appUpdateManager.StartUpdate(
            //       // PlayAsync Operation에서 반환한 결과입니다.결과 가져오기().
            //       appUpdateInfoResult,
            //       //요청된 앱 내 업데이트 및 해당 매개 변수를 정의하는 앱 업데이트 옵션이 생성되었습니다.
            //       appUpdateOptions);

        }
        else
        {
            // Log appUpdateInfoOperation.Error.
            //appUpdateInfoOperation을 기록합니다.오류.
        }
    }
}
