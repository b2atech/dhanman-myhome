namespace Dhanman.MyHome.Application.Contracts;
 public class CustomerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CompanyId { get; set; }
    public Guid CreatedBy { get; set; }

}