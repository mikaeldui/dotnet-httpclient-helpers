using System.Net.Http.Headers;

namespace System.Net.Http
{
    public partial class UserAgent
    {
        public UserAgent(string name, string? version = null, IEnumerable<string>? comments = null, UserAgent? dependentProduct = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Version = version;
            Comments = comments == null ? (new()) : (new(comments));
            DependentProduct = dependentProduct;
        }

        public string Name { get; set; }
        public string? Version { get; set; }
        public List<string> Comments { get; }
        public UserAgent? DependentProduct { get; set; }

        public override string ToString()
        {
            string userAgent = Name;

            if(!string.IsNullOrWhiteSpace(Version))
                userAgent += "/" + Version;

            if (this is { Comments: not null, Comments: { Count: not 0 } } && Comments.Any(c => !string.IsNullOrWhiteSpace(c)))
                userAgent += $" ({Comments.Where(c => !string.IsNullOrWhiteSpace(c)).Distinct().Join("; ")})"; 

            if (DependentProduct != null)
                userAgent += " " + DependentProduct.ToString();

            return userAgent;
        }
    }
}