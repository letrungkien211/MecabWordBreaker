namespace MecabWordBreaker
{
    /// <summary>
    /// Mecab word
    /// </summary>
    public class PartOfSpeech
    {
        /// <summary>
        /// Text value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Part of Speech Tagging. Noun, Verb, Adjective
        /// </summary>
        public string PosTag { get; set; }

        /// <summary>
        /// Reading
        /// </summary>
        public string Reading { get; set; }
    }
}
