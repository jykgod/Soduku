using UnityEngine;
using System.Collections;
using admob;
public class admobdemo : MonoBehaviour {

	public static admobdemo Instance;
	void Awake(){
		Instance = this;
		initAdmob();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    Admob ad;
    //bool isAdmobInited = false;
    void initAdmob()
    {
        
          //  isAdmobInited = true;
            ad = Admob.Instance();
            ad.bannerEventHandler += onBannerEvent;
            ad.interstitialEventHandler += onInterstitialEvent;
            ad.rewardedVideoEventHandler += onRewardedVideoEvent;
            ad.nativeBannerEventHandler += onNativeBannerEvent;
			ad.initAdmob("ca-app-pub-2267601683777960/1468758532", "ca-app-pub-2267601683777960/2945491736");
        //   ad.setTesting(true);
    }
	public void ShowBanner1(){
		ad.showBannerRelative(AdSize.SmartBanner, AdPosition.BOTTOM_CENTER, 0, "1");
		ad.removeBanner ("2");
	}

	public void ShowFull(){
		ad.loadInterstitial ();
	}

	public void ShowBanner2(){
		ad.removeBanner ("1");
		ad.showBannerRelative(AdSize.SmartBanner, AdPosition.TOP_CENTER, 0, "2");
	}

    void onInterstitialEvent(string eventName, string msg)
    {
        Debug.Log("handler onAdmobEvent---" + eventName + "   " + msg);
        if (eventName == AdmobEvent.onAdLoaded)
        {
            Admob.Instance().showInterstitial();
        }
    }
    void onBannerEvent(string eventName, string msg)
    {
        Debug.Log("handler onAdmobBannerEvent---" + eventName + "   " + msg);
    }
    void onRewardedVideoEvent(string eventName, string msg)
    {
        Debug.Log("handler onRewardedVideoEvent---" + eventName + "   " + msg);
    }
    void onNativeBannerEvent(string eventName, string msg)
    {
        Debug.Log("handler onAdmobNativeBannerEvent---" + eventName + "   " + msg);
    }
}
