using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds;

public class AdmobManager : MonoBehaviour
{



    private InterstitialAd interstitial;
    private void Start()
    {
        MobileAds.Initialize(initStatus => { });
        RequestInterstitial();

    }

    private void RequestInterstitial()
    {

        string adUnitId = "ca-app-pub-7174486247480198/8501212793";




        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);

    }
    public void GameOver()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }

}

