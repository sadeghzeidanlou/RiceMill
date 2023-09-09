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
        public string Title { get; set; } = string.Empty;

        public VehicleTypeEnum Type { get; set; }

        public byte Index { get; set; }

        public static List<VehicleType> GetAll => new() {
            new VehicleType { Type = VehicleTypeEnum.Tractor, Title = "تراکتور", Index = 0},
            new VehicleType { Type = VehicleTypeEnum.Nissan, Title = "نیسان", Index = 1},
            new VehicleType { Type = VehicleTypeEnum.PeykanPickupTruck, Title = "پیکان وانت", Index = 2},
            new VehicleType { Type = VehicleTypeEnum.MazdaPickupTruck, Title = "وانت مزدا" , Index = 3},
            new VehicleType { Type = VehicleTypeEnum.Savari, Title = "سواری" , Index = 4},
            new VehicleType { Type = VehicleTypeEnum.Truck, Title = "کامیون" , Index = 5},
            new VehicleType { Type = VehicleTypeEnum.Motorcycle, Title = "موتور سیکلت" , Index = 6},
            new VehicleType { Type = VehicleTypeEnum.Isuzo, Title = "ایسوزو" , Index = 7},
            new VehicleType { Type = VehicleTypeEnum.Khavar, Title = "خاور" , Index = 8}
        };
    }
}