using GameAnalyticsSDK;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class DuckyAnalytics : Singleton<DuckyAnalytics>
{
    private const string IDGenerationChars = "abcdefghijklmnopqrstuvwxyz0123456789";
    private const int ProfileIdLength = 10;

    private string _profileId;

    private void Awake()
    {
        GameAnalytics.Initialize();
    }

    private void Start()
    {
        TrySendRegDayEvent();
        SendGameStartCountEvent();
        SendDaysPlayedEvent();
    }

    private void TrySendRegDayEvent()
    {
        bool isLogged = CustomPlayerPrefs.GetBool(DuckyAnalyticPrefs.FirstTimeLog);

        if (isLogged == false)
        {
            CustomPlayerPrefs.SetBool(DuckyAnalyticPrefs.FirstTimeLog, true);

            DateTime date = DateTime.Today;
            string dateString = date.ToString("dd/MM/yyyy");

            PlayerPrefs.SetString(DuckyAnalyticPrefs.FirstPlayDate, dateString);

            YandexAppMetricaUserProfile userProfile = new YandexAppMetricaUserProfile();
            userProfile.Apply(YandexAppMetricaAttribute.CustomString("reg_day").WithValue(dateString));
            ReportUserProfile(userProfile);
        }
    }

    private void SendGameStartCountEvent()
    {
        int playsCount = PlayerPrefs.GetInt(DuckyAnalyticPrefs.StartsCount, 0);
        playsCount++;
        PlayerPrefs.SetInt(DuckyAnalyticPrefs.StartsCount, playsCount);

        Dictionary<string, object> eventProps = new Dictionary<string, object>();
        eventProps.Add("count", playsCount);

        SendAnalyticsEvents("game_start", eventProps);

        YandexAppMetricaUserProfile userProfile = new YandexAppMetricaUserProfile();
        userProfile.Apply(YandexAppMetricaAttribute.CustomCounter("sessions_count").WithDelta(playsCount));
        ReportUserProfile(userProfile);
    }

    private void SendDaysPlayedEvent()
    {
        try
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            string firstPlayDateString = PlayerPrefs.GetString(DuckyAnalyticPrefs.FirstPlayDate, DateTime.Now.ToString("dd/MM/yyyy"));
            var date = DateTime.ParseExact(firstPlayDateString, "dd/MM/yyyy", provider);
            int daysInGame = DateTime.Today.Subtract(date).Days;

            YandexAppMetricaUserProfile userProfile = new YandexAppMetricaUserProfile();
            userProfile.Apply(YandexAppMetricaAttribute.CustomCounter("days_in_game").WithDelta(daysInGame));
            ReportUserProfile(userProfile);
        }
        catch (FormatException)
        {
            throw new ArgumentException($"1{PlayerPrefs.GetString(DuckyAnalyticPrefs.FirstPlayDate, "")}1 is not valid date!");
        }
    }
    
    private static void SendAnalyticsEvents(string eventName, Dictionary<string, object> eventProps)
    {
        AppMetrica.Instance.ReportEvent(eventName, eventProps);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, eventName, eventProps);
    }
    
    private void ReportUserProfile(YandexAppMetricaUserProfile userProfile)
    {
        AppMetrica.Instance.SetUserProfileID(GetProfileId());
        AppMetrica.Instance.ReportUserProfile(userProfile);
    }

    private string GetProfileId()
    {
        if (PlayerPrefs.HasKey(DuckyAnalyticPrefs.ProfileID))
        {
            _profileId = PlayerPrefs.GetString(DuckyAnalyticPrefs.ProfileID, "");
        }
        else
        {
            _profileId = GenerateProfileId(ProfileIdLength);
            PlayerPrefs.SetString(DuckyAnalyticPrefs.ProfileID, _profileId);
        }

        return _profileId;
    }

    private string GenerateProfileId(int length)
    {
        var random = new System.Random();

        return new string(Enumerable.Repeat(IDGenerationChars, length)
            .Select(letter => letter[random.Next(letter.Length)]).ToArray());
    }
}

public static class DuckyAnalyticPrefs
{
    public const string FirstTimeLog = "FirstTimeLog";
    public const string FirstPlayDate = "FirstPlayDate";
    public const string ProfileID = "ProfileId";
    public const string StartsCount = "StartsCount";
}
