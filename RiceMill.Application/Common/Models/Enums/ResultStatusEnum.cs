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

        #endregion

        #region Concern 500 To 599

        ConcernNotFound = 500,
        ConcernIdIsNotValid,
        ConcernTitleIsNotValid,
        ConcernTitleLengthIsNotValid,
        ConcernUserIdIsNotValid,
        ConcernRiceMillIdIsNotValid,

        #endregion

        #region User 600 To 699

        UserNotFound = 600,
        UserIdIsNotValid,
        UserUsernameIsNotValid,
        UserUsernameLengthIsNotValid,
        UserPasswordIsNotValid,
        UserRoleIsNotValid,
        UserUserPersonIdIsNotValid,
        UserParentUserIdIsNotValid,
        UserRiceMillIdIsNotValid,

        #endregion

        #region RiceMill 700 To 799

        RiceMillNotFound = 700,
        RiceMillIdIsNotValid,
        RiceMillTitleIsNotValid,
        RiceMillTitleLengthIsNotValid,
        RiceMillAddressIsNotValid,
        RiceMillAddressLengthIsNotValid,
        RiceMillWageIsNotValid,
        RiceMillPhoneLengthIsNotValid,
        RiceMillPostalCodeLengthIsNotValid,
        RiceMillDescriptionLengthIsNotValid,
        RiceMillOwnerPersonIdIsNotValid,

        #endregion

        #region UserActivity 800 To 899

        UserActivityUserIdIsNotValid = 800,
        UserActivityIpIsNotValid,
        UserActivityUserActivityTypeIsNotValid,
        UserActivityEntityTypeIsNotValid,
        UserActivityApplicationIdIsNotValid,
        UserActivityBeforeEditIsNotValid,
        UserActivityAfterEditIsNotValid,
        UserActivityRiceMillIdIsNotValid,

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
                {ResultStatusEnum.RiceMillPhoneLengthIsNotValid, "طول شماره تماس کارخانه بیش از حد مجاز است" },
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
            };

        public static string GetErrorMessage(this ResultStatusEnum resultStatus) => ResultStatusMessage[resultStatus];
    }
}