using System;

namespace XrCode
{
    public class AdModule : BaseModule
    {
        protected override void OnLoad() 
        { 
            
        }

        public void PlayRewardAd(Action successAction, Action<string> failAction = null)
        {
            successAction?.Invoke();
        }

        protected override void OnDispose() 
        {
        
        }


    }
}
