namespace Shared.Enums
{
    public enum GenderEnum
    {
        Men,
        Women
    }

    public class GenderType
    {
        public string Title { get; set; } = string.Empty;

        public GenderEnum Type{ get; set; }

        public byte Index { get; set; }

        public static List<GenderType> GetAll => new() {
            new GenderType { Type = GenderEnum.Men, Title = "مرد", Index = 0},
            new GenderType { Type = GenderEnum.Women, Title = "زن", Index = 1}
        };
    }
}