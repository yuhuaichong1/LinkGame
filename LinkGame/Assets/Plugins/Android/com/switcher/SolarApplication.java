package com.switcher;

import android.app.Application;

import com.gg.user.Login;

public class SolarApplication extends Application {

    @Override
    public void onCreate() {
        super.onCreate();
        Login.getInstance(this).initialize("a0515c093f0f1ac7");
    }
}
