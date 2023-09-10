namespace Shared.Enums
{
    /// <summary>
    /// Operation that can do in a dryer
    /// </summary>
    public enum DryerOperationEnum
    {
        /// <summary>
        /// Load input load to a dryer/>
        /// </summary>
        Load,

        /// <summary>
        /// UnLoad input load from a dryer/>
        /// </summary>
        Unload
    }

    public class DryerOperation
    {
        public string Title { get; set; } = string.Empty;

        public DryerOperationEnum Operation { get; set; }

        public byte Index { get; set; }

        public static List<DryerOperation> GetAll => new() {
            new DryerOperation { Operation = DryerOperationEnum.Load, Title = "بارگیری", Index = 0},
            new DryerOperation { Operation = DryerOperationEnum.Unload, Title = "تخلیه", Index = 1}
        };
    }
}