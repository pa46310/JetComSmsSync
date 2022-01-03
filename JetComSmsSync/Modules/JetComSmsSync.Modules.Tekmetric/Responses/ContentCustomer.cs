using System.Collections.Generic;

namespace JetComSmsSync.Modules.Tekmetric.Responses
{
    public class ContentCustomer
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Phone> Phone { get; set; }
        public Address Address { get; set; }
        public string Notes { get; set; }
        public CustomerType CustomerType { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public int ShopId { get; set; }
        public bool OkForMarketing { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string DeletedDate { get; set; }
        public string Birthday { get; set; }
        public string BigID { get; set; }

        public void UpdateParameters(string bigId)
        {
            BigID = bigId;
            if (Phone != null && Phone.Count > 0)
            {
                foreach (var phone in Phone)
                {
                    phone.BigID = bigId;
                    phone.CustomerId = Id;
                }
            }

            if (Address != null)
            {
                Address.BigID = bigId;
                Address.CustomerId = Id;
            }

            if (CustomerType != null)
            {
                CustomerType.BigID = bigId;
                CustomerType.CustomerId = Id;
            }
        }
    }
}
