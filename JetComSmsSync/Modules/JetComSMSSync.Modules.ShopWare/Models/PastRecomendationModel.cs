namespace JetComSMSSync.Modules.ShopWare.Models
{
    public class PastRecomendationModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Approved { get; set; }
        public string Approver_Id { get; set; }
        public string Approval_Type { get; set; }
        public bool Imported { get; set; }
        public string Vehicle_Id { get; set; }
        public bool Done { get; set; }
        public string Approval_At { get; set; }
        public string Created_At { get; set; }
        public string Updated_At { get; set; }
        public string BigID { get; set; }
    }
}
