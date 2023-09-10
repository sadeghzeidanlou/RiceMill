namespace Shared.Enums
{
    /// <summary>
    /// All notices way to customer about any things
    /// </summary>
    public enum NoticesTypeEnum
    {
        /// <summary>
        /// Don't need to notice
        /// </summary>
        None,

        /// <summary>
        /// Notice via <see cref="Email"/>
        /// </summary>
        Email,

        /// <summary>
        /// Notice via <see cref="Sms"/>
        /// </summary>
        Sms
    }

    public class NoticesType
    {
        public string Title { get; set; } = string.Empty;

        public NoticesTypeEnum Type { get; set; }

        public byte Index { get; set; }

        public static List<NoticesType> GetAll => new() {
            new NoticesType { Type = NoticesTypeEnum.None, Title = "هیچکدام", Index = 0},
            new NoticesType { Type = NoticesTypeEnum.Email, Title = "ایمیل", Index = 1},
            new NoticesType { Type = NoticesTypeEnum.Sms, Title = "پیام کوتاه", Index = 2}
        };
    }
}