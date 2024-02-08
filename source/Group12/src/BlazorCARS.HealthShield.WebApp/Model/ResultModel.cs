namespace BlazorCARS.HealthShield.WebApp.Model
{
    public class ResultModel
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
        public int Count { get; set; }
    }
}
