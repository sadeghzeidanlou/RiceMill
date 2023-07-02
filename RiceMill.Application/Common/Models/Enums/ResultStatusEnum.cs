namespace RiceMill.Application.Common.Models.Enums
{
    public enum ResultStatusEnum
    {
        #region General 1 To 499

        Success = 1,
        Fail,
        NotAuthenticated,

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

        RiceMillIdIsNotValid = 700,

        #endregion
    }

    public class ErrorDictionary
    {
        private static Dictionary<ResultStatusEnum, string> ResultStatusMessage = new()
            {
                #region Genearl
                
                {ResultStatusEnum.Success,"عملیات '{0}' با موفقیت انجام شد" },
                {ResultStatusEnum.Fail,"عملیات '{0}' دچار خطا شده است" },
                {ResultStatusEnum.NotAuthenticated, "احراز هویت بدرستی انجام نشده است" },
                
                #endregion

                #region Concern
                
                {ResultStatusEnum.ConcernNotFound, "دلیل یافت نشد یا شما مجاز به دسترسی نمی باشید" },
                {ResultStatusEnum.ConcernIdIsNotValid, "شناسه دلیل، معتبر نمی باشد" },
                {ResultStatusEnum.ConcernTitleIsNotValid, "عنوان دلیل، معتبر نمی باشد" },
                {ResultStatusEnum.ConcernTitleLengthIsNotValid, "طول عنوان دلیل بیش از حد مجاز است" },
                {ResultStatusEnum.ConcernUserIdIsNotValid, "شناسه کاربر دلیل، معتبر نمی باشد" },
                {ResultStatusEnum.ConcernRiceMillIdIsNotValid, "شناسه کارخانه دلیل، معتبر نمی باشد" },

                #endregion

                #region User

                {ResultStatusEnum.UserIdIsNotValid, "شناسه کاربر معتبر نمی باشد" },

                #endregion

                #region RiceMill

                {ResultStatusEnum.RiceMillIdIsNotValid, "شناسه کارخانه معتبر نمی باشد" },
                
                #endregion
            };

        public static string GetErrorMessage(ResultStatusEnum resultStatus) => ResultStatusMessage[resultStatus];
    }
}