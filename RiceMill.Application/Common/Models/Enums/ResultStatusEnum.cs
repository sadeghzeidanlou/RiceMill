namespace RiceMill.Application.Common.Models.Enums
{
    public enum ResultStatusEnum
    {
        #region General 0 To 499

        Success,
        Fail,
        Unauthorized,
        Forbidden,
        NotImplemented,
        DatabaseError,
        UnHandleError,

        #endregion

        #region Concern 500 To 549

        ConcernNotFound = 500,
        ConcernIdIsNotValid,
        ConcernTitleIsNotValid,
        ConcernTitleLengthIsNotValid,
        ConcernUserIdIsNotValid,
        ConcernRiceMillIdIsNotValid,

        #endregion

        #region User 550 To 599

        UserNotFound = 550,
        UserIdIsNotValid,
        UserUsernameIsNotValid,
        UserUsernameLengthIsNotValid,
        UserPasswordIsNotValid,
        UserRoleIsNotValid,
        UserUserPersonIdIsNotValid,
        UserParentUserIdIsNotValid,
        UserRiceMillIdIsNotValid,

        #endregion

        #region RiceMill 600 To 649

        RiceMillNotFound = 600,
        RiceMillIdIsNotValid,
        RiceMillTitleIsNotValid,
        RiceMillTitleLengthIsNotValid,
        RiceMillAddressIsNotValid,
        RiceMillAddressLengthIsNotValid,
        RiceMillWageIsNotValid,
        RiceMillPhoneIsNotValid,
        RiceMillPhoneLengthIsNotValid,
        RiceMillPostalCodeIsNotValid,
        RiceMillPostalCodeLengthIsNotValid,
        RiceMillDescriptionLengthIsNotValid,
        RiceMillOwnerPersonIdIsNotValid,

        #endregion

        #region UserActivity 650 To 699

        UserActivityUserIdIsNotValid = 650,
        UserActivityIpIsNotValid,
        UserActivityUserActivityTypeIsNotValid,
        UserActivityEntityTypeIsNotValid,
        UserActivityApplicationIdIsNotValid,
        UserActivityBeforeEditIsNotValid,
        UserActivityAfterEditIsNotValid,
        UserActivityRiceMillIdIsNotValid,

        #endregion

        #region Vehicle 700 To 749

        VehicleNotFound = 700,
        VehicleIdIsNotValid,
        VehiclePlateIsNotValid,
        VehiclePlateMaximumLengthIsNotValid,
        VehiclePlateMinimumLengthIsNotValid,
        VehicleDescriptionLengthIsNotValid,
        VehicleVehicleTypeIsNotValid,
        VehicleOwnerPersonIdIsNotValid,
        VehicleRiceMillIdIsNotValid,

        #endregion

        #region Village 750 To 799

        VillageNotFound = 750,
        VillageIdIsNotValid,
        VillageTitleIsNotValid,
        VillageTitleLengthIsNotValid,
        VillageUserIdIsNotValid,
        VillageRiceMillIdIsNotValid,

        #endregion

        #region Person 800 To 849

        PersonNotFound = 800,
        PersonIdIsNotValid,
        PersonNameIsNotValid,
        PersonNameLengthIsNotValid,
        PersonFamilyIsNotValid,
        PersonFamilyLengthIsNotValid,
        PersonGenderIsNotValid,
        PersonMobileNumberIsNotValid,
        PersonMobileNumberLengthIsNotValid,
        PersonHomeNumberIsNotValid,
        PersonHomeNumberLengthIsNotValid,
        PersonAddressIsNotValid,
        PersonAddressLengthIsNotValid,
        PersonFatherNameIsNotValid,
        PersonFatherNameLengthIsNotValid,
        PersonRiceMillIdIsNotValid,

        #endregion

    }

    public static class ErrorDictionary
    {
        private static readonly Dictionary<ResultStatusEnum, string> ResultStatusMessage = new()
            {
                #region Genearl
                
                {ResultStatusEnum.Success,"عملیات '{0}' با موفقیت انجام شد" },
                {ResultStatusEnum.Fail,"عملیات '{0}' دچار خطا شده است" },
                {ResultStatusEnum.Unauthorized, "احراز هویت بدرستی انجام نشده است" },
                {ResultStatusEnum.Forbidden, "شما مجاز به ادامه عملیات نمی باشد" },
                {ResultStatusEnum.NotImplemented, "درحال حاضر عملیات مورد نظر در سیستم پشتیبانی نمی شود" },
                {ResultStatusEnum.DatabaseError, "خطایی در سمت پایگاه داده رخ داده است" },
                {ResultStatusEnum.UnHandleError, "خطای پیش بینی نشده اتفاق افتاده است" },
                
                #endregion

                #region Concern
                
                {ResultStatusEnum.ConcernNotFound, "دلیل یافت نشد یا شما مجاز به دسترسی نمی باشید" },
                {ResultStatusEnum.ConcernIdIsNotValid, "شناسه دلیل معتبر نمی باشد" },
                {ResultStatusEnum.ConcernTitleIsNotValid, "عنوان دلیل معتبر نمی باشد" },
                {ResultStatusEnum.ConcernTitleLengthIsNotValid, "طول عنوان دلیل بیش از حد مجاز است" },
                {ResultStatusEnum.ConcernUserIdIsNotValid, "شناسه کاربر برای این دلیل معتبر نمی باشد" },
                {ResultStatusEnum.ConcernRiceMillIdIsNotValid, "شناسه کارخانه برای این دلیل معتبر نمی باشد" },

                #endregion

                #region User

                {ResultStatusEnum.UserNotFound, "کاربر یافت نشد یا شما مجاز به دسترسی نمی باشید" },
                {ResultStatusEnum.UserIdIsNotValid, "شناسه کاربر معتبر نمی باشد" },
                {ResultStatusEnum.UserUsernameIsNotValid, "نام کاربری کاربر معتبر نمی باشد" },
                {ResultStatusEnum.UserUsernameLengthIsNotValid, "طول نام کاربری کاربر بیش از حد مجاز است" },
                {ResultStatusEnum.UserPasswordIsNotValid, "رمز عبور کاربر معتبر نمی باشد" },
                {ResultStatusEnum.UserRoleIsNotValid, "نقش کاربر معتبر نمی باشد" },
                {ResultStatusEnum.UserUserPersonIdIsNotValid, "شناسه فردی کاربر معتبر نمی باشد" },
                {ResultStatusEnum.UserParentUserIdIsNotValid, "شناسه کاربری ایجاد کننده کاربر معتبر نمی باشد" },
                {ResultStatusEnum.UserRiceMillIdIsNotValid, "شناسه کارخانه کاربر معتبر نمی باشد" },

                #endregion

                #region RiceMill

                {ResultStatusEnum.RiceMillNotFound, "کارخانه یافت نشد یا شما مجاز به دسترسی نمی باشید" },
                {ResultStatusEnum.RiceMillIdIsNotValid, "شناسه کارخانه معتبر نمی باشد" },
                {ResultStatusEnum.RiceMillTitleIsNotValid, "عنوان کارخانه معتبر نمی باشد" },
                {ResultStatusEnum.RiceMillTitleLengthIsNotValid, "طول عنوان کارخانه بیش از حد مجاز است" },
                {ResultStatusEnum.RiceMillAddressIsNotValid, "آدرس کارخانه معتبر نمی باشد" },
                {ResultStatusEnum.RiceMillAddressLengthIsNotValid, "طول آدرس کارخانه بیش از حد مجاز است" },
                {ResultStatusEnum.RiceMillWageIsNotValid, "مقدار کارمزد کارخانه معتبر نمی باشد" },
                {ResultStatusEnum.RiceMillPhoneIsNotValid, "شماره تماس کارخانه معتبر نمی باشد" },
                {ResultStatusEnum.RiceMillPhoneLengthIsNotValid, "طول شماره تماس کارخانه بیش از حد مجاز است" },
                {ResultStatusEnum.RiceMillPostalCodeIsNotValid, "کد پستی کارخانه معتبر نمی باشد" },
                {ResultStatusEnum.RiceMillPostalCodeLengthIsNotValid, "طول کد پستی کارخانه بیش از حد مجاز است" },
                {ResultStatusEnum.RiceMillDescriptionLengthIsNotValid, "طول توضیحات کارخانه بیش از حد مجاز است" },
                {ResultStatusEnum.RiceMillOwnerPersonIdIsNotValid, "شناسه صاحب کارخانه معتبر نمی باشد" },

                #endregion

                #region User

                {ResultStatusEnum.UserActivityUserIdIsNotValid, "شناسه کاربر معتبر نمی باشد" },
                {ResultStatusEnum.UserActivityIpIsNotValid, "آی پی کاربر معتبر نمی باشد" },
                {ResultStatusEnum.UserActivityUserActivityTypeIsNotValid, "نوع فعالیت کاربر معتبر نمی باشد" },
                {ResultStatusEnum.UserActivityEntityTypeIsNotValid, "نوع آبجکت معتبر نمی باشد" },
                {ResultStatusEnum.UserActivityApplicationIdIsNotValid, "برنامه استفاده شده معتبر نمی باشد" },
                {ResultStatusEnum.UserActivityBeforeEditIsNotValid, "اطلاعات قبل از تغییر معتبر نمی باشد" },
                {ResultStatusEnum.UserActivityAfterEditIsNotValid, "اطلاعات بعد از تغییر معتبر نمی باشد" },
                {ResultStatusEnum.UserActivityRiceMillIdIsNotValid, "شناسه کارخانه معتبر نمی باشد" },

                #endregion

                #region Vehicle

                {ResultStatusEnum.VehicleNotFound, "وسیله نقلیه یافت نشد یا شما مجاز به دسترسی نمی باشید" },
                {ResultStatusEnum.VehicleIdIsNotValid, "شناسه وسیله نقلیه معتبر نمی باشد" },
                {ResultStatusEnum.VehiclePlateIsNotValid, "پلاک وسیله نقلیه معتبر نمی باشد" },
                {ResultStatusEnum.VehiclePlateMaximumLengthIsNotValid, "طول پلاک وسیله نقلیه بیش از حد مجاز است" },
                {ResultStatusEnum.VehiclePlateMinimumLengthIsNotValid, "طول پلاک وسیله نقلیه کمتر از حد مجاز است" },
                {ResultStatusEnum.VehicleDescriptionLengthIsNotValid, "طول توضیحات وسیله نقلیه بیش از حد مجاز است" },
                {ResultStatusEnum.VehicleVehicleTypeIsNotValid, "نوع وسیله نقلیه معتبر نمی باشد" },
                {ResultStatusEnum.VehicleOwnerPersonIdIsNotValid, "شناسه صاحب وسیله نقلیه معتبر نمی باشد" },
                {ResultStatusEnum.VehicleRiceMillIdIsNotValid, "شناسه کارخانه معتبر نمی باشد" },

                #endregion

                #region Village
                
                {ResultStatusEnum.VillageNotFound, "مبدا یافت نشد یا شما مجاز به دسترسی نمی باشید" },
                {ResultStatusEnum.VillageIdIsNotValid, "شناسه مبدا معتبر نمی باشد" },
                {ResultStatusEnum.VillageTitleIsNotValid, "عنوان مبدا معتبر نمی باشد" },
                {ResultStatusEnum.VillageTitleLengthIsNotValid, "طول عنوان مبدا بیش از حد مجاز است" },
                {ResultStatusEnum.VillageUserIdIsNotValid, "شناسه کاربر برای این مبدا معتبر نمی باشد" },
                {ResultStatusEnum.VillageRiceMillIdIsNotValid, "شناسه کارخانه برای این مبدا معتبر نمی باشد" },

                #endregion

                #region Person

                {ResultStatusEnum.PersonNotFound, "فرد یافت نشد یا شما مجاز به دسترسی نمی باشید" },
                {ResultStatusEnum.PersonIdIsNotValid, "شناسه فرد معتبر نمی باشد" },
                {ResultStatusEnum.PersonNameIsNotValid, "نام فرد معتبر نمی باشد" },
                {ResultStatusEnum.PersonNameLengthIsNotValid, "طول نام فرد بیش از حد مجاز است" },
                {ResultStatusEnum.PersonFamilyIsNotValid, "نام خانوادگی فرد معتبر نمی باشد" },
                {ResultStatusEnum.PersonFamilyLengthIsNotValid, "طول نام خانوادگی فرد بیش از حد مجاز است" },
                {ResultStatusEnum.PersonGenderIsNotValid, "جنسیت فرد معتبر نمی باشد" },
                {ResultStatusEnum.PersonMobileNumberIsNotValid, "شماره موبایل فرد معتبر نمی باشد" },
                {ResultStatusEnum.PersonMobileNumberLengthIsNotValid, "طول شماره موبایل فرد بیش از حد مجاز است" },
                {ResultStatusEnum.PersonHomeNumberIsNotValid, "شماره منزل فرد معتبر نمی باشد" },
                {ResultStatusEnum.PersonHomeNumberLengthIsNotValid, "طول شماره منزل فرد بیش از حد مجاز است" },
                {ResultStatusEnum.PersonAddressIsNotValid, "آدرس فرد معتبر نمی باشد" },
                {ResultStatusEnum.PersonAddressLengthIsNotValid, "طول آدرس فرد بیش از حد مجاز است" },
                {ResultStatusEnum.PersonFatherNameIsNotValid, "نام پدر فرد معتبر نمی باشد" },
                {ResultStatusEnum.PersonFatherNameLengthIsNotValid, "طول نام پدر فرد بیش از حد مجاز است" },
                {ResultStatusEnum.PersonRiceMillIdIsNotValid, "شناسه کارخانه معتبر نمی باشد" },

                #endregion
            };

        public static string GetErrorMessage(this ResultStatusEnum resultStatus) => ResultStatusMessage[resultStatus];
    }
}