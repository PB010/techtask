namespace TechTask.Application.Helper
{
    public class NotNullConvert
    {
        public static void AssignIfNotNull<T>(ref T target, T value, T check)
        {
            target = check == null ? target = value : target;
        }
    }
}
