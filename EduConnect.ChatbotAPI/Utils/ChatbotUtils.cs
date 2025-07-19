using System.Text.RegularExpressions;

namespace EduConnect.ChatbotAPI.Utils
{
    public class ChatbotUtils
    {
        public static string RemoveThinkTags(string? text)
        {
            // Regex sẽ nhận mọi chuỗi <think>…</think>, bao gồm cả nội dung bên trong,
            // ngay cả khi có xuống dòng hoặc nội dung lồng ghép.
            string pattern = @"<think\b[^>]*>.*?<\/think>";
            return Regex.Replace(text!, pattern, "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        }
    }
}
