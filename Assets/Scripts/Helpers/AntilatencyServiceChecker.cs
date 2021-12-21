using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.Events;

namespace Helpers
{
    public class AntilatencyServiceChecker : MonoBehaviour
    {
        public UnityEvent onServiceNotInstalled;
        public UnityEvent onServiceInstalled;

        public UnityEngine.UI.Text debug;

        private void Start()
        {
            CheckForAntilatencyServiceInstallation();
        }

        public void CheckForAntilatencyServiceInstallation()
        {
            
            
            var serviceDownloadUrl =
                "https://developers.antilatency.com/Downloadable/BC6EF242A8420FB83A30D195650AC9C8/AntilatencyService_3.4.0_Android.apk";
            
            Application.OpenURL(serviceDownloadUrl);

            
            var bundleId = "com.antilatency.antilatencyService";
            var up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var ca = up.GetStatic<AndroidJavaObject>("currentActivity");
            debug.text += ca + Environment.NewLine;
            
            var packageManager = ca.Call<AndroidJavaObject>("getPackageManager");
            
            debug.text += packageManager + Environment.NewLine;

            var pluginClass = new AndroidJavaClass("android.content.pm.PackageManager");
            
            debug.text += pluginClass + Environment.NewLine;
            
            var launchIntent =
                packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", bundleId);
            var flag = pluginClass.GetStatic<int>("GET_META_DATA");

            var arrayOfAppInfo =
                packageManager.Call<AndroidJavaObject[]>("getInstalledApplications", flag);

            foreach (var app in arrayOfAppInfo)
            {
                debug.text += app + Environment.NewLine;
                Debug.Log(app.ToString());
            }

            if (launchIntent == null)
            {
                Application.OpenURL(serviceDownloadUrl);
            }
            else
            {
                ca.Call("startActivity", launchIntent);
            }

            up.Dispose();
            ca.Dispose();
            packageManager.Dispose();
            launchIntent.Dispose();


            
        }
        
    }
}
