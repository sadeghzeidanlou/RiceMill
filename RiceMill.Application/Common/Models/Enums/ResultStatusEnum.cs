namespace RiceMill.Application.Common.Models.Enums
{
    public enum ResultStatusEnum
    {
        #region General 0 To 499

        Success,
        Fail,
        Unauthorized,
        Forbidden,

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

        UserIdIsNotValid = 600,

        #endregion

        #region RiceMill 700 To 799

        RiceMillNotFound = 700,
        RiceMillIdIsNotValid,
        RiceMillTitleIsNotValid,
        RiceMillTitleLengthIsNotValid,
        RiceMillAddressIsNotValid,
        RiceMillAddressLengthIsNotValid,
        RiceMillWageValueIsNotValid,
        RiceMillPhoneLengthIsNotValid,
        RiceMillPostalCodeLengthIsNotValid,
        RiceMillDescriptionLengthIsNotValid,
        RiceMillOwnerPersonIdIsNotValid,

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
                {ResultStatusEnum.Forbidden, "شما مجاز به دسترسی نمی باشد" },
                
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

                {ResultStatusEnum.UserIdIsNotValid, "شناسه کاربر معتبر نمی باشد" },

                #endregion

                #region RiceMill

                {ResultStatusEnum.RiceMillNotFound, "کارخانه یافت نشد یا شما مجاز به دسترسی نمی باشید" },
                {ResultStatusEnum.RiceMillIdIsNotValid, "شناسه کارخانه معتبر نمی باشد" },
                {ResultStatusEnum.RiceMillTitleIsNotValid, "نام کارخانه معتبر نمی باشد" },
                {ResultStatusEnum.RiceMillTitleLengthIsNotValid, "طول نام کارخانه بیش از حد مجاز است" },
                {ResultStatusEnum.RiceMillAddressIsNotValid, "آدرس کارخانه معتبر نمی باشد" },
                {ResultStatusEnum.RiceMillAddressLengthIsNotValid, "طول آدرس کارخانه بیش از حد مجاز است" },
                {ResultStatusEnum.RiceMillWageValueIsNotValid, "مقدار کارمزد کارخانه معتبر نمی باشد" },
                {ResultStatusEnum.RiceMillPhoneLengthIsNotValid, "طول شماره تماس کارخانه بیش از حد مجاز است" },
                {ResultStatusEnum.RiceMillPostalCodeLengthIsNotValid, "طول کد پستی کارخانه بیش از حد مجاز است" },
                {ResultStatusEnum.RiceMillDescriptionLengthIsNotValid, "طول توضیحات کارخانه بیش از حد مجاز است" },
                {ResultStatusEnum.RiceMillOwnerPersonIdIsNotValid, "شناسه صاحب کارخانه معتبر نمی باشد" },

                #endregion
            };

        public static string GetErrorMessage(this ResultStatusEnum resultStatus) => ResultStatusMessage[resultStatus];
    }
}