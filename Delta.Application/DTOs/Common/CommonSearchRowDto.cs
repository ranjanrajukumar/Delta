namespace Delta.Application.DTOs.Common
{
    public class CommonSearchRowDto
    {
        public int Id { get; set; }

        public Dictionary<string, string> Columns { get; set; }
            = new Dictionary<string, string>();
    }
}
