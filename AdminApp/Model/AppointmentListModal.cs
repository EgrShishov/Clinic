public class AppointmentListModal
{
    public int Id { get; set; }
    public string Time { get; set; }
    public string PatientName { get; set; }
    public string ServiceName { get; set; }
    public bool IsApproved { get; set; }
    public string ApprovalStatus => IsApproved ? "Approved" : "Not Approved";
}
