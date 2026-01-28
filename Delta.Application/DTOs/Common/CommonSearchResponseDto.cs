namespace Delta.Application.DTOs.Common
{
    public class CommonSearchResponseDto
    {
        public string DisplayName { get; set; } = string.Empty;

        public List<string> Headers { get; set; }
            = new List<string>();

        public List<CommonSearchRowDto> Data { get; set; }
            = new List<CommonSearchRowDto>();
    }
}
