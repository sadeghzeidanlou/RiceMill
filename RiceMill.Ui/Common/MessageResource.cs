﻿using RiceMill.Application.Common.Models.Enums;

namespace RiceMill.Ui.Common
{
    public static class MessageDictionary
    {
        private static readonly Dictionary<ResultStatusEnum, string> MessageText = new()
            {
                #region Genearl
                
                {ResultStatusEnum.Success,"عملیات '{0}' با موفقیت انجام شد" },
                {ResultStatusEnum.Fail,"عملیات '{0}' دچار خطا شده است" },
                {ResultStatusEnum.Unauthorized, "احراز هویت بدرستی انجام نشده است" },
                {ResultStatusEnum.Forbidden, "شما مجاز به ادامه عملیات نمی باشد" },
                {ResultStatusEnum.NotImplemented, "درحال حاضر عملیات مورد نظر در سیستم پشتیبانی نمی شود" },
                {ResultStatusEnum.DatabaseError, "خطایی در سمت پایگاه داده رخ داده است" },
                {ResultStatusEnum.UnHandleError, "خطای پیش بینی نشده اتفاق افتاده است" },
                {ResultStatusEnum.Alert, "هشدار" },
                {ResultStatusEnum.GoBack, "بازگشت" },
                
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
                {ResultStatusEnum.PaymentPaidPersonIdIsNotValid, "شناسه فرد دریافت کننده معتبر نمی باشد" },

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
                {ResultStatusEnum.RiceThreshingStartTimeIsNotValid, "زمان شروع شالیکوبی معتبر نمی باشد" },
                {ResultStatusEnum.RiceThreshingEndTimeIsNotValid, "زمان پایان شالیکوبی معتبر نمی باشد" },
                {ResultStatusEnum.RiceThreshingUnbrokenRiceIsNotValid, "مقدار برنج سالم شالیکوبی معتبر نمی باشد" },
                {ResultStatusEnum.RiceThreshingBrokenRiceIsNotValid, "مقدار برنج شکسته شالیکوبی معتبر نمی باشد" },
                {ResultStatusEnum.RiceThreshingChickenRiceIsNotValid, "مقدار برنج مرغی شالیکوبی معتبر نمی باشد" },
                {ResultStatusEnum.RiceThreshingFlourIsNotValid, "مقدار آرد شالیکوبی معتبر نمی باشد" },
                {ResultStatusEnum.RiceThreshingDescriptionLengthIsNotValid, "طول توضیحات شالیکوبی معتبر نمی باشد" },

                #endregion

                #region Income

                {ResultStatusEnum.IncomeNotFound, "درآمد یافت نشد یا شما مجاز به دسترسی نمی باشید" },
                {ResultStatusEnum.IncomeIdIsNotValid, "شناسه درآمد معتبر نمی باشد" },
                {ResultStatusEnum.IncomeIncomeTimeIsNotValid, "زمان کسب درآمد معتبر نمی باشد" },
                {ResultStatusEnum.IncomeUnbrokenRiceIsNotValid, "مقدار برنج سالم درآمد معتبر نمی باشد" },
                {ResultStatusEnum.IncomeBrokenRiceIsNotValid, "مقدار برنج شکسته درآمد معتبر نمی باشد" },
                {ResultStatusEnum.IncomeFlourIsNotValid, "مقدار آرد درآمد معتبر نمی باشد" },
                {ResultStatusEnum.IncomeDescriptionLengthIsNotValid, "طول توضیحات درآمد معتبر نمی باشد" },

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
                {ResultStatusEnum.DeliveryCarrierPersonNotFound, "فرد حمل کننده بار تحویلی یافت نشد یا شما مجاز به دسترسی نیستید" }

                #endregion
            };

        public static string GetMessageText(this ResultStatusEnum resultStatus) => MessageText[resultStatus];
    }
}