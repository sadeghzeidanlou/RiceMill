namespace Shared.Enums
{
    public enum RoleEnum
    {
        Admin,
        RiceMillManager,
        SuperUser,
        User
    }

    public class RoleType
    {
        public string Title { get; set; } = string.Empty;

        public RoleEnum Type { get; set; }

        public byte Index { get; set; }

        public static List<RoleType> GetAll => new() {
            new RoleType { Type = RoleEnum.Admin, Title = "مدیر برنامه", Index = 0},
            new RoleType { Type = RoleEnum.RiceMillManager, Title = "مدیر کارخانه", Index = 1},
            new RoleType { Type = RoleEnum.SuperUser, Title = "کاربر ارشد", Index = 2},
            new RoleType { Type = RoleEnum.User, Title = "فقط رویت", Index = 3}
        };
    }
}