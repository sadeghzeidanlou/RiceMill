namespace Shared.Enums
{
    public enum VehicleTypeEnum
    {
        Motorcycle,
        Tractor,
        PeykanPickupTruck,
        Nissan,
        Isuzo,
        Khavar,
        Savari,
        MazdaPickupTruck,
        Truck
    }

    public class VehicleType
    {
        public string Title { get; set; }

        public VehicleTypeEnum Type { get; set; }

        public static List<VehicleType> GetAll => new() {
            new VehicleType { Type = VehicleTypeEnum.Tractor, Title = "تراکتور" },
            new VehicleType { Type = VehicleTypeEnum.Nissan, Title = "نیسان" },
            new VehicleType { Type = VehicleTypeEnum.PeykanPickupTruck, Title = "پیکان وانت" },
            new VehicleType { Type = VehicleTypeEnum.MazdaPickupTruck, Title = "وانت مزدا" },
            new VehicleType { Type = VehicleTypeEnum.Savari, Title = "سواری" },
            new VehicleType { Type = VehicleTypeEnum.Truck, Title = "کامیون" },
            new VehicleType { Type = VehicleTypeEnum.Motorcycle, Title = "موتور سیکلت" },
            new VehicleType { Type = VehicleTypeEnum.Isuzo, Title = "ایسوزو" },
            new VehicleType { Type = VehicleTypeEnum.Khavar, Title = "خاور" }
        };
    }
}