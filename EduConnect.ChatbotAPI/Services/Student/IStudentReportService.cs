namespace EduConnect.ChatbotAPI.Services.Student
{
    public interface IStudentReportService
    {
        Task StudentReportDaily();
        Task StudentReportWeekly();

    }
}
