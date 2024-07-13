public interface IProfileService
{
    public Task<bool> DoctorExistsAsync(int doctorId);
    public Task<bool> PatientExistsAsync(int patientId);
    public Task<PatientProfileResponse> GetPatientAsync(int patientId);
    public Task<DoctorProfileResponse> GetDoctorAsync(int doctorId);
}
