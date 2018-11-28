using System;
using System.Collections.Generic;
using System.Linq;
using NMeCab;

namespace MecabWordBreaker
{
    /// <summary>
    /// Mecab wrapper
    /// </summary>
    public class MecabWB
    {
        private MeCabTagger MeCabTagger { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="meCabTagger">Mecab tagger</param>
        public MecabWB(MeCabTagger meCabTagger)
        {
            MeCabTagger = meCabTagger;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dirDic">path which contains dictionary</param>
        public MecabWB(string dirDic) : this(MeCabTagger.Create(new MeCabParam()
        {
            DicDir = dirDic
        }))
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MecabWB() : this(@".\dic\ipadic")
        {
        }

        /// <summary>
        /// Break
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public IEnumerable<string> BreakAsList(string s)
        {
            return BreakAsMecabNodes(s).Select(x => x.Surface);
        }

        /// <summary>
        /// Word Break
        /// </summary>
        /// <param name="s"></param>
        /// <param name="joinDelimiter"></param>
        /// <returns></returns>
        public string Break(string s, string joinDelimiter = " ")
        {
            return string.Join(joinDelimiter, BreakAsList(s));
        }


        /// <summary>
        /// Break as nodes
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public IEnumerable<MeCabNode> BreakAsMecabNodes(string s)
        {
            var tokens = new List<string>();
            var node = MeCabTagger.ParseToNode(s);

            while (node != null)
            {
                if (node.CharType > 0)
                {
                    yield return node;
                }
                node = node.Next;
            }
        }

        /// <summary>
        /// Break as part of speech
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public IEnumerable<PartOfSpeech> BreakAsPOSs(string s)
        {
            return BreakAsMecabNodes(s).Select(x =>
            {
                var tags = x.Feature?.Split(',');
                if (tags == null || tags.Length < 9)
                    throw new IndexOutOfRangeException($"OriginalString={s}. Feature={x.Feature}");

                return new PartOfSpeech()
                {
                    Value = x.Surface,
                    PosTag = tags[0],
                    Reading = tags[6]
                };
            });
        }
    }
}
