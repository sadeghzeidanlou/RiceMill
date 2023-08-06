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
}