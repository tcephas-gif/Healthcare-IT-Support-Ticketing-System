namespace HealthcareTicketingSystem.Helpers
{
    public static class ProfanityFilter
    {
        private static readonly string[] BannedWords =
       {
    // Strong profanity
    "fuck",
    "fucking",
    "fucked",
    "motherfucker",
    "shit",
    "bullshit",
    "asshole",
    "bitch",
    "bastard",
    "dick",
    "cock",
    "pussy",
    "cunt",
    "whore",
    "slut",

    // Hate speech
    "nigger",
    "nigga",
    "faggot",
    "retard",

    // Spam / Links
    "http://",
    "https://",
    "www.",
    ".com",
    ".net",
    ".org"
};

        public static bool ContainsInappropriateContent(params string?[] values)
        {
            string content = string.Join(" ", values.Where(v => !string.IsNullOrWhiteSpace(v)));

            return BannedWords.Any(word =>
                content.Contains(word, StringComparison.OrdinalIgnoreCase));
        }
    }
}