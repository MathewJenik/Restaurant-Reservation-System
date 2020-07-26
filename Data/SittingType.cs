using System.ComponentModel.DataAnnotations;

namespace T2RMSWS.Data
{
    public class SittingType
    {

        [Key]
        public int Id { get; set; }
        public string Description { get; set; }

    }
}