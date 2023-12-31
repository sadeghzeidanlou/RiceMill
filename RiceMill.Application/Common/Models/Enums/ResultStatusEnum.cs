﻿namespace RiceMill.Application.Common.Models.Enums
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
        Alert,
        GoBack,
        NoInternetAccess,

        #endregion

        #region Concern 500 To 549

        ConcernNotFound = 500,
        ConcernIdIsNotValid,
        ConcernTitleIsNotValid,
        ConcernTitleLengthIsNotValid,
        PleaseSelectConcern,

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
        PleaseSelectRiceMill,

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
        PleaseSelectVehicle,

        #endregion

        #region Village 750 To 799

        VillageNotFound = 750,
        VillageIdIsNotValid,
        VillageTitleIsNotValid,
        VillageTitleLengthIsNotValid,
        PleaseSelectVillage,

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
        PleaseSelectPerson,
        PersonMobileNumberIsDuplicate,

        #endregion

        #region Dryer 850 To 899

        DryerNotFound = 850,
        DryerIdIsNotValid,
        DryerTitleIsNotValid,
        DryerTitleLengthIsNotValid,
        PleaseSelectDryer,

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
        PleaseSelectPayment,
        PaymentPaidCostIsNotValid,

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
        PleaseSelectInputLoad,

        #endregion

        #region DryerHistory 1000 To 1049

        DryerHistoryNotFound = 1000,
        DryerHistoryIdIsNotValid,
        DryerHistoryOperationIsNotValid,
        DryerHistoryStartTimeIsNotValid,
        DryerHistoryStopTimeIsNotValid,
        PleaseSelectDryerHistory,
        DryerHistoryNumberOfBagIsNotValid,

        #endregion

        #region RiceThreshing 1050 To 1099

        RiceThreshingNotFound = 1050,
        RiceThreshingIdIsNotValid,
        RiceThreshingStartTimeIsNotValid,
        RiceThreshingEndTimeIsNotValid,
        RiceThreshingUnbrokenRiceIsNotValid,
        RiceThreshingBrokenRiceIsNotValid,
        RiceThreshingChickenRiceIsNotValid,
        RiceThreshingFlourIsNotValid,
        RiceThreshingDescriptionLengthIsNotValid,
        PleaseSelectRiceThreshing,
        RiceThreshingEndTimeShouldGreaterThanStartTime,

        #endregion

        #region Income 1100 To 1149

        IncomeNotFound = 1100,
        IncomeIdIsNotValid,
        IncomeIncomeTimeIsNotValid,
        IncomeUnbrokenRiceIsNotValid,
        IncomeBrokenRiceIsNotValid,
        IncomeFlourIsNotValid,
        IncomeDescriptionLengthIsNotValid,
        PleaseSelectIncome,
        IncomeValueIsNotValid,

        #endregion

        #region Delivery 1150 To 1199

        DeliveryNotFound = 1150,
        DeliveryIdIsNotValid,
        DeliveryDeliveryTimeIsNotValid,
        DeliveryUnbrokenRiceIsNotValid,
        DeliveryBrokenRiceIsNotValid,
        DeliveryChickenRiceIsNotValid,
        DeliveryFlourIsNotValid,
        DeliveryDescriptionLengthIsNotValid,
        DeliveryDelivererPersonIdIsNotValid,
        DeliveryDelivererPersonNotFound,
        DeliveryReceiverPersonIdIsNotValid,
        DeliveryReceiverPersonNotFound,
        DeliveryCarrierPersonIdIsNotValid,
        DeliveryCarrierPersonNotFound,
        PleaseSelectDelivery,
        DeliveryDeliveryValueIsNotValid,

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
                {ResultStatusEnum.Forbidden, "شما دسترسی لازم برای انجام اینکار را ندارید" },
                {ResultStatusEnum.NotImplemented, "درحال حاضر عملیات مورد نظر در سیستم پشتیبانی نمی شود" },
                {ResultStatusEnum.DatabaseError, "خطایی در سمت پایگاه داده رخ داده است" },
                {ResultStatusEnum.UnHandleError, "خطای پیش بینی نشده اتفاق افتاده است" },
                {ResultStatusEnum.Alert, "هشدار" },
                {ResultStatusEnum.GoBack, "بازگشت" },
                {ResultStatusEnum.NoInternetAccess, "لطفا ارتباط اینترنت خود را بررسی کنید" },
                
                #endregion

                #region Concern
                
                {ResultStatusEnum.ConcernNotFound, "دلیل پرداخت یافت نشد یا شما مجاز به دسترسی نمی باشید" },
                {ResultStatusEnum.ConcernIdIsNotValid, "شناسه دلیل پرداخت معتبر نمی باشد" },
                {ResultStatusEnum.ConcernTitleIsNotValid, "عنوان دلیل پرداخت معتبر نمی باشد" },
                {ResultStatusEnum.ConcernTitleLengthIsNotValid, "طول عنوان دلیل پرداخت بیش از حد مجاز است" },
                {ResultStatusEnum.PleaseSelectConcern, "لطفا یک دلیل پرداخت را انتخاب کنید" },

                #endregion

                #region User

                {ResultStatusEnum.UserNotFound, "کاربر یافت نشد یا شما مجاز به دسترسی نمی باشید" },
                {ResultStatusEnum.UserIdIsNotValid, "شناسه کاربر معتبر نمی باشد" },
                {ResultStatusEnum.UserUsernameIsNotValid, "نام کاربری معتبر نمی باشد" },
                {ResultStatusEnum.UserUsernameLengthIsNotValid, "طول نام کاربری کاربر بیش از حد مجاز است" },
                {ResultStatusEnum.UserPasswordIsNotValid, "رمز عبور معتبر نمی باشد" },
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
                {ResultStatusEnum.PleaseSelectRiceMill, "لطفا یک کارخانه را انتخاب کنید" },
                
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
                {ResultStatusEnum.PleaseSelectVehicle, "لطفا یک وسیله نقلیه را انتخاب کنید" },

                #endregion

                #region Village
                
                {ResultStatusEnum.VillageNotFound, "مبدا یافت نشد یا شما مجاز به دسترسی نمی باشید" },
                {ResultStatusEnum.VillageIdIsNotValid, "شناسه مبدا معتبر نمی باشد" },
                {ResultStatusEnum.VillageTitleIsNotValid, "عنوان مبدا معتبر نمی باشد" },
                {ResultStatusEnum.VillageTitleLengthIsNotValid, "طول عنوان مبدا بیش از حد مجاز است" },
                {ResultStatusEnum.PleaseSelectVillage, "لطفا یک مبدا را انتخاب کنید" },

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
                {ResultStatusEnum.PleaseSelectPerson, "لطفا یک نفر را انتخاب کنید" },
                {ResultStatusEnum.PersonMobileNumberIsDuplicate, "شماره موبایل فرد تکراری است" },

                #endregion

                #region Dryer
                
                {ResultStatusEnum.DryerNotFound, "خشک کن یافت نشد یا شما مجاز به دسترسی نمی باشید" },
                {ResultStatusEnum.DryerIdIsNotValid, "شناسه خشک کن معتبر نمی باشد" },
                {ResultStatusEnum.DryerTitleIsNotValid, "عنوان خشک کن معتبر نمی باشد" },
                {ResultStatusEnum.DryerTitleLengthIsNotValid, "طول عنوان خشک کن بیش از حد مجاز است" },
                {ResultStatusEnum.PleaseSelectDryer, "لطفا یک خشک کن را انتخاب کنید" },

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
                {ResultStatusEnum.PaymentPaidPersonIdIsNotValid, "شناسه فرد دریافت کننده معتبر نمی باشد" },
                {ResultStatusEnum.PleaseSelectPayment, "لطفا یک پرداخت را انتخاب کنید" },
                {ResultStatusEnum.PaymentPaidCostIsNotValid, "هزینه پرداختی معتبر نمی باشد" },

                #endregion

                #region InputLoad

                {ResultStatusEnum.InputLoadNotFound, "بار ورودی یافت نشد یا شما مجاز به دسترسی نمی باشید" },
                {ResultStatusEnum.InputLoadIdIsNotValid, "شناسه بار ورودی معتبر نمی باشد" },
                {ResultStatusEnum.InputLoadNumberOfBagsIsNotValid, "تعداد کیسه های بار ورودی معتبر نمی باشد" },
                {ResultStatusEnum.InputLoadNumberOfBagsInDryerIsNotValid, "تعداد کیسه های داخل خشک کن معتبر نمی باشد" },
                {ResultStatusEnum.InputLoadDescriptionLengthIsNotValid, "طول توضیحات بار ورودی معتبر نمی باشد" },
                {ResultStatusEnum.InputLoadReceiveTimeIsNotValid, "زمان دریافت بار ورودی معتبر نمی باشد" },
                {ResultStatusEnum.InputLoadDelivererPersonIdIsNotValid, "شناسه تحویل دهنده بار ورودی معتبر نمی باشد" },
                {ResultStatusEnum.InputLoadDelivererPersonNotFound, "فرد تحویل دهنده بار ورودی یافت نشد یا شما مجاز به دسترسی نیستید" },
                {ResultStatusEnum.InputLoadReceiverPersonIdIsNotValid, "شناسه دریافت کننده بار ورودی معتبر نمی باشد" },
                {ResultStatusEnum.InputLoadReceiverPersonNotFound, "فرد دریافت کننده بار ورودی یافت نشد یا شما مجاز به دسترسی نیستید" },
                {ResultStatusEnum.InputLoadCarrierPersonIdIsNotValid, "شناسه حمل کننده بار ورودی معتبر نمی باشد" },
                {ResultStatusEnum.InputLoadCarrierPersonNotFound, "فرد حمل کننده بار ورودی یافت نشد یا شما مجاز به دسترسی نیستید" },
                {ResultStatusEnum.InputLoadOwnerPersonIdIsNotValid, "شناسه صاحب بار ورودی معتبر نمی باشد" },
                {ResultStatusEnum.InputLoadOwnerPersonNotFound, "صاحب بار ورودی یافت نشد یا شما مجاز به دسترسی نیستید" },
                {ResultStatusEnum.PleaseSelectInputLoad, "لطفا یک بار ورودی را انتخاب کنید" },

                #endregion

                #region DryerHistory

                {ResultStatusEnum.DryerHistoryNotFound, "سابقه خشک کن یافت نشد یا شما مجاز به دسترسی نمی باشید" },
                {ResultStatusEnum.DryerHistoryIdIsNotValid, "شناسه سابقه خشک کن معتبر نمی باشد" },
                {ResultStatusEnum.DryerHistoryOperationIsNotValid, "عملیات خشک کن معتبر نمی باشد" },
                {ResultStatusEnum.DryerHistoryStartTimeIsNotValid, "زمان شروع عملیات خشک کن معتبر نمی باشد" },
                {ResultStatusEnum.DryerHistoryStopTimeIsNotValid, "زمان پایان عملیات خشک کن معتبر نمی باشد" },
                {ResultStatusEnum.PleaseSelectDryerHistory, "لطفا یک ردیف را انتخاب کنید" },
                {ResultStatusEnum.DryerHistoryNumberOfBagIsNotValid, "تعداد کیسه های درون خشک کن معتبر نمی باشد" },

                #endregion

                #region RiceThreshing

                {ResultStatusEnum.RiceThreshingNotFound, "شالیکوبی یافت نشد یا شما مجاز به دسترسی نمی باشید" },
                {ResultStatusEnum.RiceThreshingIdIsNotValid, "شناسه شالیکوبی معتبر نمی باشد" },
                {ResultStatusEnum.RiceThreshingStartTimeIsNotValid, "زمان شروع شالیکوبی معتبر نمی باشد" },
                {ResultStatusEnum.RiceThreshingEndTimeIsNotValid, "زمان پایان شالیکوبی معتبر نمی باشد" },
                {ResultStatusEnum.RiceThreshingUnbrokenRiceIsNotValid, "مقدار برنج سالم شالیکوبی معتبر نمی باشد" },
                {ResultStatusEnum.RiceThreshingBrokenRiceIsNotValid, "مقدار برنج شکسته شالیکوبی معتبر نمی باشد" },
                {ResultStatusEnum.RiceThreshingChickenRiceIsNotValid, "مقدار برنج مرغی شالیکوبی معتبر نمی باشد" },
                {ResultStatusEnum.RiceThreshingFlourIsNotValid, "مقدار آرد شالیکوبی معتبر نمی باشد" },
                {ResultStatusEnum.RiceThreshingDescriptionLengthIsNotValid, "طول توضیحات شالیکوبی معتبر نمی باشد" },
                {ResultStatusEnum.PleaseSelectRiceThreshing, "لطفا یک شالیکوبی را انتخاب کنید" },
                {ResultStatusEnum.RiceThreshingEndTimeShouldGreaterThanStartTime, "زمان پایان شالیکوبی باید از زمان شروع بزرگتر باشد" },

                #endregion

                #region Income

                {ResultStatusEnum.IncomeNotFound, "درآمد یافت نشد یا شما مجاز به دسترسی نمی باشید" },
                {ResultStatusEnum.IncomeIdIsNotValid, "شناسه درآمد معتبر نمی باشد" },
                {ResultStatusEnum.IncomeIncomeTimeIsNotValid, "زمان کسب درآمد معتبر نمی باشد" },
                {ResultStatusEnum.IncomeUnbrokenRiceIsNotValid, "مقدار برنج سالم درآمد معتبر نمی باشد" },
                {ResultStatusEnum.IncomeBrokenRiceIsNotValid, "مقدار برنج شکسته درآمد معتبر نمی باشد" },
                {ResultStatusEnum.IncomeFlourIsNotValid, "مقدار آرد درآمد معتبر نمی باشد" },
                {ResultStatusEnum.IncomeDescriptionLengthIsNotValid, "طول توضیحات درآمد معتبر نمی باشد" },
                {ResultStatusEnum.PleaseSelectIncome, "لطفا یک درآمد را انتخاب کنید" },
                {ResultStatusEnum.IncomeValueIsNotValid, "مقدار درآمد معتبر نمی باشد" },

                #endregion

                #region Delivery

                {ResultStatusEnum.DeliveryNotFound, "تحویل یافت نشد یا شما مجاز به دسترسی نمی باشید" },
                {ResultStatusEnum.DeliveryIdIsNotValid, "شناسه تحویل معتبر نمی باشد" },
                {ResultStatusEnum.DeliveryDeliveryTimeIsNotValid, "زمان تحویل معتبر نمی باشد" },
                {ResultStatusEnum.DeliveryUnbrokenRiceIsNotValid, "مقدار برنج سالم برای تحویل معتبر نمی باشد" },
                {ResultStatusEnum.DeliveryBrokenRiceIsNotValid, "مقدار برنج شکسته برای تحویل معتبر نمی باشد" },
                {ResultStatusEnum.DeliveryChickenRiceIsNotValid, "مقدار برنج مرغی برای تحویل معتبر نمی باشد" },
                {ResultStatusEnum.DeliveryFlourIsNotValid, "مقدار آرد برای تحویل معتبر نمی باشد" },
                {ResultStatusEnum.DeliveryDescriptionLengthIsNotValid, "طول توضیحات تحویل معتبر نمی باشد" },
                {ResultStatusEnum.DeliveryDelivererPersonIdIsNotValid, "شناسه فرد تحویل دهنده بار معتبر نمی باشد" },
                {ResultStatusEnum.DeliveryDelivererPersonNotFound, "فرد تحویل دهنده بار یافت نشد یا شما مجاز به دسترسی نیستید" },
                {ResultStatusEnum.DeliveryReceiverPersonIdIsNotValid, "شناسه فرد تحویل گیرنده بار معتبر نمی باشد" },
                {ResultStatusEnum.DeliveryReceiverPersonNotFound, "فرد تحویل گیرنده بار یافت نشد یا شما مجاز به دسترسی نیستید" },
                {ResultStatusEnum.DeliveryCarrierPersonIdIsNotValid, "شناسه فرد حمل کننده بار معتبر نمی باشد" },
                {ResultStatusEnum.DeliveryCarrierPersonNotFound, "فرد حمل کننده بار تحویلی یافت نشد یا شما مجاز به دسترسی نیستید" },
                {ResultStatusEnum.PleaseSelectDelivery, "لطفا یک تحویل را انتخاب کنید" },
                {ResultStatusEnum.DeliveryDeliveryValueIsNotValid, "مقدار بار تحویلی معتبر نمی باشد" }

                #endregion
            };

        public static string GetErrorMessage(this ResultStatusEnum resultStatus) => ResultStatusMessage[resultStatus];
    }
}