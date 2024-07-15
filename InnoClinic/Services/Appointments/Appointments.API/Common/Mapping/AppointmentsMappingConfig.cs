public class AppointmentsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateAppointmentRequest request, int PatientId), CreateAppointmentCommand>()
            .Map(dest => dest.Date, src => src.request.AppointmentDate)
            .Map(dest => dest.DoctorId, src => src.request.DoctorId)
            .Map(dest => dest.OfficeId, src => src.request.OfficeId)
            .Map(dest => dest.PatientId, src => src.PatientId)
            .Map(dest => dest.ServiceId, src => src.request.ServiceId)
            .Map(dest => dest.SpecializationId, src => src.request.SpecializationId)
            .Map(dest => dest.Time, src => src.request.TimeSlot);

        config.NewConfig<(CreateAppointmentResultRequest request, int DoctorId), CreateAppointmentsResultCommand>()
            .Map(dest => dest.PatientId, src => src.request.PatientId)
            .Map(dest => dest.DateofBirth, src => src.request.DateOfBirth)
            .Map(dest => dest.DoctorId, src => src.DoctorId)
            .Map(dest => dest.ServiceId, src => src.request.ServiceId)
            .Map(dest => dest.AppointmentDate, src => src.request.AppointmentDate)
            .Map(dest => dest.Conclusion, src => src.request.Conclusion)
            .Map(dest => dest.Recommendations, src => src.request.Recommendations)
            .Map(dest => dest.Complaints, src => src.request.Complaints);

        config.NewConfig<(UpdateAppointmentResultRequest request, int ResultId), EditAppointmentsResultCommand>()
            .Map(dest => dest.ResultsId, src => src.ResultId)
            .Map(dest => dest.Conclusion, src => src.request.Conclusion)
            .Map(dest => dest.Recommendations, src => src.request.Recommendations)
            .Map(dest => dest.Complaints, src => src.request.Complaints);
    }
}
