using GoogleMobileAds.Api;
using UnityEngine;

public class AdmobAdsManager : MonoBehaviour
{   
    private const string Banner = nameof(Banner);
    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.ShowInterAd, ShowInterstitialAd);
        EventManager.AddHandler(GameEvent.ShowRewardAd, ShowRewardedAd);
    }


    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.ShowRewardAd, ShowRewardedAd);
        EventManager.RemoveHandler(GameEvent.ShowInterAd, ShowInterstitialAd);
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private static AdmobAdsManager instance;
    public static AdmobAdsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AdmobAdsManager>();
            }
            return instance;
        }
    }

    [SerializeField] private string appID = "ca-app-pub-3289243164533066~8203554964";

#if UNITY_ANDROID
    private string bannerID = "ca-app-pub-3289243164533066/8387387792";
    private string interID = "ca-app-pub-3289243164533066/5524586581";
    private string rewardedID = "ca-app-pub-3289243164533066/6889639148";
#elif UNITY_IPHONE
        private string bannerID = "ca-app-pub-3940256099942544/2934735716";
        private string interID = "ca-app-pub-3940256099942544/4411468910";
        private string rewardedID = "ca-app-pub-3940256099942544/1712485313";
#endif

    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;


    private void Start()
    {
        LoadInterstitialAd();
        LoadRewardedAd();
        LoadBannerAd();


        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        MobileAds.Initialize(initStatus =>
        {
            print("Admob initialized");
        });

        var myBannerAd = GameObject.FindGameObjectWithTag(Banner);
        DontDestroyOnLoad(myBannerAd);

    }

    #region Rewarded

    public void LoadRewardedAd()
    {

        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        RewardedAd.Load(rewardedID, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                print("Rewarded failed to load" + error);
                return;
            }

            print("Rewarded ad loaded !!");
            rewardedAd = ad;
            RewardedAdEvents(rewardedAd);
        });
    }
    public void ShowRewardedAd()
    {

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                print("Give reward to player !!");
                //GameManager.Instance.GrantCoins(1);
            });
        }
        else
        {
            print("Rewarded ad not ready");
        }

        Time.timeScale = 1; // resume the game
        LoadRewardedAd();

    }
    public void RewardedAdEvents(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log("Rewarded ad paid {0} {1}." +
                adValue.Value +
                adValue.CurrencyCode);
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
        };
    }

    #endregion

    #region BannerAds

    public void LoadBannerAd()
    {
        CreateBannerView();
        ListenToBannerEvent();
        if (bannerView == null)
        {
            CreateBannerView();
        }
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");
        print("Loading Banner Ad");
        bannerView?.LoadAd(adRequest);// show the banner ad on the screen
    }

    private void CreateBannerView()
    {
        if (bannerView != null)
        {
            DestroyBannerAd();
        }
        bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);
    }

    private void ListenToBannerEvent()
    {
        bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad with response : "
                      + bannerView.GetResponseInfo());
        };
        // Raised when an ad fails to load into the banner view.
        bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : "
                           + error);
        };
        // Raised when the ad is estimated to have earned money.
        bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log("Banner view paid {0} {1}." +
                      adValue.Value +
                      adValue.CurrencyCode);
        };
        // Raised when an impression is recorded for an ad.
        bannerView.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        bannerView.OnAdClicked += () =>
        {
            Debug.Log("Banner view was clicked.");
        };
        // Raised when an ad opened full screen content.
        bannerView.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        bannerView.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view full screen content closed.");
        };
    }

    public void DestroyBannerAd()
    {
        if (bannerView != null)
        {
            print("Destroying Banner Ad");
            bannerView.Destroy();
            bannerView = null;
        }
    }

    #endregion

    #region InterstitialAds
    public void LoadInterstitialAd()
    {
        DestroyInterstitialAd();
    }

    private void DestroyInterstitialAd()
    {
        if (interstitialAd != null)
        {
            print("Destroying Interstitial Ad");
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");
        print("Loading Interstitial Ad");

        InterstitialAd.Load(interID, adRequest, (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                print("Interstitial ad failed to load with error: " + error?.ToString());
                return;
            }
            print("Interstitial ad loaded " + ad.GetResponseInfo());
            interstitialAd = ad;
            InterstitialEvents(interstitialAd);
        });

    }
    public void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
        }
        else
        {
            print("Interstitial ad is not ready yet");
        }
        LoadInterstitialAd();
    }

    public void InterstitialEvents(InterstitialAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log("Interstitial ad paid {0} {1}." +
                      adValue.Value +
                      adValue.CurrencyCode);
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
        };
    }

    #endregion
}