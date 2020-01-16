using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;

//Banner ad
public class Admob : MonoBehaviour
{
	private BannerView adBanner;
	private UnifiedNativeAd adNative;
	private bool nativeLoaded = false;

	private string idApp, idBanner, idNative;

	[SerializeField] GameObject adNativePanel;
	[SerializeField] RawImage adIcon;
	[SerializeField] RawImage adChoices;
	[SerializeField] Text adHeadline;
	[SerializeField] Text adCallToAction;
	[SerializeField] Text adAdvertiser;


	void Awake ()
	{
		adNativePanel.SetActive (false); //hide ad panel
	}

	void Start ()
	{
		idApp = "ca-app-pub-3940256099942544~3347511713";
		idBanner = "ca-app-pub-3940256099942544/6300978111";
		idNative = "ca-app-pub-3940256099942544/2247696110";

		MobileAds.Initialize (idApp);

		RequestBannerAd ();
		RequestNativeAd ();
	}

	void Update ()
	{
		if (nativeLoaded) {
			nativeLoaded = false;

			Texture2D iconTexture = this.adNative.GetIconTexture ();
			Texture2D iconAdChoices = this.adNative.GetAdChoicesLogoTexture ();
			string headline = this.adNative.GetHeadlineText ();
			string cta = this.adNative.GetCallToActionText ();
			string advertiser = this.adNative.GetAdvertiserText ();
			adIcon.texture = iconTexture;
			adChoices.texture = iconAdChoices;
			adHeadline.text = headline;
			adAdvertiser.text = advertiser;
			adCallToAction.text = cta;

			//register gameobjects
			adNative.RegisterIconImageGameObject (adIcon.gameObject);
			adNative.RegisterAdChoicesLogoGameObject (adChoices.gameObject);
			adNative.RegisterHeadlineTextGameObject (adHeadline.gameObject);
			adNative.RegisterCallToActionGameObject (adCallToAction.gameObject);
			adNative.RegisterAdvertiserTextGameObject (adAdvertiser.gameObject);

			adNativePanel.SetActive (true); //show ad panel
		}
	}

	#region Banner Methods --------------------------------------------------

	public void RequestBannerAd ()
	{
		adBanner = new BannerView (idBanner, AdSize.Banner, AdPosition.Bottom);
		AdRequest request = AdRequestBuild ();
		adBanner.LoadAd (request);
	}

	public void DestroyBannerAd ()
	{
		if (adBanner != null)
			adBanner.Destroy ();
	}

	#endregion

	#region Native Ad Mehods ------------------------------------------------

	private void RequestNativeAd ()
	{
		AdLoader adLoader = new AdLoader.Builder (idNative).ForUnifiedNativeAd ().Build ();
		adLoader.OnUnifiedNativeAdLoaded += this.HandleOnUnifiedNativeAdLoaded;
		adLoader.LoadAd (AdRequestBuild ());
	}

	//events
	private void HandleOnUnifiedNativeAdLoaded (object sender, UnifiedNativeAdEventArgs args)
	{
		this.adNative = args.nativeAd;
		nativeLoaded = true;
	}

	#endregion

	//------------------------------------------------------------------------
	AdRequest AdRequestBuild ()
	{
		return new AdRequest.Builder ().Build ();
	}

	void OnDestroy ()
	{
		DestroyBannerAd ();
	}

}

