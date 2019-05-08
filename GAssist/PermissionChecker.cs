﻿using System;
using Tizen.Security;

namespace GAssist
{
    class PermissionChecker
    {
        public const string recorderPermission = "http://tizen.org/privilege/recorder";

        public PermissionChecker()
        {
            //const string audioRecorderPermission = "http://tizen.org/privilege/audiorecorder";
            
            //SetupPPMHandler(audioRecorderPermission);
            
        }
        


        public static void  CheckAndRequestPermission(String permission)
        {
            SetupPPMHandler(recorderPermission);
            try
            {
                CheckResult result = PrivacyPrivilegeManager.CheckPermission(permission);
                switch (result)
                {
                    case CheckResult.Allow:
                        /// Update UI and start accessing protected functionality
                        break;
                    case CheckResult.Deny:
                        PrivacyPrivilegeManager.RequestPermission(permission);
                        break;
                    case CheckResult.Ask:
                        PrivacyPrivilegeManager.RequestPermission(permission);
                        break;
                }
            }
            catch (Exception e)
            {
                /// Handle exception
            }
        }

        private static void SetupPPMHandler(string privilege)
        {
            PrivacyPrivilegeManager.ResponseContext context = null;
            if (PrivacyPrivilegeManager.GetResponseContext(privilege).TryGetTarget(out context))
            {
                context.ResponseFetched += PPMResponseHandler;
            }
        }

        private static void PPMResponseHandler(object sender, RequestResponseEventArgs e)
        {
            if (e.cause == CallCause.Error)
            {
                /// Handle errors
                return;
            }

            switch (e.result)
            {
                case RequestResult.AllowForever:
                    /// Update UI and start accessing protected functionality
                    break;
                case RequestResult.DenyForever:
                    /// Show a message and terminate the application
                    break;
                case RequestResult.DenyOnce:
                    /// Show a message with explanation
                    break;
            }
        }

    }
}
