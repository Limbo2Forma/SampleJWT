using System.ComponentModel.DataAnnotations;

namespace SampleJWT.Models {
    public class Author {
        public int AuthorId { get; set; }
        [Required]
        public string Name { get; set; }

        internal static bool TryParse(string key, out Author result) {
            result = new Author {
                AuthorId = 99,
                Name = key
            };
            return true;
        }
    }
}