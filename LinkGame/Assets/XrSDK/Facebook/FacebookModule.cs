using Facebook.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XrCode;
namespace XrSDK
{
    public class FacebookModule : BaseModule
    {
        enum Scope
        {
            PublicProfile = 1,
            UserFriends = 2,
            UserBirthday = 3,
            UserAgeRange = 4,
            PublishActions = 5,
            UserLocation = 6,
            UserHometown = 7,
            UserGender = 8,
        };
        public FacebookModule(FacebookData data)
        {

        }

        protected override void OnLoad()
        {
            base.OnLoad();
            FacebookDefines.FaceBookLimitedLogin += FaceBookLimitedLogin;
            FacebookDefines.FaceBookClassicLogin += FaceBookClassicLogin;
            FacebookDefines.FaceBookLogout += FaceBookLogout;

            Debug.Log("Facebook Initialized");
            FaceBookInit();
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            FacebookDefines.FaceBookLimitedLogin -= FaceBookLimitedLogin;
            FacebookDefines.FaceBookClassicLogin -= FaceBookClassicLogin;
            FacebookDefines.FaceBookLogout -= FaceBookLogout;
        }

        private void FaceBookInit()
        {
            FB.Init(this.OnInitComplete, this.OnHideUnity);
        }

        private void FaceBookLimitedLogin()
        {
            this.CallFBLogin(LoginTracking.LIMITED, new HashSet<Scope> { Scope.PublicProfile });
        }

        private void FaceBookClassicLogin()
        {
            this.CallFBLogin(LoginTracking.ENABLED, new HashSet<Scope> { Scope.PublicProfile });
        }

        private void FaceBookLogout()
        {
            CallFBLogout();
        }

        private void OnInitComplete()
        {
            Debug.Log("FaceBook初始化完成！");
            if (AccessToken.CurrentAccessToken != null)
            {
                Debug.Log("CurrentAccessToken：" + AccessToken.CurrentAccessToken.ToString());
            }
        }

        private void OnHideUnity(bool isGameShown)
        {
            Debug.Log("Is game shown: " + isGameShown);
        }

        private void CallFBLogin(LoginTracking mode, HashSet<Scope> scope)
        {
            List<string> scopes = new List<string>();

            if (scope.Contains(Scope.PublicProfile))
            {
                scopes.Add("public_profile");
            }
            if (scope.Contains(Scope.UserFriends))
            {
                scopes.Add("user_friends");
            }
            if (scope.Contains(Scope.UserBirthday))
            {
                scopes.Add("user_birthday");
            }
            if (scope.Contains(Scope.UserAgeRange))
            {
                scopes.Add("user_age_range");
            }

            if (scope.Contains(Scope.UserLocation))
            {
                scopes.Add("user_location");
            }

            if (scope.Contains(Scope.UserHometown))
            {
                scopes.Add("user_hometown");
            }

            if (scope.Contains(Scope.UserGender))
            {
                scopes.Add("user_gender");
            }

            if (mode == LoginTracking.ENABLED)
            {
                if (Constants.CurrentPlatform == FacebookUnityPlatform.IOS)
                {
                    FB.Mobile.LoginWithTrackingPreference(LoginTracking.ENABLED, scopes, "classic_nonce123");
                }
                else
                {
                    FB.LogInWithReadPermissions(scopes);
                }
            }
            else // mode == loginTracking.LIMITED
            {
                FB.Mobile.LoginWithTrackingPreference(LoginTracking.LIMITED, scopes, "limited_nonce123");
            }
        }

        private void CallFBLogout()
        {
            FB.LogOut();
        }
    }
}