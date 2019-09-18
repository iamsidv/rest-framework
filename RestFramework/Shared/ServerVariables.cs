using UnityEngine;

public class ServerVariables
{
    public const int RESPONSE_SUCCESS = 1;
    public const int RESPONSE_ID_PASS_MATCH_INCORRECT = 231;
    public const int RESPONSE_INVALID_TOKEN = 204;

    public const int RESPONSE_FAILURE = -1;

    public const int PIC_AVATAR = 1;
    public const int PIC_GALLERY = 2;


    public enum Gender
    {
        Male = 1,
        Female = 2,
        Default = 0,
        Other = 3
    }

    public enum FavouriteStatus
    {
        ADD = 1,
        REMOVE = 2
    }


    public enum RewardStatus
    {
        NONE = 0,
        NOT_CLAIMED = 1,
        IN_PROCESS = 2,
        CAN_CLAIM = 3
    }

    public enum PrivacyState
    {
        SELF = 1,
        FRIENDS = 2,
        EVERYONE = 3
    }

    public enum WheelSpinType
    {
        NONE = 0,
        FREE = 1,
        PREMIUM = 2
    }

    //public const string endpoint = "https://api.playcoinkings.com/V1/PHP/rest.php?";
    //public const string baseApiURL = endpoint + "methodName=";

    public const string applicationKey = "applicationKey";
    public const int paramAppKey = 12345;

#if UNITY_EDITOR
    public static string deviceIdentifier = SystemInfo.deviceName +"6";
#elif !UNITY_EDITOR && UNITY_IOS || UNITY_ANDROID
    public static string deviceIdentifier = SystemInfo.deviceUniqueIdentifier;
#elif !UNITY_EDITOR
    public static string deviceIdentifier = "DeviceFive";
#endif

    #region keys
    public const string kType = "type";
    public const string kDeviceToken = "device_token";
    public const string methodName = "methodName";
    public const string kUserID = "user_id";
    public const string kReferralCode = "referral_code";
    public const string kFbID = "facebook_id";
    public const string kName = "user_name";
    public const string kLanguage = "user_language";
    public const string kAccessToken = "access_token";
    public const string kFbAccessToken = "facebook_access_token";
    public const string kFbFriendsId = "fb_friends_id";
    public const string kFbFriendId = "friend_facebook_id";
    public const string kFbFriendsIdList = "facebook_id_list";
    public const string kFbFriendsFbIdList = "friend_facebook_id_list";
    public const string kGiftType = "gift_type";
    public const string kGiftIdList = "gift_id_list";
    public const string kGiftId = "gift_id";
    public const string kLayerID = "layerId";
    public const string kCompletedSeconds = "completed_seconds";
    public const string kLimit = "limit";
    public const string kMasterCountryId = "master_country_id";
    public const string kOtherUserId = "other_user_id";
    public const string kBet = "bet";
    public const string kCoins = "coins";
    public const string kSpin = "spins";
    public const string kEnery = "energy";
    public const string kHordHealth = "horde_health";
    public const string kHordStrength = "horde_strength";
    public const string kHordMood = "horde_mood";
    public const string kUserCountryId = "user_country_id";
    public const string kUserCurrentLevel = "user_country_level";
    public const string kMasterBuildingId = "master_building_id";
    public const string kUserBuildingId = "user_building_id";
    public const string kUserRaidId = "user_raid_id";
    public const string kMaskers = "markers";
    public const string kIap_data = "iap_data";
    public const string kMasterOriginCountryId = "master_origin_country_id";
    public const string kHordeType = "horde_type";
    public const string kLastNotificationId = "last_notification_id";
    public const string kGemsCollectType = "gems_collect_type";
    public const string kAndroidPushToken = "android_push_token";
    public const string kIosPushToken = "ios_push_token";
    public const string kRewardType = "reward_type";
    public const string kAdType = "type";
    public const string kUserBalloonId = "user_baloon_id";
    public const string kMasterIapId = "master_iap_id";
    public const string kEnableNotification = "enable_notification";
    public const string kSpinType = "spin_type";
    public const string kIsTutorial = "is_tutorial";

    #endregion


    #region apiMethods
    public const string apiSaveLayer = "layer.save";
    public const string apiGetLeaderBoard = "leaderboard.get";
    public const string apiUserLogin = "user.login";
    public const string apiFBFriendsLeaderboard = "leaderboard.fbfriends";
    public const string apiUserUpdate = "user.update";
    public const string apiMasterCountry = "master.country";
    public const string apiMasterbet = "master.bet";
    public const string apiCountryChoose = "country.choose";
    public const string apiUserDetail = "user.detail";
    public const string apiSlotSpin = "slot.spin";
    public const string apiSlotSpinCompleted = "slot.spinComplete";
    public const string apiSlotBet = "slot.bet";
    public const string apiUserCountry = "user.country";
    public const string apiUserBuilding= "user.building";
    public const string apiMasterBuilding = "master.building";
    public const string apiBuildingContruct = "building.construct";
    public const string apiBuildingUpgrade = "building.upgrade";
    public const string apiInitiateRaid = "user.initiateRaid";
    public const string apiUserRaid = "user.raid";
    public const string apiBuildingAttack = "building.attack";
    public const string apiBuildingRepair = "building.repair";
    public const string apiMasterDailyReward = "master.dailyReward";
    public const string apiDailyRewardSpin = "dailyReward.spin";
    public const string apiUserAttackRaidList = "user.AttackRaidList";
    public const string apiGemsCollect = "gems.collect";
    public const string apiWorldDominace = "world.dominance";
    public const string apiUserHordeDetails = "user.hordeDetail";
    public const string apiUserHordeUpgrade = "user.hordeUpgrade";
    public const string apiNotificationGet="notification.get";
    public const string apiLeaderboardDetail = "leaderboard.detail";
    public const string apiProgressBarList = "progressbar.list";
	public const string apiIAPMasterList = "master.iap";
    public const string apiIAPPurchase = "iap.purchase";
    public const string apiGiftStatus = "gift.status";
	public const string apiRecieveGift = "gift.receive";
    public const string apiSendGift = "gift.send";
    public const string apiWatchAd = "ad.watch";
    public const string apiBaloonTap = "baloon.tap";
    public const string apilinkFbAccount = "user.linkAccount";
    #endregion

    public const string MatchEmailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
    public const string DefaultYear = "1992";

    //@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
    //+ @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
    //+ @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
    //+ @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
}
