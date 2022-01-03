namespace JetComSmsSync.Modules.Tekmetric.Models
{
    public static class Comparers
    {
        public static CustomerComparer Customer { get; } = new CustomerComparer();
        public static AddressComparer Address { get; } = new AddressComparer();
        public static PhoneComparer Phone { get; } = new PhoneComparer();
        public static CustomerTypeComparer CustomerType { get; } = new CustomerTypeComparer();
        public static VehicleComparer Vehicle { get; } = new VehicleComparer();
        public static RepairOrderComparer RepairOrder { get; } = new RepairOrderComparer();
        public static AppointmentComparer Appointment { get; } = new AppointmentComparer();
        public static JobComparer Job { get; } = new JobComparer();
        public static PartComparer Part { get; } = new PartComparer();
        public static LaborComparer Labor { get; } = new LaborComparer();
    }
}
