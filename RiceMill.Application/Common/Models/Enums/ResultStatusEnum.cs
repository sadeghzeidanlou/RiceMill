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

        ConcernTitleIsNotValid = 500,

        #endregion
    }

    public class ErrorDictionary
    {
        public static Dictionary<ResultStatusEnum, string> ResultStatusMessage;

        public ErrorDictionary()
        {
            ResultStatusMessage = new Dictionary<ResultStatusEnum, string>()
            {
                #region Genearl
                
                {ResultStatusEnum.Success,"عملیات '{0}' با موفقیت انجام شد" },
                {ResultStatusEnum.Fail,"عملیات '{0}' دچار خطا شده است" },
                {ResultStatusEnum.NotAuthenticated, "احراز هویت بدرستی انجام نشده است" },
                
                #endregion

                #region Concern
                
                {ResultStatusEnum.ConcernTitleIsNotValid, "عنوان دلیل معتبر نمی باشد" }
                
                #endregion
            };
        }
    }
}