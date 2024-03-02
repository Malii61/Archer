using UnityEngine;
using GoogleMobileAds.Api;
public class ADController : MonoBehaviour
{
    private BannerAD banner;
    private InterstitialAD interstitial;

    public static ADController Instance;

    //public delegate void OnReward();
    //public static event OnReward OnGaveReward;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        banner = GetComponent<BannerAD>();
        interstitial = GetComponent<InterstitialAD>();
    }
    private void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            interstitial.LoadInterstitialAd();
            //reward.LoadRewardedAd();
            ShowBanner();
        });
    }
    public void ShowBanner()
    {
        banner.LoadAd();
    }
    public void ShowInterstitial()
    {
        interstitial.ShowAd();
    }
    //public void ShowRewardAd()
    //{
    //    reward.ShowRewardedAd();
    //}
    //public void GiveReward()
    //{
    //    OnGaveReward?.Invoke();
    //}
}