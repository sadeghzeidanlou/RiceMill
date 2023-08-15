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

        #endregion

        #region User 550 To 599

        UserNotFound = 550,
        UserIdIsNotValid,
        UserUsernameIsNotValid,
        UserUsernameLengthIsNotValid,
        UserPasswordIsNotValid,
        UserRoleIsNotValid,
        UserUserPersonIdIsNotValid,

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

        #endregion

        #region Village 750 To 799

        VillageNotFound = 750,
        VillageIdIsNotValid,
        VillageTitleIsNotValid,
        VillageTitleLengthIsNotValid,

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
        PersonNoticesTypeIsNotValid,
        PersonAddressIsNotValid,
        PersonAddressLengthIsNotValid,
        PersonFatherNameIsNotValid,
        PersonFatherNameLengthIsNotValid,

        #endregion

        #region Dryer 850 To 899

        DryerNotFound = 850,
        DryerIdIsNotValid,
        DryerTitleIsNotValid,
        DryerTitleLengthIsNotValid,

        #endregion

        #region Payment 900 To 949

        PaymentNotFound = 900,
        PaymentIdIsNotValid,
        PaymentPaymentTimeIsNotValid,
        PaymentUnbrokenRiceIsNotValid,
        PaymentBrokenRiceIsNotValid,
        PaymentFlourIsNotValid,
        PaymentMoneyIsNotValid,
        PaymentDescriptionLengthIsNotValid,
        PaymentPaidPersonIdIsNotValid,

        #endregion

        #region InputLoad 950 To 999

        InputLoadNotFound = 950,
        InputLoadIdIsNotValid,
        InputLoadNumberOfBagsIsNotValid,
        InputLoadNumberOfBagsInDryerIsNotValid,
        InputLoadDescriptionLengthIsNotValid,
        InputLoadReceiveTimeIsNotValid,
        InputLoadVillageIdIsNotValid,
        InputLoadDelivererPersonIdIsNotValid,
        InputLoadDelivererPersonNotFound,
        InputLoadReceiverPersonIdIsNotValid,
        InputLoadReceiverPersonNotFound,
        InputLoadCarrierPersonIdIsNotValid,
        InputLoadCarrierPersonNotFound,
        InputLoadOwnerPersonIdIsNotValid,
        InputLoadOwnerPersonNotFound,

        #endregion

        #region DryerHistory 1000 To 1049

        DryerHistoryNotFound = 1000,
        DryerHistoryIdIsNotValid,
        DryerHistoryOperationIsNotValid,
        DryerHistoryStartTimeIsNotValid,
        DryerHistoryStopTimeIsNotValid,

        #endregion

        #region RiceThreshing 1050 To 1099

        RiceThreshingNotFound = 1050,
        RiceThreshingIdIsNotValid,

        #endregion

        #region Income 1100 To 1149

        IncomeNotFound = 1100,
        IncomeIdIsNotValid,

        #endregion

        #region Delivery 1150 To 1199

        DeliveryNotFound = 1150,
        DeliveryIdIsNotValid,

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

                #endregion

                #region User

                {ResultStatusEnum.UserNotFound, "کاربر یافت نشد یا شما مجاز به دسترسی نمی باشید" },
                {ResultStatusEnum.UserIdIsNotValid, "شناسه کاربر معتبر نمی باشد" },
                {ResultStatusEnum.UserUsernameIsNotValid, "نام کاربری کاربر معتبر نمی باشد" },
                {ResultStatusEnum.UserUsernameLengthIsNotValid, "طول نام کاربری کاربر بیش از حد مجاز است" },
                {ResultStatusEnum.UserPasswordIsNotValid, "رمز عبور کاربر معتبر نمی باشد" },
                {ResultStatusEnum.UserRoleIsNotValid, "نقش کاربر معتبر نمی باشد" },
                {ResultStatusEnum.UserUserPersonIdIsNotValid, "شناسه فردی برای کاربر معتبر نمی باشد" },

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

                #region UserActivity

                {ResultStatusEnum.UserActivityUserIdIsNotValid, "شناسه کاربر برای فعالیت کاربر معتبر نمی باشد" },
                {ResultStatusEnum.UserActivityIpIsNotValid, "آی پی کاربر معتبر نمی باشد" },
                {ResultStatusEnum.UserActivityUserActivityTypeIsNotValid, "نوع فعالیت کاربر معتبر نمی باشد" },
                {ResultStatusEnum.UserActivityEntityTypeIsNotValid, "نوع آبجکت معتبر نمی باشد" },
                {ResultStatusEnum.UserActivityApplicationIdIsNotValid, "برنامه استفاده شده معتبر نمی باشد" },
                {ResultStatusEnum.UserActivityBeforeEditIsNotValid, "اطلاعات قبل از تغییر معتبر نمی باشد" },
                {ResultStatusEnum.UserActivityAfterEditIsNotValid, "اطلاعات بعد از تغییر معتبر نمی باشد" },

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

                #endregion

                #region Village
                
                {ResultStatusEnum.VillageNotFound, "مبدا یافت نشد یا شما مجاز به دسترسی نمی باشید" },
                {ResultStatusEnum.VillageIdIsNotValid, "شناسه مبدا معتبر نمی باشد" },
                {ResultStatusEnum.VillageTitleIsNotValid, "عنوان مبدا معتبر نمی باشد" },
                {ResultStatusEnum.VillageTitleLengthIsNotValid, "طول عنوان مبدا بیش از حد مجاز است" },

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
                {ResultStatusEnum.PersonNoticesTypeIsNotValid, "نحوه اطلاع رسانی به فرد معتبر نمی باشد" },
                {ResultStatusEnum.PersonAddressIsNotValid, "آدرس فرد معتبر نمی باشد" },
                {ResultStatusEnum.PersonAddressLengthIsNotValid, "طول آدرس فرد بیش از حد مجاز است" },
                {ResultStatusEnum.PersonFatherNameIsNotValid, "نام پدر فرد معتبر نمی باشد" },
                {ResultStatusEnum.PersonFatherNameLengthIsNotValid, "طول نام پدر فرد بیش از حد مجاز است" },

                #endregion

                #region Dryer
                
                {ResultStatusEnum.DryerNotFound, "خشک کن یافت نشد یا شما مجاز به دسترسی نمی باشید" },
                {ResultStatusEnum.DryerIdIsNotValid, "شناسه خشک کن معتبر نمی باشد" },
                {ResultStatusEnum.DryerTitleIsNotValid, "عنوان خشک کن معتبر نمی باشد" },
                {ResultStatusEnum.DryerTitleLengthIsNotValid, "طول عنوان خشک کن بیش از حد مجاز است" },

                #endregion

                #region Payment

                {ResultStatusEnum.PaymentNotFound, "پرداخت یافت نشد یا شما مجاز به دسترسی نمی باشید" },
                {ResultStatusEnum.PaymentIdIsNotValid, "شناسه پرداخت معتبر نمی باشد" },
                {ResultStatusEnum.PaymentPaymentTimeIsNotValid, "زمان پرداخت معتبر نمی باشد" },
                {ResultStatusEnum.PaymentUnbrokenRiceIsNotValid, "مقدار برنج سالم برای پرداخت معتبر نمی باشد" },
                {ResultStatusEnum.PaymentBrokenRiceIsNotValid, "مقدار برنج شکسته برای پرداخت معتبر نمی باشد" },
                {ResultStatusEnum.PaymentFlourIsNotValid, "مقدار آرد برنج برای پرداخت معتبر نمی باشد" },
                {ResultStatusEnum.PaymentMoneyIsNotValid, "مقدار پول برای پرداخت معتبر نمی باشد" },
                {ResultStatusEnum.PaymentDescriptionLengthIsNotValid, "طول توضیحات پرداخت معتبر نمی باشد" },
                {ResultStatusEnum.PaymentPaidPersonIdIsNotValid, "شناسه شخص دریافت کننده معتبر نمی باشد" },

                #endregion

                #region InputLoad

                {ResultStatusEnum.InputLoadNotFound, "بار ورودی یافت نشد یا شما مجاز به دسترسی نمی باشید" },
                {ResultStatusEnum.InputLoadIdIsNotValid, "شناسه بار ورودی معتبر نمی باشد" },
                {ResultStatusEnum.InputLoadNumberOfBagsIsNotValid, "تعداد کیسه های بار ورودی معتبر نمی باشد" },
                {ResultStatusEnum.InputLoadNumberOfBagsInDryerIsNotValid, "تعداد کیسه های داخل خشک کن معتبر نمی باشد" },
                {ResultStatusEnum.InputLoadDescriptionLengthIsNotValid, "طول توضیحات بار ورودی معتبر نمی باشد" },
                {ResultStatusEnum.InputLoadReceiveTimeIsNotValid, "زمان دریافت بار ورودی معتبر نمی باشد" },
                {ResultStatusEnum.InputLoadDelivererPersonIdIsNotValid, "شناسه تحویل دهنده بار ورودی معتبر نمی باشد" },
                {ResultStatusEnum.InputLoadDelivererPersonNotFound, "تحویل دهنده بار ورودی یافت نشد یا شما مجاز به دسترسی نیستید" },

                {ResultStatusEnum.InputLoadReceiverPersonIdIsNotValid, "شناسه دریافت کننده بار ورودی معتبر نمی باشد" },
                {ResultStatusEnum.InputLoadReceiverPersonNotFound, "دریافت کننده بار ورودی یافت نشد یا شما مجاز به دسترسی نیستید" },

                {ResultStatusEnum.InputLoadCarrierPersonIdIsNotValid, "شناسه حمل کننده بار ورودی معتبر نمی باشد" },
                {ResultStatusEnum.InputLoadCarrierPersonNotFound, "حمل کننده بار ورودی یافت نشد یا شما مجاز به دسترسی نیستید" },

                {ResultStatusEnum.InputLoadOwnerPersonIdIsNotValid, "شناسه صاحب بار ورودی معتبر نمی باشد" },
                {ResultStatusEnum.InputLoadOwnerPersonNotFound, "صاحب بار ورودی یافت نشد یا شما مجاز به دسترسی نیستید" },

                #endregion

                #region DryerHistory

                {ResultStatusEnum.DryerHistoryNotFound, "سابقه خشک کن یافت نشد یا شما مجاز به دسترسی نمی باشید" },
                {ResultStatusEnum.DryerHistoryIdIsNotValid, "شناسه سابقه خشک کن معتبر نمی باشد" },
                {ResultStatusEnum.DryerHistoryOperationIsNotValid, "عملیات خشک کن معتبر نمی باشد" },
                {ResultStatusEnum.DryerHistoryStartTimeIsNotValid, "زمان شروع عملیات خشک کن معتبر نمی باشد" },
                {ResultStatusEnum.DryerHistoryStopTimeIsNotValid, "زمان پایان عملیات خشک کن معتبر نمی باشد" },

                #endregion

                #region RiceThreshing

                {ResultStatusEnum.RiceThreshingNotFound, "شالیکوبی یافت نشد یا شما مجاز به دسترسی نمی باشید" },
                {ResultStatusEnum.RiceThreshingIdIsNotValid, "شناسه شالیکوبی معتبر نمی باشد" },

                #endregion

                #region Income

                {ResultStatusEnum.IncomeNotFound, "درآمد یافت نشد یا شما مجاز به دسترسی نمی باشید" },
                {ResultStatusEnum.IncomeIdIsNotValid, "شناسه درآمد معتبر نمی باشد" },

                #endregion

                #region Delivery

                {ResultStatusEnum.DeliveryNotFound, "تحویل یافت نشد یا شما مجاز به دسترسی نمی باشید" },
                {ResultStatusEnum.DeliveryIdIsNotValid, "شناسه تحویل معتبر نمی باشد" },

                #endregion
            };

        public static string GetErrorMessage(this ResultStatusEnum resultStatus) => ResultStatusMessage[resultStatus];
    }
}