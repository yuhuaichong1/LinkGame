using System;

namespace XrCode
{
    public class AdModule : BaseModule
    {
        protected override void OnLoad() 
        {
            FacadeAd.PlayRewardAd += PlayRewardAd;
        }

        public void PlayRewardAd(EAdSource eAdSource, Action successAction, Action<string> failAction = null)
        {
            successAction?.Invoke();
        }

        public void PlayInterAd(EAdSource eAdSource, Action successAction, Action<string> failAction = null)
        {

        }

        protected override void OnDispose() 
        {
        
        }


    }
}
