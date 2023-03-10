namespace RedTechBackEnd.Dto
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public string OrderType { get; set; }

        public string CustomerName { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedByUsername { get; set; }

    }
}
